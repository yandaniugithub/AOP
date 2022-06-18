using Autofac;
using Autofac.Extras.DynamicProxy;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Yak.AOP.WebApi
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

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Yak.AOP.WebApi", Version = "v1" });
            });
        }
        public void ConfigureContainer(ContainerBuilder builder)
        {
            //注册用户维护业务层
            var basePath = AppContext.BaseDirectory;
            var serviceDll = Path.Combine(basePath, "Yak.AOP.Service.dll");

            if (!File.Exists(serviceDll))
            {
                throw new Exception("找不到程序集");
            }
            //注册AOP拦截器
            builder.RegisterType(typeof(UserAop));
            builder.RegisterAssemblyTypes(Assembly.LoadFrom(serviceDll))
                .AsImplementedInterfaces()
                .EnableInterfaceInterceptors()//开启切面，需要引入Autofac.Extras.DynamicProxy
                .InterceptedBy(typeof(UserAop));//指定拦截器，可以指定多个
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Yak.AOP.WebApi v1"));
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
