using Microsoft.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

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
