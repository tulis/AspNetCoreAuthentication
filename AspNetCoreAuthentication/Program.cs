using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace AspNetCoreAuthentication
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            if(args.Any())
            {
                switch(args[0])
                {
                    case "AwsCognito":
                        return WebHost.CreateDefaultBuilder(args)
                            .UseStartup<StartupAwsCognito>();
                    default:
                        throw new NotSupportedException(
                            $"Authentication through [{args[0]}] is not supported.");
                }

            }
            else
            {
                return WebHost.CreateDefaultBuilder(args)
                    .UseStartup<StartupAwsCognito>();
            }
        }
    }
}
