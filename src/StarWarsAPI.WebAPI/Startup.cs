using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using FluentValidation;
using AutoMapper;
using FluentValidation.AspNetCore;
using StarWarsAPI.Domain.Interfaces;
using StartWarsAPI.Infra.Repositories;
using StartWarsAPI.Infra.Interfaces;
using StartWarsAPI.Infra.Context;
using StarWarsAPI.Domain.Entities;
using StarWarsAPI.Domain.Services;
using StarWarsAPI.Application.Interfaces;
using StarWarsAPI.Application.AutoMapper;

namespace StarWarsAPI.WebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            

            

            services.AddResponseCompression();

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "StaWars API", Version = "v1" });
            });

            //Registering dependency injection
            services.AddSingleton<IPlanetRepository, PlanetRepository>();
            services.AddSingleton<IPlanetContext, PlanetContext>();
            services.AddTransient<IValidator<Planet>, PlanetValidator>();
            services.AddSingleton<IPlanetService, PlanetService>();
            services.AddSingleton<IPlanetApplicationService, PlanetApplicationService>();

            //Register automapper
            var config = new AutoMapper.MapperConfiguration(cfg => 
            {
                cfg.AddProfile(new DomainToViewModelMapping());

            });

            var mapper = config.CreateMapper();
            services.AddSingleton(mapper);

            //Register fluent validation, using only fluent validation and validating child properties
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddFluentValidation(fv => {
                    fv.RunDefaultMvcValidationAfterFluentValidationExecutes = false;
                    fv.ImplicitlyValidateChildProperties = true;
                });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseStaticFiles();
            //Enable middleware to serve generated Swagger as a Json endpoint
            app.UseSwagger();

            //Enable middleware to serve swagger-ui(static files) and specifying the swagger endpoint
            app.UseSwaggerUI(c => 
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "StarWars API V1");
            });

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
