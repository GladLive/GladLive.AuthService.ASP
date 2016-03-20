using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Data.Entity;

namespace GladLive.AuthService.ASP
{
	public class Startup
	{
		public void ConfigureServices(IServiceCollection services)
		{
			//This adds the MVC core features
			services.AddMvcCore()
				.AddProtobufNetFormatters(); //add custom ProtobufNet formatters

			services.AddEntityFramework()
				.AddSqlServer()
				.AddDbContext<AccountDbContext>(option =>
				{
					option.UseSqlServer(@"Server=Glader-PC;Database=ASPTEST;Trusted_Connection=True;");
				});

			services.AddTransient<AccountDbContext, AccountDbContext>();

			//Repository service for account access
			services.AddTransient<IAccountRepository, AccountRepository>();

			//Authentication services that deals with auth, decryption and etc
			services.AddSingleton<IAuthService, AuthenticationService>();
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
