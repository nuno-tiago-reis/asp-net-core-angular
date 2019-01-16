using AutoMapper;

using JetBrains.Annotations;

using Kindly.API.Controllers;
using Kindly.API.Controllers.Auth;
using Kindly.API.Controllers.Likes;
using Kindly.API.Controllers.Messages;
using Kindly.API.Controllers.Pictures;
using Kindly.API.Controllers.Users;
using Kindly.API.Models;
using Kindly.API.Models.Repositories.Likes;
using Kindly.API.Models.Repositories.Pictures;
using Kindly.API.Models.Repositories.Messages;
using Kindly.API.Models.Repositories.Users;
using Kindly.API.Models.Repositories.Roles;
using Kindly.API.Utility;
using Kindly.API.Utility.Settings;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

using System.Text;

using Swashbuckle.AspNetCore.Swagger;

namespace Kindly.API
{
	public sealed class Startup
	{
		/// <summary>
		/// Gets the configuration.
		/// </summary>
		public IConfiguration Configuration { get; }

		/// <summary>
		/// Initializes a new instance of the <see cref="Startup"/> class.
		/// </summary>
		/// 
		/// <param name="configuration">The configuration.</param>
		public Startup(IConfiguration configuration)
		{
			this.Configuration = configuration;
		}

		/// <summary>
		/// This method gets called by the runtime. Use this method to add services to the container.
		/// </summary>
		/// 
		/// <param name="services">The services.</param>
		[UsedImplicitly]
		public void ConfigureServices(IServiceCollection services)
		{
			// Cors
			services.AddCors();

			// Auto Mapper
			services.AddAutoMapper();

			// Compatibility
			services
				.AddMvc(options =>
				{
					var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();

					options.Filters.Add(new AuthorizeFilter(policy));

				})
				.AddJsonOptions(options =>
				{
					options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
					options.SerializerSettings.Converters.Add(new StringEnumConverter(true));
				})
				.SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

			// Database Context
			services.AddTransient<KindlySeeder>();
			services.AddDbContext<KindlyContext>(options =>
			{
				options.UseSqlServer(this.Configuration.GetConnectionString(KindlyConstants.DefaultConnection));
			});

			services.AddScoped<ILikeRepository, LikeRepository>();
			services.AddScoped<IMessageRepository, MessageRepository>();
			services.AddScoped<IPictureRepository, PictureRepository>();
			services.AddScoped<IUserRepository, UserRepository>();
			services.AddScoped<IRoleRepository, RoleRepository>();

			// Configurations
			services.Configure<CloudinarySettings>(Configuration.GetSection(KindlyConstants.AppSettingsCloudinary));

			// Filters
			services.AddScoped<KindlyActivityFilter>();

			// Authorization - Policies
			services.AddAuthorization(options =>
			{
				options.AddPolicy(nameof(KindlyPolicies.AllowIfOwner), policy =>
				{
					policy.AddRequirements(new ResourceOwnerRequirement());
				});
				options.AddPolicy(nameof(KindlyPolicies.AllowIfElevatedUser), policy =>
				{
					policy.AddRequirements(new ElevatedUserRequirement());
				});
			});

			// Authorization - Handlers
			services.AddScoped<IAuthorizationHandler, AuthOwnerHandler>();
			services.AddScoped<IAuthorizationHandler, LikesOwnerHandler>();
			services.AddScoped<IAuthorizationHandler, MessagesOwnerHandler>();
			services.AddScoped<IAuthorizationHandler, PicturesOwnerHandler>();
			services.AddScoped<IAuthorizationHandler, UsersOwnerHandler>();
			services.AddScoped<IAuthorizationHandler, ElevatedUserHandler>();

			// Authentication - Identity
			var builder = services.AddIdentityCore<User>(options =>
			{
				// User
				options.User.RequireUniqueEmail = true;

				// Password
				options.Password.RequiredLength = 6;
				options.Password.RequireDigit = true;
				options.Password.RequireLowercase = false;
				options.Password.RequireUppercase = false;
				options.Password.RequiredUniqueChars = 1;
				options.Password.RequireNonAlphanumeric = true;
			});

			builder = new IdentityBuilder(builder.UserType, typeof(Role), builder.Services);
			builder.AddEntityFrameworkStores<KindlyContext>();
			builder.AddSignInManager<SignInManager<User>>();
			builder.AddRoleValidator<RoleValidator<Role>>();
			builder.AddRoleManager<RoleManager<Role>>();

			// Authentication - JWT Token Generator
			services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
			{
				options.TokenValidationParameters = new TokenValidationParameters
				{
					ValidateIssuer = false,
					ValidateAudience = false,
					ValidateIssuerSigningKey = true,
					IssuerSigningKey = new SymmetricSecurityKey
					(
						Encoding.UTF8.GetBytes(this.Configuration.GetSection(KindlyConstants.AppSettingsEncryptionKey).Value)
					)
				};
			});

			// Swagger
			services.AddSwaggerGen(options =>
			{
				options.SwaggerDoc("v1", new Info { Title = "Kindly API", Version = "v1" });
			});
		}

		/// <summary>
		/// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		/// </summary>
		/// 
		/// <param name="applicationBuilder">The application builder.</param>
		/// <param name="environment">The environment.</param>
		/// <param name="seeder">The seeder.</param>
		[UsedImplicitly]
		public void Configure(IApplicationBuilder applicationBuilder, IHostingEnvironment environment, KindlySeeder seeder)
		{
			if (environment.IsDevelopment())
			{
				applicationBuilder.UseExceptionHandler(builder =>
				{
					builder.Run(KindlyUtilities.ProcessException);
				});
				seeder.SeedUsers();
			}
			else
			{
				applicationBuilder.UseExceptionHandler(builder =>
				{
					builder.Run(KindlyUtilities.ProcessException);
				});
				applicationBuilder.UseHsts();
			}

			// Enable middleware to serve generated Swagger as a JSON endpoint.
			applicationBuilder.UseSwagger();

			// Enable middleware to serve Swagger UI specifying the Swagger JSON endpoint.
			applicationBuilder.UseSwaggerUI(options =>
			{
				options.SwaggerEndpoint("/swagger/v1/swagger.json", "Kindly API V1");
				//options.RoutePrefix = string.Empty;
			});

			// Enable CORS
			applicationBuilder.UseCors(action =>
			{
				action.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
			});
			// Enable Authentication
			applicationBuilder.UseAuthentication();
			// Enable MVC
			applicationBuilder.UseMvc();
		}
	}
}