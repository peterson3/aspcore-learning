using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace testDotNetCOre
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IGreeter,Greeter>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app,
                              IHostingEnvironment env,
                              IGreeter greeter,
                              ILogger<Startup> logger)
        {
            //Middlewares
            app.Use(next => {
                return async context =>{
                    logger.LogInformation("Request Incoming");
                    if (context.Request.Path.StartsWithSegments("/mymiddle")){
                        await context.Response.WriteAsync("Hit!");
                        logger.LogInformation("Request Handled");
                    }
                    else{
                        await next(context);
                        logger.LogInformation("Request Outgoing");
                    }
                };
            });

            app.UseWelcomePage(new WelcomePageOptions{
               Path = "/welcome" 
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.Run(async (context) =>
            {
                var greetingMessage = greeter.GetMessageOfTheDay();
                await context.Response.WriteAsync(greetingMessage);
            });
        }
    }
}
