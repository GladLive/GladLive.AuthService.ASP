using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GladLive.AuthService.ASP
{
	/// <summary>
	/// Repository service for <see cref="Account"/> model objects.
	/// </summary>
	public interface IAccountRepository : IRepository<Account>, IRepositoryAsync<Account>
	{
		/// <summary>
		/// Queries for the account by the <see cref="string"/>
		/// name of the account.
		/// </summary>
		/// <param name="accountName">Account name of the account.</param>
		/// <returns>The account with the matching <paramref name="accountName"/> or null.</returns>
		Account GetByAccountName(string accountName);

		/// <summary>
		/// Asyncronously queries for the account by the <see cref="string"/>
		/// name of the account.
		/// </summary>
		/// <param name="accountName">Account name of the account.</param>
		/// <returns>A future of the account with the matching <paramref name="accountName"/> or null.</returns>
		Task<Account> GetByAccountNameAsync(string accountName);
	}
}
