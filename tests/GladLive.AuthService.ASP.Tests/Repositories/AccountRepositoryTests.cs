using Microsoft.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace GladLive.AuthService.ASP.Tests
{
	public class AccountRepositoryTests
	{

		[Theory]
		[InlineData(1)]
		[InlineData(2)]
		[InlineData(3)]
		[InlineData(4)]
		public void Test_Repository_Indicates_AccountID_That_Should_Be_In_DB_Queries_To_Be(int id)
		{
			//arrange
			using (var context = InMemoryContext())
			{
				AccountRepository repo = new AccountRepository(context);

				//act
				bool result = repo.Exists(id);

				context.Database.EnsureDeleted();

				//assert
				Assert.True(result);
			}
		}

		[Theory]
		[InlineData(5)]
		[InlineData(6)]
		[InlineData(7)]
		[InlineData(8)]
		public void Test_Repository_Indicates_AccountID_That_Shouldnt_Be_In_DB_Queries_Not_To_Be(int id)
		{
			//arrange
			using (var context = InMemoryContext())
			{
				AccountRepository repo = new AccountRepository(context);

				//act
				bool result = repo.Exists(id);

				context.Database.EnsureDeleted();

				//assert
				Assert.False(result);
			}
		}

		[Theory]
		[InlineData("Test1")]
		public void Test_Repository_Indicates_AccountID_That_Should_Be_In_DB_Queries_To_Be(string name)
		{
			//arrange
			using (var context = InMemoryContext())
			{
				AccountRepository repo = new AccountRepository(context);

				//act
				Account account = repo.GetByAccountName(name);

				context.Database.EnsureDeleted();

				//assert
				Assert.NotNull(account);
			}
		}

		private AccountDbContext InMemoryContext()
		{
			AccountDbContext context = AccountDbContextTests.InMemoryContext();

			context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.TrackAll;

			//Add some test accounts
			context.Accounts.Add(new Account()
			{
				AccountID = 1,
				AccountName = "Test1",
				AccountStanding = Account.Standing.Active,
				CreationIP = "127.0.0.1",
				PasswordHash = ""
			});

			context.Accounts.Add(new Account()
			{
				AccountID = 2,
				AccountName = "Test2",
				AccountStanding = Account.Standing.Active,
				CreationIP = "127.0.0.1",
				PasswordHash = ""
			});

			context.Accounts.Add(new Account()
			{
				AccountID = 3,
				AccountName = "Test3",
				AccountStanding = Account.Standing.Active,
				CreationIP = "127.0.0.1",
				PasswordHash = ""
			});

			context.Accounts.Add(new Account()
			{
				AccountID = 4,
				AccountName = "Test4",
				AccountStanding = Account.Standing.Active,
				CreationIP = "127.0.0.1",
				PasswordHash = ""
			});

			context.SaveChanges(true);

			return context;
		}
	}
}
