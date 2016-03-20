using GladLive.Web.Payloads.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GladLive.AuthService.ASP
{
	/// <summary>
	/// Service for authenticating a username and password pair
	/// </summary>
	public interface IAuthService
	{
		AuthResponseCode TryAuthenticate(string userName, byte[] encryptedPassword);

		Task<AuthResponseCode> TryAuthenticateAsync(string userName, byte[] encryptedPassword);
	}
}
