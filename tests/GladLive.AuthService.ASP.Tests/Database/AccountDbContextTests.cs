using Microsoft.Data.Entity.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.Entity;

namespace GladLive.AuthService.ASP.Tests
{
	public class AccountDbContextTests
	{
		public static AccountDbContext InMemoryContext()
		{
			DbContextOptionsBuilder<AccountDbContext> optionsBuilder = new DbContextOptionsBuilder<AccountDbContext>();
			optionsBuilder.UseInMemoryDatabase();
			
			AccountDbContext context = new AccountDbContext(optionsBuilder.Options);

			return context;
		}
	}
}
