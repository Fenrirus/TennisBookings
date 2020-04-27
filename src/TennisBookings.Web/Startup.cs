namespace TennisBookings.Web
{
    using System;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;
    using TennisBookings.Web.Configuration;
    using TennisBookings.Web.Services;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IWeatherForecaster, WeatherForecaster>();

            // services.AddSingleton<IWeatherForecaster, WeatherForecaster>();
            // sprawi, że wywołany zostanie pierwsza wartość, pomimot zarejestrownia dwóch
            // services.TryAddSingleton<IWeatherForecaster, AmazingWeatherForecaster>();
            // podmienia wartość
            // services.Replace(ServiceDescriptor.Singleton<IWeatherForecaster, AmazingWeatherForecaster>());
            // usuwa wszystkie wartości
            // services.RemoveAll<IWeatherForecaster>();
            services.Configure<FeaturedConfiguration>(Configuration.GetSection("Features"));
            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseMvc();
        }
    }
}