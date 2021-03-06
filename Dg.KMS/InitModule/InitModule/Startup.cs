using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace InitModule
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
            ////注册数据库Gwglxs
            ///https://www.cnblogs.com/qingzhen/p/11712457.html
            //services.AddDbContext<GwglxsDbContext>(options =>
            //            options.UseSqlServer(
            //                Configuration.GetConnectionString("GwglxsConnection")
            //            )//.UseLazyLoadingProxies()//懒加载,需要加载包：Microsoft.EntityFrameworkCore.Proxies
            //        );
            services.AddAutofac(container =>
            {
                container.RegisterType<MyClass>().SingleInstance();
            });
            services.AddControllers();
        }
        //public void ConfigureContainer(ContainerBuilder builder)
        //{
        //    builder.RegisterType<MyClass>().SingleInstance();
        //}

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }


        //public IServiceProvider ConfigureServices(IServiceCollection services)
        //{
        //    services.AddApplicationInsightsTelemetry(Configuration);
        //    services.AddMvc();
        //    return RegisterAutofac(services);
        //}


        //private IServiceProvider RegisterAutofac(IServiceCollection services)
        //{
        //    var builder = new ContainerBuilder();
        //    builder.Populate(services);
        //    var assembly = this.GetType().GetTypeInfo().Assembly;
        //    builder.RegisterType<AopInterceptor>();
        //    builder.RegisterAssemblyTypes(assembly)
        //    .Where(type =>
        //    typeof(IDependency).IsAssignableFrom(type) && !type.GetTypeInfo().IsAbstract)
        //    .AsImplementedInterfaces()
        //    .InstancePerLifetimeScope().EnableInterfaceInterceptors().InterceptedBy(typeof(AopInterceptor));
        //    this.ApplicationContainer = builder.Build();
        //    return new AutofacServiceProvider(this.ApplicationContainer);
        //}


    }
}
