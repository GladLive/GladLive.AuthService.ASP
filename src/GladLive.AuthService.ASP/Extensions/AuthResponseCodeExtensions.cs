using GladLive.Web.Payloads.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GladLive.AuthService.ASP
{
	public static class AuthResponseCodeExtensions
	{
		public static AuthResponseCode ToResponseCode(this Account.Standing standing)
		{
			switch (standing)
			{
				case Account.Standing.Active:
					return AuthResponseCode.Success;
				case Account.Standing.Suspended:
					return AuthResponseCode.Locked;
				case Account.Standing.Closed:
					return AuthResponseCode.Banned;
				default:
					throw new InvalidOperationException($"Invalid {nameof(Account.Standing)} value {standing}");
			}
		}
	}
}
