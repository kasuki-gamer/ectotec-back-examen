using ectotec.AccesoDatos.Data;
using ectotec.Middleware;
using Edi.Captcha;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ectotec
{
    public class Startup
    {
        readonly string myAllowSites = "_myAllowSites";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));

            services.AddDepencency();

            services.AddSessionBasedCaptcha(option =>
            {
                option.Letters = "ABCDEFGHJKLMNPRTUVWXYZ";
                option.SessionName = "CaptchaCode";
                option.CodeLength = 4;
            });

            services.AddHttpContextAccessor();

            services.AddMvc().AddSessionStateTempDataProvider();
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(20);
            });

            services.AddCors(options =>
            {
                options.AddPolicy(name: myAllowSites,
                                  builder =>
                                  {
                                      builder.WithOrigins("http://localhost:4200", "http://127.0.0.1:4200")
                                      //builder.WithOrigins()
                                      .AllowAnyMethod()
                                      .AllowAnyHeader();
                                  });
            });

            services.AddHttpClient();

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSession();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(myAllowSites);

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
