using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StatlerWaldorfCorp.LocationService.DatabaseInfrastructure;
using StatlerWaldorfCorp.LocationService.Persistence;
using StatlerWaldorfCorp.LocationService.Repository;

namespace StatlerWaldorfCorp.LocationService
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            // var connectionString = Configuration.GetConnectionString("Mysql");
            services.AddEntityFrameworkMySql().AddDbContext<LocationDbContext>(option=>
            option.UseMySql("Host=localhost;Port=3306;Database=locationservice;Username=root;Password=inteword"));
            services.AddScoped<ILocationRecordRepository, LocationRecordRepository>();
            services.AddMvc();
        }

        public static IConfigurationRoot Configuration { get; set; }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseMvc();
        }
    }
}
