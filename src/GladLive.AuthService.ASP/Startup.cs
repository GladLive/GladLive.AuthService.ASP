using GladLive.Security.Common;
using GladLive.Web.Payloads.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace GladLive.AuthService.ASP
{
	public class Startup
	{
		public void ConfigureServices(IServiceCollection services)
		{
			//This adds the MVC core features
			services.AddMvcCore();
			services.AddLogging();

			//We only have a protobuf-net Web API for authentication right now

			//This is required due to fault in ASP involving model validation with IPAddress
			//Reference: https://github.com/aspnet/Mvc/issues/4571 for more information
			/*services.Configure<MvcOptions>(c =>
			{
				c.ValueProviderFactories.Add(new DefaultTypeBasedExcludeFilter(typeof(IPAddress)));
			});*/

			services.AddEntityFrameworkSqlServer()
				.AddDbContext<AccountDbContext>(option =>
				{
					option.UseSqlServer(@"Server=Glader-PC;Database=ASPTEST;Trusted_Connection=True;");
				});

			services.AddTransient<AccountDbContext, AccountDbContext>();

			//Repository service for account access
			services.AddTransient<IAccountRepository, AccountRepository>();

			//Authentication services that deals with auth, decryption and etc
			services.AddSingleton<IAuthService, AuthenticationService>();

			services.AddSingleton<ICryptoService, RSACryptoProviderAdapter>(x =>
				{
					return new RSACryptoProviderAdapter(new RSACryptoServiceProvider());
				});

			//Add BCrypt hashing service for hash verification
			services.AddSingleton<IHashingService, BCryptHashingService>();
		}

		public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
		{
			//Comment this out for IIS but we shouldn't need it. We'll be running this on
			//Linux behind AWS Elastic Load Balancer probably
			//app.UseIISPlatformHandler();

			loggerFactory.AddConsole(LogLevel.Information);
			app.UseMvc();

			//We have to register the payload types
			//We could maybe do some static analysis to find referenced payloads and auto generate this code
			//or find them at runtime but for now this is ok
			new GladNet.Serializer.Protobuf.ProtobufnetRegistry().Register(typeof(AuthRequest));
			new GladNet.Serializer.Protobuf.ProtobufnetRegistry().Register(typeof(AuthResponse));
		}

		//This changed in RTM. Fluently build and setup the web hosting
		public static void Main(string[] args) => new WebHostBuilder()
			.UseKestrel()
			.UseContentRoot(Directory.GetCurrentDirectory())
			.UseStartup<Startup>()
			.Build()
			.Run();
	}
}
