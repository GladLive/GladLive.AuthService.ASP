using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Http;
using Microsoft.Extensions.DependencyInjection;

namespace GladLive.AuthService.ASP
{
	public class Startup
	{
		public void ConfigureServices(IServiceCollection services)
		{
			//This adds the MVC core features
			services.AddMvcCore()
				.AddProtobufNetFormatters(); //add custom ProtobufNet formatters
		}

		public void Configure(IApplicationBuilder app)
		{
			//Comment this out for IIS but we shouldn't need it. We'll be running this on
			//Linux behind AWS Elastic Load Balancer probably
			//app.UseIISPlatformHandler();

			//We only need default routing right now.
			//We only have a protobuf-net Web API for authentication right now
			app.UseMvcWithDefaultRoute();
		}

		public static void Main(string[] args) => WebApplication.Run<Startup>(args);
	}
}
