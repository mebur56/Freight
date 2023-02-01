using Domain.Interfaces;
using Domain.Entities;
using Infra.Context;
using Infra.Repository;
using Service.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.Extensions.Hosting.Internal;
using Newtonsoft.Json;
using DocumentFormat.OpenXml.Drawing;

namespace Application
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddDbContext<MySqlContext>(options =>
            {
                var server = Configuration["database:mysql:server"];
                var port = Configuration["database:mysql:port"];
                var database = Configuration["database:mysql:database"];
                var username = Configuration["database:mysql:username"];
                var password = Configuration["database:mysql:password"];
                string connectionString = $"Server={server};Port={port};Database={database};Uid={username};Pwd={password}";

                options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
            });

            services.AddScoped<IBaseRepository<Freight>, BaseRepository<Freight>>();
            services.AddScoped<IBaseRepository<FreightPrice>, BaseRepository<FreightPrice>>();
            services.AddScoped<IBaseService<Freight>, BaseService<Freight>>();
            services.AddScoped<IBaseService<FreightPrice>, BaseService<FreightPrice>>();
            services.AddScoped<IFreightService<Freight>, FreightService<Freight>>();
            services.AddScoped<IFreightPriceService<FreightPrice>, FreightPriceService<FreightPrice>>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }


            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }

}