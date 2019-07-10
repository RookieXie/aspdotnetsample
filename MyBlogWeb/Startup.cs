using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MyBlog.Core;
using MyBlog.DBContext;
using MyBlog.Models;
using MyBlog.Services;
using NLog.Extensions.Logging;
using NLog.Web;
using Swashbuckle.AspNetCore.Swagger;

namespace MyBlogWeb
{
    public class Startup
    {
        private MapperConfiguration _mapperConfiguration { get; set; }
        private const string V = "v1";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddDbContext<EFContext>(options =>
             options.UseMySql(Configuration.GetConnectionString("MySQLConnection")));
            //添加Dapper支持
            services.AddScoped(opt => new DapperContext(Configuration.GetConnectionString("MySQLConnection")));
            //services.AddScoped(option => new RedisCore(Configuration.GetValue<string>("RedisConnection"), Configuration.GetValue("RedisIndex", 0)));

            //AutoMapper
            _mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MyAMProfile());
            });
            services.AddSingleton<IMapper>(sp => _mapperConfiguration.CreateMapper());

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "My API", Version = V });

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
            //services.AddScoped<ITestApiService, TestApiService>();
            services.AddAutoScope();



            services.AddTransient<IOperationTransient, MyOperation>();
            services.AddScoped<IOperationScoped, MyOperation>();
            services.AddSingleton<IOperationSingleton, MyOperation>();
            services.AddSingleton<IOperationSingletonInstance>(new MyOperation(Guid.Empty));

            // OperationService depends on each of the other Operation types.
            services.AddTransient<OperationService, OperationService>();
            //跨域
            services.AddCors(option =>
            {
                option.AddPolicy(MyAllowSpecificOrigins,
                    build =>
                    {
                        build.WithOrigins("*");
                    });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            loggerFactory.AddNLog();
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                env.ConfigureNLog("NLogLinux.config");
            }
            else
            {
                env.ConfigureNLog("NLogWindows.config");
            }

            //Cors
            app.UseCors(MyAllowSpecificOrigins);
           
            app.UseSwagger();
            app.UseSwaggerUI(c=> {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });
            //app.Use(async (context, next) =>
            //{
            //    await context.Response.WriteAsync("test 1 start \n");
            //    await next.Invoke();
            //    await context.Response.WriteAsync("test 1 end");
            //});
            //app.Run(async context =>
            //{
            //    await context.Response.WriteAsync("test 2 \n");
            //});
            //Directory.GetCurrentDirectory() I:\GitHub\MyBlog\MyBlogWeb
            app.UseRequestLog();

            //app.UseMvc();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

        }
    }
}
