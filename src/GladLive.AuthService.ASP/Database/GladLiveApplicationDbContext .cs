using Microsoft.EntityFrameworkCore;
using OpenIddict;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GladLive.AuthService.ASP
{
	/// <summary>
	/// <see cref="DbContext"/> for the <see cref="GladLiveApplicationUser"/>s for <see cref="OpenIddict"/>.
	/// See Documentation for details: https://github.com/openiddict/openiddict-core
	/// </summary>
	public class GladLiveApplicationDbContext : OpenIddictDbContext<GladLiveApplicationUser>
	{
		public GladLiveApplicationDbContext(DbContextOptions<GladLiveApplicationDbContext> options) 
			: base(options)
		{

		}
	}
}
