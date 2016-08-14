using GladLive.Module.System.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Logging;

namespace GladLive.Authentication.OAuth.Module.Modules
{
	public class OAuthApplicationModule : ApplicationConfigurationModule
	{
		public OAuthApplicationModule(IApplicationBuilder app, ILoggerFactory loggerFactory)
			: base(app, loggerFactory)
		{

		}

		public override void Register()
		{
			this.applicationBuilder.UseOpenIddict();
		}
	}
}
