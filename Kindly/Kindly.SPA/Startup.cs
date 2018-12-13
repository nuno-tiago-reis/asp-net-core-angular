using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Kindly.SPA
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
			Configuration = configuration;
		}

		/// <summary>
		/// This method gets called by the runtime. Use this method to add services to the container.
		/// </summary>
		/// <param name="services">The services.</param>
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
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
				appBuilder.UseDeveloperExceptionPage();
			}
			else
			{
				appBuilder.UseHsts();
			}
  
			appBuilder.UseHttpsRedirection();
			appBuilder.UseDefaultFiles();
			appBuilder.UseStaticFiles();
			appBuilder.UseMvc();
		}
	}
}