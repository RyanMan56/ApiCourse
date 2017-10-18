using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Serialization;
using Microsoft.AspNetCore.Mvc.Formatters;
using NLog.Extensions.Logging;
using NLog.Web;
using CityInfo.API.Services;
using Microsoft.Extensions.Configuration;
using CityInfo.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace CityInfo.API
{
    public class Startup
    {
        // The ASP.NET Core 1.0 way of doing it. This is done in CreateDefaultBuilder in the Program class
        //public static IConfigurationRoot Configuration;                
        //public Startup(IHostingEnvironment env)
        //{
        //    var builder = new ConfigurationBuilder()
        //        .SetBasePath(env.ContentRootPath)
        //        .AddJsonFile("appSettings.json", optional: false, reloadOnChange: true);

        //    Configuration = builder.Build(); 
        //}

        // ASP.NET Core 2.X way of doing it
        public static IConfiguration Configuration { get; private set; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services) // Service is a component intended for common consumption in an application (Entity Framework Core, MVC, etc)
        {
            services.AddMvc()
                .AddMvcOptions(o => o.OutputFormatters.Add(
                    new XmlDataContractSerializerOutputFormatter())); // Default format is the first one in the collection (int this case JSON since we didn't Clear it)
                                                                      //.AddJsonOptions(o => 
                                                                      //{
                                                                      //    if (o.SerializerSettings.ContractResolver != null)
                                                                      //    {
                                                                      //        var castedResolver = o.SerializerSettings.ContractResolver as DefaultContractResolver;
                                                                      //        castedResolver.NamingStrategy = null;
                                                                      //    }
                                                                      //});
#if DEBUG
            services.AddTransient<IMailService, LocalMailService>();
#else
            services.AddTransient<IMailService, CloudMailService>();
#endif
            var connectionString = Startup.Configuration["connectionStrings:cityInfoDbConnectionString"];
            services.AddDbContext<CityInfoContext>(o => o.UseSqlServer(connectionString));

            services.AddScoped<ICityInfoRepository, CityInfoRepository>(); // Scoped is best for a repository
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, CityInfoContext cityInfoContext)
        {
            loggerFactory.AddConsole();

            loggerFactory.AddDebug();

            //loggerFactory.AddProvider(new NLog.Extensions.Logging.NLogLoggerProvider()); // But there's a shortcut with most providers
            loggerFactory.AddNLog(); // Anything logged gets automagically logged with NLog as well, since we've added it here

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            cityInfoContext.EnsureSeedDataForContext();

            //app.UseStatusCodePages();

            AutoMapper.Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Entities.City, Models.CityWithoutPointsOfInterestDto>(); // Convention based; will map property names on the source object to the same names on the destination object.
                                                                                       // If property doesn't exist, it will be ignored.
                cfg.CreateMap<Entities.City, Models.CityDto>();
                // CityDto includes pointOfInterests, so these need mapping too even if no requests are made for the pointOfInterest
                cfg.CreateMap<Entities.PointOfInterest, Models.PointOfInterestDto>();
                cfg.CreateMap<Models.PointOfInterestForCreationDto, Entities.PointOfInterest>(); // automapper ignores additional properties
                cfg.CreateMap<Models.PointOfInterestForUpdateDto, Entities.PointOfInterest>();
                cfg.CreateMap<Entities.PointOfInterest, Models.PointOfInterestForUpdateDto>();
            });

            app.UseMvc(); // Added to the pipeline after logger so that exceptions can be caught before handing the request over to MVC. And so exceptions can be caught in the MVC code

            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync("Hello World!");
            //});
        }
    }
}
