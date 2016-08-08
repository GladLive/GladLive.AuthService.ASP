using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GladLive.AuthService.ASP
{
	/// <summary>
	/// Concrete repository for <see cref="Account"/> / <see cref="AccountDbContext"/>.
	/// </summary>
	public class AccountRepository : Repository<AccountDbContext>, IAccountRepository
	{
		public AccountRepository(AccountDbContext context) 
			: base(context)
		{

		}

		/// <summary>
		/// Queries for the existence of the model by the id.
		/// </summary>
		/// <param name="id">Model id.</param>
		/// <returns>True if the model exists or false otherwise.</returns>
		public bool Exists(int id)
		{
			//Indicates if at least one account exists with that id (should be unique though)
			return databaseContext.Accounts
				.Where(x => x.AccountID == id)
				.Count() != 0;
		}

		/// <summary>
		/// Async queries for the existence of the model by the id.
		/// </summary>
		/// <param name="id">Model id.</param>
		/// <returns>A future bool of True if the model exists or false otherwise.</returns>
		public async Task<bool> ExistsAsync(int id)
		{
			//Async: indicates if at least one account exists with that id (should be unique though)
			IEnumerable<Account> accounts = await databaseContext.Accounts.ToAsyncEnumerable()
				.Where(ax => ax.AccountID == id).ToList();

			return accounts.Count() != 0;
		}

		/// <summary>
		/// Queries for the model instance by the id.
		/// </summary>
		/// <param name="id">Model id.</param>
		/// <returns>The model instance or null if not found.</returns>
		public Account Get(int id)
		{
			return databaseContext.Accounts
				.FirstOrDefault(x => x.AccountID == id);
		}

		/// <summary>
		/// Queries for a non-lazy collection of all <see cref="Account"/> models.
		/// </summary>
		/// <returns>A non-lazy non-null collection of all <see cref="Account"/>s.</returns>
		public IEnumerable<Account> GetAll()
		{
			return databaseContext.Accounts;
		}

		/// <summary>
		/// Async queries for a non-lazy collection of all <see cref="Account"/> models.
		/// </summary>
		/// <returns>A non-lazy non-null future collection of all <see cref="Account"/> s.</returns>
		public async Task<IEnumerable<Account>> GetAllAsync()
		{
			return await databaseContext.Accounts.ToAsyncEnumerable().ToList();
		}

		/// <summary>
		/// Async queries for the model instance by the id.
		/// </summary>
		/// <param name="id">Model id.</param>
		/// <returns>A future model instance or null if not found.</returns>
		public async Task<Account> GetAsync(int id)
		{
			return await databaseContext.Accounts.ToAsyncEnumerable()
				.FirstOrDefault();
		}

		/// <summary>
		/// Queries for the account by the <see cref="string"/>
		/// name of the account.
		/// </summary>
		/// <param name="accountName">Account name of the account.</param>
		/// <returns>The account with the matching <paramref name="accountName"/> or null.</returns>
		public Account GetByAccountName(string accountName)
		{
			return databaseContext.Accounts
				.Where(a => a.AccountName == accountName)
				.FirstOrDefault();
		}

		/// <summary>
		/// Asyncronously queries for the account by the <see cref="string"/>
		/// name of the account.
		/// </summary>
		/// <param name="accountName">Account name of the account.</param>
		/// <returns>A future of the account with the matching <paramref name="accountName"/> or null.</returns>
		public async Task<Account> GetByAccountNameAsync(string accountName)
		{
			return await databaseContext.Accounts
				.ToAsyncEnumerable()
				.Where(a => a.AccountName == accountName)
				.FirstOrDefault();
		}
	}
}
