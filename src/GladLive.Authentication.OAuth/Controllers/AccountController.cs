using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GladLive.AuthService.ASP;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GladLive.Authentication.OAuth
{
	//WARNING: Do not deploy this controller to production
	//Just a test controller for adding accounts
	//Do not deploy this controller
	public class AccountController : Controller
	{
		private UserManager<GladLiveApplicationUser> UserManager { get; }

		public AccountController([NotNull] UserManager<GladLiveApplicationUser> userManager)
		{
			if (userManager == null) throw new ArgumentNullException(nameof(userManager));

			UserManager = userManager;
		}

		[HttpPost("api/AccountCreateRequest")]
		[AllowAnonymous]
		public async Task<IActionResult> Register([FromQuery]string userName, [FromQuery] string email, [FromQuery] string password)
		{
			if (string.IsNullOrWhiteSpace(userName)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(userName));
			if (string.IsNullOrWhiteSpace(email)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(email));
			if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(password));

			if (!ModelState.IsValid) return BadRequest(ModelState);

			var user = new GladLiveApplicationUser { UserName = userName, Email = email };
			var result = await UserManager.CreateAsync(user, password);

			if (result.Succeeded)
			{
				return Ok();
			}

			AddErrors(result);

			// If we got this far, something failed.
			return BadRequest(ModelState);
		}

		private void AddErrors(IdentityResult result)
		{
			foreach (var error in result.Errors)
			{
				ModelState.AddModelError(string.Empty, error.Description);
			}
		}
	}
}
