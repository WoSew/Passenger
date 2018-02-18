using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Passenger.Core.Repositories;
using Passenger.Infrastructure.Mappers;
using Passenger.Infrastructure.Repositories;
using Passenger.Infrastructure.Services;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Passenger.Infrastructure.IoC.Modules;
using Passenger.Infrastructure.IoC;
using Microsoft.IdentityModel.Tokens;
using Passenger.Infrastructure.Settings;
using System.Text;
using Newtonsoft.Json;
using Passenger.Api.Framework;
using NLog.Extensions.Logging;
using NLog.Web;
using Passenger.Infrastructure.Mongo;

namespace Passenger.Api
{
    public class Startup
    {
        public IConfigurationRoot Configuration { get; }
        public IContainer ApplicationContainer { get; private set; } //bedzie trzymal nasze konfiguracje dla konterna IoC
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        } 
        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services) //zwracamy IServiceProvider by moc podmieniec domyslny konterner na nasz wlasny
        {
            // Add framework services.

            services.AddAuthorization( x => x.AddPolicy("admin", p=> p.RequireRole("admin")));
            services.AddMemoryCache();
            services.AddMvc()
                .AddJsonOptions( x => x.SerializerSettings.Formatting = Formatting.Indented); //zmienamy sposob wyswietlania JSON

            //Autofac implementation
            var builder = new ContainerBuilder();
            builder.Populate(services); // przekazujemy kolekcje services by zachować spójność z tym co mieliście wcześniej (inicjalizacja calego MVC itd.)
            builder.RegisterModule(new ContainerModule(Configuration));
            ApplicationContainer = builder.Build();

            return new AutofacServiceProvider(ApplicationContainer);
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, IApplicationLifetime appLifetime)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            // loggerFactory.AddNLog();
            // app.AddNLogWeb();
            // env.ConfigureNLog("nlog.config");

            var jwtSettings = app.ApplicationServices.GetService<JwtSettings>();
                        app.UseJwtBearerAuthentication(new JwtBearerOptions
            {
                AutomaticAuthenticate = true, 
                TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = jwtSettings.Issuer, //jesli adres aplikacji ktora wygenerowala kod bedzie inny niz ten - blad aplikacji
                    ValidateAudience = false,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key)) //sposob podpisywania klucza
                }
            });

            MongoConfigurator.Initialize();
            
            var generalSettings = app.ApplicationServices.GetService<GeneralSettings>();
            if(generalSettings.SeedData)
            {
                var dataInitializer = app.ApplicationServices.GetService<IDataInitializer>();
                dataInitializer.SeedAsync();
            }

            app.UserExceptionHandler();
            app.UseMvc();         

            appLifetime.ApplicationStopped.Register(() => ApplicationContainer.Dispose()); //w razie zatrzymania aplikacji, wywola metode register i wywola na kontenerze metode dispose by wyczyscic nieuzytki
        }
    }
}
