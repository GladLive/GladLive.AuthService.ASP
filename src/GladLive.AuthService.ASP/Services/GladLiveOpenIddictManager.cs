using OpenIddict;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using AspNet.Security.OpenIdConnect.Extensions;

namespace GladLive.AuthService.ASP
{
	public class GladLiveOpenIddictManager : OpenIddictUserManager<GladLiveApplicationUser>
	{
		public GladLiveOpenIddictManager(IServiceProvider services, IOpenIddictUserStore<GladLiveApplicationUser> store, IOptions<IdentityOptions> options, ILogger<OpenIddictUserManager<GladLiveApplicationUser>> logger, IPasswordHasher<GladLiveApplicationUser> hasher, IEnumerable<IUserValidator<GladLiveApplicationUser>> userValidators, IEnumerable<IPasswordValidator<GladLiveApplicationUser>> passwordValidators, ILookupNormalizer keyNormalizer, IdentityErrorDescriber errors) 
			: base(services, store, options, logger, hasher, userValidators, passwordValidators, keyNormalizer, errors)
		{

		}

		//This lets us modify the token to include an identity
		public override async Task<ClaimsIdentity> CreateIdentityAsync(GladLiveApplicationUser user, IEnumerable<string> scopes)
		{
			var claimsIdentity = await base.CreateIdentityAsync(user, scopes);

			claimsIdentity.AddClaim("user_name", user.UserName,
				OpenIdConnectConstants.Destinations.AccessToken,
				OpenIdConnectConstants.Destinations.IdentityToken);

			return claimsIdentity;
		}
	}
}
