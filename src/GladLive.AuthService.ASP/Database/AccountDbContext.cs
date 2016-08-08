using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GladLive.AuthService.ASP
{
	public class AccountDbContext : DbContext, IDisposable
	{
		public DbSet<Account> Accounts { get; set; }

		public AccountDbContext(DbContextOptions options) 
			: base(options)
		{

		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{

		}
	}
}
