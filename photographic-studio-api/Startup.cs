using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using photographic_studio_api.Filters;

namespace photographic_studio_api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        readonly string Cors = "AnyOrigin";
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddMemoryCache();//加入缓存组件
            string constr = Configuration.GetConnectionString("MySqlConnection");
            services.AddDbContext<Context.photographicDbcontext>(options => options.UseMySql(constr));
            services.AddCors(options => {
                options.AddPolicy(Cors, builder => {
                    builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                });
            });
            services.AddMvc(options =>
            {
                options.Filters.Add<HttpGlobalAttribule>();
                options.Filters.Add<HttpGlobalExceptionFilter>(); //加入全局异常类
            });
            Utils.IOC.init(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //            app.UseHttpsRedirection().UseCors(builder =>
            //builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            app.UseCors(Cors);

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();
            Utils.ServiceLocator.Instance = app.ApplicationServices;
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });
            string uploadPath = $"{AppContext.BaseDirectory}/upload";
            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }
            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(AppContext.BaseDirectory, "upload")),
                RequestPath = new PathString("/upload")
            });
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
    }
}
