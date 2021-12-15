using System;
using ECommerce.DataAccess;
using ECommerce.DTO.Response;
using EcommerceAPI.DTO.Response;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using ECommerce.DataAccess.IRepositories;
using ECommerce.DataAccess.Repositories;
using ECommerce.Services.Implementations;
using ECommerce.Services.Interfaces;
using EcommerceAPI.Entities;
using ECommerceAPI.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace EcommerceAPI
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<ECommerceDbContext>();

            #region TransientRepository
            services.AddTransient<ICategoryRepository, CategoryRepository>();
            //services.AddTransient<IProductRepository, ProductoRepository>();
            services.AddScoped<IProductRepository, ProductoRepository>();
            services.AddTransient<ICustomerRepository, CustomerRepository>();
            #endregion

            #region MyRegion
            services.AddTransient<ICategoryServices, CategoryService>();
            services.AddTransient<IProductServices, ProductService>();
            services.AddTransient<ICustomerServices, CustomerService>();
            #endregion


            services.AddControllers();//1 añadir controles

            //services.AddSingleton(new ProductDTOColletionResponse
            //{
            //    Colletion = new List<ProductDTO>()
            //});

            services.AddDbContext<ECommerceDbContext>(options =>
            {
                //options.UseSqlServer("Data Source=10.10.2.26;Initial Catalog=EcommerceDB;User Id=lss;Password=lss;Persist Security Info = True");
                //options.UseSqlServer(@"Data Source=DBA\MSSQLSERVER2019;Initial Catalog=EcommerceDB;User Id=lss;Password=lss;Persist Security Info = True");
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
                options.LogTo(Console.WriteLine, LogLevel.Information).EnableSensitiveDataLogging();
            });

            services.AddAutoMapper(options =>
            {
                options.AddMaps(typeof(Category));
                options.AddMaps(typeof(Product));
                options.AddMaps(typeof(Customer));
            });

            services.AddSwaggerGen(s =>
            {
                s.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "ECommerce API",
                    Version = "v1",
                    Description = "API para el curso FullStack"
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
                app.UseSwaggerUI(s => s.SwaggerEndpoint("/swagger/v1/swagger.json",
                    "ECommerceApi v1"));
            }
            else
            {
                app.UseSwagger();
                app.UseSwaggerUI(s => s.SwaggerEndpoint("../swagger/v1/swagger.json",
                    "ECommerceApi v1"));
            }

            app.UseRouting(); //hace el llamado al Atributo de [Route]

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers(); //2 mapear los controles
            });
        }
    }
}
