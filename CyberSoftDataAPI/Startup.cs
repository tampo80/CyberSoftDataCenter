using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;
using CyberSoftDataCenter.Data;
using System.Net;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using CyberSoftDataAPI.Utils;

namespace CyberSoftDataAPI
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddDbContext<CdataCenterDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddMvc().AddJsonOptions(options => {
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            }); ;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseMvc();
            app.UseExceptionHandler(
               options =>
               {
                   options.Run(
                              async mycontext =>
                              {
                                  mycontext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                                  mycontext.Response.ContentType = "text/html";
                                  var ex = mycontext.Features.Get<IExceptionHandlerFeature>();
                                  if (ex != null)
                                  {
                                      Routine.LogFile(ex.Error.Message, ex.Error.StackTrace, ex.Error.Source, Routine.ErreurLine(ex.Error), ex.Error.TargetSite.Name);
                                      var err = $"<h1>Error: {ex.Error.Message}</h1>{ex.Error.StackTrace }";
                                      await mycontext.Response.WriteAsync(err).ConfigureAwait(false);

                                  }
                              });
               }
               );
        }
    }
}
