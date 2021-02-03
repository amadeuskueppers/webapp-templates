using Ak.Template.Logic;
using Ak.Template.Logic.Contract;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System.Collections.Generic;

namespace Ak.Template.WebApi
{
    public class Startup
    {
        // todo: allowedOrigins in config auslagern (frontend base Url anpassen bei production)
        List<string> allowedOrigins = new List<string>
        {
            "http://localhost:4200",
            "http://localhost:4200/*"
        };

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // hier wird ein Singleton verwendet, um die Instanz beizubehalten
            // die Daten aus dem Speicher gehen nach dem Http-Request nicht verloren
            // Mehr zu den Lifetimes hier: https://medium.com/@bhavana.vanjani/dependency-injection-lifetimes-in-asp-net-core-transient-singleton-scoped-8ade37d8b742
            services.AddSingleton<IMitarbeiterManager, MitarbeiterManager>();
            // services.AddTransient<IMitarbeiterManager, MitarbeiterManager>();

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                                  builder =>
                                  {
                                      builder.WithOrigins(allowedOrigins.ToArray())
                                                          .AllowAnyHeader()
                                                          .AllowAnyMethod();
                                  });
            });

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Ak.Template.WebApi", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Ak.Template.WebApi v1"));
            }

            app.UseRouting();

            app.UseCors();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
