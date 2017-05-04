using GladLive.Module.System.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Authentication;

namespace GladLive.Authentication.JWT.Module.Modules
{
	public class JWTApplicationModule : ApplicationConfigurationModule
	{
		public JWTApplicationModule(IApplicationBuilder app, ILoggerFactory loggerFactory)
			: base(app, loggerFactory)
		{

		}

		public override void Register()
		{
			JwtBearerOptions bearerOptions = new JwtBearerOptions
			{
				AutomaticAuthenticate = true,
				AutomaticChallenge = true,
				RequireHttpsMetadata = true,
				TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
				{
					IssuerSigningKey = new X509SecurityKey(new X509Certificate2(@"Certs/JWTCert.pfx")),
					ValidateIssuerSigningKey = false, //WARNING: This is bad. We should validate the signing key in the future
					ValidateAudience = false,
					ValidateIssuer = false,
					ValidateLifetime = false, //temporary until we come up with a solution
					NameClaimType = "name", //we use the name identifier since we don't care about the literal username; just a GUID/UID/ID
				},
			};

			// use jwt bearer authentication
			applicationBuilder.UseJwtBearerAuthentication(bearerOptions);
		}
	}
}
