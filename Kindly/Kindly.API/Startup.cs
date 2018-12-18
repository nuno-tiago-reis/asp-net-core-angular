using AutoMapper;

using System.Net;
using System.Text;

using Kindly.API.Models;
using Kindly.API.Models.Repositories;
using Kindly.API.Utility;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

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
		/// <param name="configuration">The configuration.</param>
		public Startup(IConfiguration configuration)
		{
			this.Configuration = configuration;
		}

		/// <summary>
		/// This method gets called by the runtime. Use this method to add services to the container.
		/// </summary>
		/// <param name="services">The services.</param>
		public void ConfigureServices(IServiceCollection services)
		{
			// Cors
			services.AddCors();
			// Auto Mapper
			services.AddAutoMapper();
			// Compatibility
			services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
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
						Encoding.UTF8.GetBytes(this.Configuration.GetSection("AppSettings:Secret").Value)
					)
				};
			});
			// Database Context
			services.AddDbContext<KindlyContext>(options =>
			{
				options.UseSqlServer(this.Configuration.GetConnectionString("DefaultConnection"));
			});
			// Database Repositories
			services.AddScoped<IUserRepository, UserRepository>();
		}

		/// <summary>
		/// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		/// </summary>
		/// <param name="appBuilder">The application builder.</param>
		/// <param name="environment">The environment.</param>
		public void Configure(IApplicationBuilder appBuilder, IHostingEnvironment environment)
		{
			if (environment.IsDevelopment())
			{
				//appBuilder.UseDeveloperExceptionPage();
				appBuilder.UseExceptionHandler(builder =>
				{
					builder.Run(async context =>
					{
						var error = context.Features.Get<IExceptionHandlerFeature>();

						if (error.Error is KindlyException kindlyException && kindlyException.StatusCode.HasValue)
							context.Response.StatusCode = kindlyException.StatusCode.Value;
						else
							context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

						await context.Response.AddApplicationError(error.Error.Message);

					});
				});
			}
			else
			{
				appBuilder.UseExceptionHandler(builder =>
				{
					builder.Run(async context =>
					{
						var error = context.Features.Get<IExceptionHandlerFeature>();

						if (error.Error is KindlyException kindlyException && kindlyException.StatusCode.HasValue)
							context.Response.StatusCode = kindlyException.StatusCode.Value;
						else
							context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

						await context.Response.AddApplicationError(error.Error.Message);

					});
				});
				appBuilder.UseHsts();
			}

			appBuilder.UseCors(action =>
			{
				action.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
			});
			appBuilder.UseAuthentication();
			appBuilder.UseMvc();
		}
	}
}