﻿using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TheFipster.Munchkin.Api.Common;
using TheFipster.Munchkin.Gaming.Api.Extensions;
using TheFipster.Munchkin.Gaming.Api.Middlewares;

namespace TheFipster.Munchkin.Gaming.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration) => Configuration = configuration;

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthorization();
            services.AddJwtAuth(Configuration);
            services.AddCorsPolicy(Configuration);
            services.AddDependecies();
            services.AddGameMessageModelBinder();
        }

        public void Configure(IApplicationBuilder app, IHostEnvironment env)
        {
            app.UseCorsPolicy();
            app.UseMiddleware<ExceptionMiddleware>();
            app.UseProxy();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseControllers();
            app.UseDeveloperSettingsBasedOnEnvironment(env);
        }
    }
}
