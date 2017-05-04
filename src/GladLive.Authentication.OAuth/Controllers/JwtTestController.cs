using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AspNet.Security.OpenIdConnect.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GladLive.Authentication.OAuth
{
	[Route("api/JwtTest")]
	public class JwtTestController : Controller
	{
		[HttpGet]
		[Authorize]
		public string Get()
		{
			if (this.User != null)
				return $"User Authenticated: {this.User.Identity.IsAuthenticated} with Name: {User.Identity.Name} and Id: {User.GetClaim(@"http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")} Claims: {User.Claims.Aggregate("", (s1, s2) => $"{s1} {s2}")}";
			else
				return "Unable to load user identity.";
		}
	}
}
