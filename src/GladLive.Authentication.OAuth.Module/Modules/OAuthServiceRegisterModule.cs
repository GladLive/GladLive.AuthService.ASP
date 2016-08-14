using GladLive.Module.System.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using GladLive.AuthService.ASP;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using System.Security.Cryptography.X509Certificates;
using OpenIddict;

namespace GladLive.Authentication.OAuth.Module
{
	public class OAuthServiceRegisterModule : ServiceRegistrationModule
	{
		public OAuthServiceRegisterModule(IServiceCollection serviceCollection, Action<DbContextOptionsBuilder> options = null) 
			: base(serviceCollection, options)
		{

		}

		public override void Register()
		{
			serviceCollection.AddEntityFrameworkSqlServer()
				.AddDbContext<GladLiveApplicationDbContext>(this.DbOptions);

			//Below is the OpenIddict registration
			//This is the recommended setup from the official Github: https://github.com/openiddict/openiddict-core
			serviceCollection.AddIdentity<GladLiveApplicationUser, IdentityRole>()
				.AddEntityFrameworkStores<GladLiveApplicationDbContext>()
				.AddUserManager<GladLiveOpenIddictManager>()
				.AddDefaultTokenProviders();

			serviceCollection.AddOpenIddict<GladLiveApplicationUser, GladLiveApplicationDbContext>()
				.EnableTokenEndpoint($"/api/AuthenticationRequest") // Enable the token endpoint (required to use the password flow).
				.AllowPasswordFlow() // Allow client applications to use the grant_type=password flow.
				.AllowRefreshTokenFlow()
				.UseJsonWebTokens()
				.AddSigningCertificate(new X509Certificate2($@"Certs/JWTCert.pfx"));

			serviceCollection.AddScoped<OpenIddictUserManager<GladLiveApplicationUser>, GladLiveOpenIddictManager>();
		}
	}
}
