using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using RestAspNet5.Model.Context;
using RestAspNet5.Business.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestAspNet5.Repository.Implementations;
using Serilog;
using RestAspNet5.Repository.Generic;
using RestAspNet5.Hypermedia.Filters;
using RestAspNet5.Hypermedia.Enricher;
using Microsoft.AspNetCore.Rewrite;

namespace RestAspNet5
{
    public class Startup
    {
        public IWebHostEnvironment Environment { get; }
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;

            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .CreateLogger();
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {


            var connection = Configuration["MySqlConection:MySqlConectionString"];
            services.AddDbContext<MySqlContext>(options => options.UseMySql(connection));



            if (Environment.IsDevelopment())
            {
                MigrateDataBase(connection);
            }

            var filterOptions = new HyperMediaFilterOptions();
            filterOptions.ContentResponseEnricherList.Add(new PersonEnricher());
            filterOptions.ContentResponseEnricherList.Add(new BookEnricher());
            services.AddSingleton(filterOptions);



            services.AddApiVersioning();

            //injeção de dependencia!
            services.AddScoped<IPersonBusiness, PersonBusinessImplementation>();

            services.AddScoped<IBookBusiness, BookBusinessImplementation>();

            services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));


            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1",
                    new OpenApiInfo
                    { 
                        Title = "REST API's From 0 to Azure With ASP.NET Core 5 and Docker", 
                        Version = "v1", 
                        Description = "API RESTful developed in course 'REST API's From 0 to Azure With ASP.NET Core 5 and Docker'",
                        Contact = new OpenApiContact
                        {
                            Name = "Thiago Teixeira",
                            Url = new Uri ("https://Thiagoteixeira.net")
                        }
                    });
            });
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json",
                                                        "REST API's From 0 to Azure With ASP.NET Core 5 and Docker - v1"));
            }


            var option = new RewriteOptions();
            option.AddRedirect("^$", "swagger");
            app.UseRewriter(option);

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapControllerRoute("DefaultApi", "{controller=values}/{id?}");

            });
        }

        private void MigrateDataBase(string connection)
        {

            try
            {
                var evolveConection = new MySql.Data.MySqlClient.MySqlConnection(connection);
                var envolve = new Evolve.Evolve(evolveConection, msg => Log.Information(msg))
                {
                    Locations = new List<string> { "db/Migrations", "db/dataset" },
                    IsEraseDisabled = true,
                };
                envolve.Migrate();


            }
            catch (Exception ex)
            {
                Log.Error("Migration Failed " + ex);
                throw;
            }
        }

    }
}
