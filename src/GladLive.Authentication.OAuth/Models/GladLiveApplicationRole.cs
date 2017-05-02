using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace GladLive.AuthService.ASP
{
	/// <summary>
	/// GladLive OpenIddict app role.
	/// See Documentation for details: https://github.com/openiddict/openiddict-core
	/// </summary>
	public class GladLiveApplicationRole : IdentityRole<int> { }
}
