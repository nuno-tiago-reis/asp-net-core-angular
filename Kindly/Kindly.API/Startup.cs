using AutoMapper;

using System.Text;

using Kindly.API.Models;
using Kindly.API.Models.Repositories;
using Kindly.API.Utility;
using Kindly.API.Utility.Configurations;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

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
		public void ConfigureServices(IServiceCollection services)
		{
			// Cors
			services.AddCors();

			// Auto Mapper
			services.AddAutoMapper();

			// Compatibility
			services.AddMvc().AddJsonOptions(options =>
				{
					options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
					options.SerializerSettings.Converters.Add(new StringEnumConverter());
				})
				.SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

			// Authentication
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

			// Database Context
			services.AddTransient<KindlySeeder>();
			services.AddDbContext<KindlyContext>(options =>
			{
				options.UseSqlServer(this.Configuration.GetConnectionString(KindlyConstants.DefaultConnection));
			});

			// Database Repositories
			services.AddScoped<IUserRepository, UserRepository>();
			services.AddScoped<IPictureRepository, PictureRepository>();

			// Configurations
			services.Configure<CloudinarySettings>(Configuration.GetSection(KindlyConstants.AppSettingsCloudinary));
		}

		/// <summary>
		/// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		/// </summary>
		/// 
		/// <param name="applicationBuilder">The application builder.</param>
		/// <param name="environment">The environment.</param>
		/// <param name="seeder">The seeder.</param>
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

			applicationBuilder.UseCors(action =>
			{
				action.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
			});
			applicationBuilder.UseAuthentication();
			applicationBuilder.UseMvc();
		}
	}
}