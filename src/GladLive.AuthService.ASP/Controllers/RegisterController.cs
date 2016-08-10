using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GladLive.AuthService.ASP
{
	/// <summary>
	/// This is only a temporarily test controller for registering a user.
	/// </summary>
	[Route("api/Register")]
	public class RegisterController : Controller
	{
		private UserManager<GladLiveApplicationUser> identityUserManager { get; }

		public RegisterController(UserManager<GladLiveApplicationUser> userManager)
		{
			identityUserManager = userManager;
		}

		[HttpPost]
		public async Task<IActionResult> Register([FromQuery] string username, [FromQuery] string password, [FromServices] ILogger<RegisterController> logger)
		{
			logger.LogInformation($"User trying to register with name: {username}");

			if (!ModelState.IsValid)
				return new BadRequestResult();

			GladLiveApplicationUser user = new GladLiveApplicationUser { UserName = username, Email = "test@test.com" };
			IdentityResult result = await identityUserManager.CreateAsync(user, password);

			return Content(result.Succeeded ? "Successful registration." : "Failed registration");
		}
	}
}
