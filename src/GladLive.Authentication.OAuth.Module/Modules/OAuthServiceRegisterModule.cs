using System;
using System.Security.Cryptography.X509Certificates;
using AspNet.Security.OpenIdConnect.Primitives;
using GladLive.AuthService.ASP;
using GladLive.Module.System.Library;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using OpenIddict.Core;
using OpenIddict.Models;
using OpenIddict.EntityFrameworkCore;

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
			serviceCollection.AddAuthentication();
	
			//Below is the OpenIddict registration
			//This is the recommended setup from the official Github: https://github.com/openiddict/openiddict-core
			serviceCollection.AddIdentity<GladLiveApplicationUser, GladLiveApplicationRole>()
				.AddEntityFrameworkStores<GladLiveApplicationDbContext, int>()
				//.AddUserManager<GladLiveOpenIddictManager>()
				.AddDefaultTokenProviders();

			serviceCollection.AddDbContext<GladLiveApplicationDbContext>(options =>
			{
				DbOptions(options);
				options.UseOpenIddict<int>();
			});

			serviceCollection.AddOpenIddict<int>(options =>
			{
				// Register the Entity Framework stores.
				options.AddEntityFrameworkCoreStores<GladLiveApplicationDbContext>();

				// Register the ASP.NET Core MVC binder used by OpenIddict.
				// Note: if you don't call this method, you won't be able to
				// bind OpenIdConnectRequest or OpenIdConnectResponse parameters.
				options.AddMvcBinders();

				options.EnableTokenEndpoint($"/api/AuthenticationRequest"); // Enable the token endpoint (required to use the password flow).
				options.AllowPasswordFlow(); // Allow client applications to use the grant_type=password flow.
				options.AllowRefreshTokenFlow();
				options.UseJsonWebTokens();
				options.AddSigningCertificate(new X509Certificate2($@"Certs/JWTCert.pfx"));
			});

			// Configure Identity to use the same JWT claims as OpenIddict instead
			// of the legacy WS-Federation claims it uses by default (ClaimTypes),
			// which saves you from doing the mapping in your authorization controller.
			serviceCollection.Configure<IdentityOptions>(options =>
			{
				options.ClaimsIdentity.UserNameClaimType = OpenIdConnectConstants.Claims.Name;
				options.ClaimsIdentity.UserIdClaimType = OpenIdConnectConstants.Claims.Subject;
				options.ClaimsIdentity.RoleClaimType = OpenIdConnectConstants.Claims.Role;
			});
		}
	}
}
