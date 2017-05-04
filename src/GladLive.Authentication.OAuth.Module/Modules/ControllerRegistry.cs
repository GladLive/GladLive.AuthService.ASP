using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Controllers;

namespace GladLive.Authentication.OAuth
{
	public class ControllerRegistry : IApplicationFeatureProvider<ControllerFeature>
	{
		public void PopulateFeature([NotNull] IEnumerable<ApplicationPart> parts, [NotNull] ControllerFeature feature)
		{
			if (parts == null) throw new ArgumentNullException(nameof(parts));
			if (feature == null) throw new ArgumentNullException(nameof(feature));

			//We only have one controller which is the new OpenIddict authorization controller that you have to implement
			feature.Controllers.Add(typeof(AuthorizationController).GetTypeInfo());
			feature.Controllers.Add(typeof(AccountController).GetTypeInfo());
			feature.Controllers.Add(typeof(JwtTestController).GetTypeInfo());
		}
	}
}
