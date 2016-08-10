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
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.AspNetCore.Identity;
using OpenIddict;

namespace GladLive.AuthService.ASP
{
	public class Startup
	{
		public void ConfigureServices(IServiceCollection services)
		{
			//This adds the MVC core features and GladNet features
			services.AddLogging();
			services.AddMvc();

			services.AddEntityFrameworkSqlServer()
				.AddDbContext<GladLiveApplicationDbContext>(option =>
				{
					option.UseSqlServer(@"Server=Glader-PC;Database=ASPTEST;Trusted_Connection=True;");
					//option.UseMemoryCache(new MemoryCache(new MemoryCacheOptions()));
				});

			// Register the OpenIddict services, including the default Entity Framework stores.
			services.AddOpenIddict<GladLiveApplicationUser, IdentityRole, GladLiveApplicationDbContext>();

			//Below is the OpenIddict registration
			//This is the recommended setup from the official Github: https://github.com/openiddict/openiddict-core
			services.AddIdentity<GladLiveApplicationUser, IdentityRole>()
				.AddEntityFrameworkStores<GladLiveApplicationDbContext>()
				.AddUserManager<GladLiveOpenIddictManager>()
				.AddDefaultTokenProviders();

			services.AddOpenIddict<GladLiveApplicationUser, GladLiveApplicationDbContext>()
				.EnableTokenEndpoint($"/api/AuthenticationRequest") // Enable the token endpoint (required to use the password flow).
				.AllowPasswordFlow() // Allow client applications to use the grant_type=password flow.
				.AllowRefreshTokenFlow()
				.UseJsonWebTokens()

#if DEBUG || DEV
				.DisableHttpsRequirement()
				.AddEphemeralSigningKey();
#else
			;
#endif			
			services.AddTransient<GladLiveApplicationDbContext, GladLiveApplicationDbContext>();

			services.AddScoped<OpenIddictUserManager<GladLiveApplicationUser>, GladLiveOpenIddictManager>();
		}

		public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
		{
			//Comment this out for IIS but we shouldn't need it. We'll be running this on
			//Linux behind AWS Elastic Load Balancer probably
			//app.UseIISPlatformHandler();

			loggerFactory.AddConsole(LogLevel.Information);


			//This JWT example indicates we shouldn't use Identity: https://github.com/capesean/openiddict-test/blob/master/src/openiddict-test/Startup.cs
			//app.UseIdentity();
			//app.UseOAuthValidation();
			app.UseOpenIddict();

			// use jwt bearer authentication
			app.UseJwtBearerAuthentication(new JwtBearerOptions
			{
				AutomaticAuthenticate = true,
				AutomaticChallenge = true,
				RequireHttpsMetadata = false,
				Audience = @"http://localhost:5000/",
				Authority = @"http://localhost:5000/"
			});

			app.UseMvc();

			//We have to register the payload types
			//We could maybe do some static analysis to find referenced payloads and auto generate this code
			//or find them at runtime but for now this is ok
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
