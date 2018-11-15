using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Protocols;
using Microsoft.IdentityModel.Logging;

namespace AspNetCoreAuthentication
{
    public class StartupAwsCognito
    {
        public StartupAwsCognito(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                // https://dzone.com/articles/identity-as-a-service-idaas-aws-cognito-and-aspnet
                //services.Configure<OpenIdConnectConfiguration>(configureOptions =>
                //{
                //    configureOptions.JwksUri = @"https://cognito-idp.ap-southeast-2.amazonaws.com/ap-southeast-2_liBGTVsZg/.well-known/jwks.json";
                //});

                options.RequireHttpsMetadata = true;
                options.MetadataAddress = @"https://cognito-idp.ap-southeast-2.amazonaws.com/ap-southeast-2_liBGTVsZg/.well-known/openid-configuration";
                options.Authority = "https://cognito-idp.ap-southeast-2.amazonaws.com/ap-southeast-2_liBGTVsZg";
                options.Audience = "54mt5ov8rl5i0c6nsneh5jjnio";

                options.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        //Log failed authentications
                        Console.WriteLine(context.Exception.Message);
                        return Task.CompletedTask;
                    },
                    OnTokenValidated = context =>
                    {
                        //Log successful authentications
                        Console.WriteLine(context.ToString());
                        return Task.CompletedTask;
                    }
                };
            });
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            //app.UseHttpsRedirection();
            //app.UseAuthentication();
            app.UseMvc();
        }
    }
}
