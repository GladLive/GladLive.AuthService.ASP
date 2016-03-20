using Microsoft.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GladLive.AuthService.ASP
{
	/// <summary>
	/// Base type for repositories with consolidated functionality
	/// for manipulating the context.
	/// </summary>
	/// <typeparam name="TDbContextType">Database context type.</typeparam>
	public abstract class Repository<TDbContextType>
		where TDbContextType : DbContext
	{
		/// <summary>
		/// Database context for the <see cref="Repository{TDbContextType}"/>
		/// </summary>
		protected TDbContextType databaseContext { get; }

		public Repository(TDbContextType context)
		{
			databaseContext = context;
		}
	}
}
