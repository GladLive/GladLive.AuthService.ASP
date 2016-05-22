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
		/// <summary>
		/// Tries to authenticate a <paramref name="userName"/> <paramref name="encryptedPassword"/> pair against the service
		/// </summary>
		/// <param name="userName">Username of the pair to authenticate with.</param>
		/// <param name="encryptedPassword">Encrypted password for the pair.</param>
		/// <returns>Success if authenticated or various other codes describing failure.</returns>
		AuthResponseCode TryAuthenticate(string userName, byte[] encryptedPassword);

		/// <summary>
		/// Asyncronously Tries to authenticate a <paramref name="userName"/> <paramref name="encryptedPassword"/> pair against the service
		/// </summary>
		/// <param name="userName">Username of the pair to authenticate with.</param>
		/// <param name="encryptedPassword">Encrypted password for the pair.</param>
		/// <returns>Success if authenticated or various other codes describing failure.</returns>
		Task<AuthResponseCode> TryAuthenticateAsync(string userName, byte[] encryptedPassword);
	}
}
