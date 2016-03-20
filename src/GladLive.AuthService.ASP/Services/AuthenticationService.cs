using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GladLive.Web.Payloads.Authentication;

namespace GladLive.AuthService.ASP
{
	public class AuthenticationService : IAuthService
	{
		private IAccountRepository accountRepository { get; }

		public AuthenticationService(IAccountRepository accountRepo)
		{
			accountRepository = accountRepo;
		}

		public AuthResponseCode TryAuthenticate(string userName, byte[] encryptedPassword)
		{
			throw new NotImplementedException();
		}

		public Task<AuthResponseCode> TryAuthenticateAsync(string userName, byte[] encryptedPassword)
		{
			throw new NotImplementedException();
		}
	}
}
