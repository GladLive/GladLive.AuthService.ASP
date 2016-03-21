using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GladLive.Web.Payloads.Authentication;
using GladLive.Security.Common;

namespace GladLive.AuthService.ASP
{
	public class AuthenticationService : IAuthService
	{
		private IAccountRepository accountRepository { get; }

		private ICryptoService decryptoService { get; }

		private IHashingService hashingService { get; }

		public AuthenticationService(IAccountRepository accountRepo, ICryptoService cryptoService, IHashingService hashService)
		{
			accountRepository = accountRepo;
			decryptoService = cryptoService;
			hashingService = hashService;
		}

		public AuthResponseCode TryAuthenticate(string userName, byte[] encryptedPassword)
		{
			return CheckRequestAgainstAccount(accountRepository.GetByAccountName(userName), encryptedPassword);
		}

		private AuthResponseCode CheckRequestAgainstAccount(Account account, byte[] encryptedPassword)
		{
			if (account == null)
				return AuthResponseCode.AccountDoesntExist;

			string decryptedPassword = decryptoService.DecryptToString(encryptedPassword);

			if (String.IsNullOrEmpty(decryptedPassword))
				return AuthResponseCode.AccountDoesntExist;

			//Don't do Task.Run/Factory.StartNew threading to get async in ASP.Net. Read this: http://blog.stephencleary.com/2013/11/taskrun-etiquette-examples-dont-use.html
			return hashingService.isHashValuesEqual(decryptedPassword, account.PasswordHash) ? AuthResponseCode.Success : AuthResponseCode.AccountDoesntExist;
		}

		public async Task<AuthResponseCode> TryAuthenticateAsync(string userName, byte[] encryptedPassword)
		{
			//Don't do Task.Run/Factory.StartNew threading to get async in ASP.Net. Read this: http://blog.stephencleary.com/2013/11/taskrun-etiquette-examples-dont-use.html
			//Just call the syncronous

			//Try to query the DB for the account and yield execution till the query completes
			return CheckRequestAgainstAccount(await accountRepository.GetByAccountNameAsync(userName), encryptedPassword);
			
		}
	}
}
