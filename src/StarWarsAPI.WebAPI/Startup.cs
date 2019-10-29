using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using FluentValidation;
using FluentValidation.AspNetCore;
using StarWarsAPI.Domain.Interfaces;
using StarWarsAPI.Infra.Repositories;
using StarWarsAPI.Infra.Interfaces;
using StarWarsAPI.Infra.Context;
using StarWarsAPI.Domain.Entities;
using StarWarsAPI.Domain.Services;
using StarWarsAPI.Application.AutoMapper;
using StarWarsAPI.Application.Interfaces;
using StarWarsAPI.Application.Services;
using System.Reflection;
using System.IO;
using System;
using Swashbuckle.AspNetCore.Swagger;

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
            services.AddMvc(options =>
            {
                options.RespectBrowserAcceptHeader = true;
            })
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                .AddFluentValidation(fv =>
                {
                    fv.RunDefaultMvcValidationAfterFluentValidationExecutes = false;
                    fv.ImplicitlyValidateChildProperties = true;
                });

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "StarWars API",
                    Description = "A sample API using ,Net Core and MongoDB",
                    TermsOfService = "None",
                    Contact = new Contact
                    {
                        Name = "Gerson Cardoso Filho",
                        Email = "gersoncfilho@mac.com",
                        Url = "https://github.com/gersondeveloper"
                    }
                });

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseStaticFiles();
           
            //Enable middleware to serve generated Swagger as a Json endpoint
            app.UseSwagger();

            //Enable middleware to serve swagger-ui(static files) and specifying the swagger endpoint
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "StarWars API V1");
            });

            app.UseMvc();

        }
    }
}
