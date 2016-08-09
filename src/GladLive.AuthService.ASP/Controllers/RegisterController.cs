using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GladLive.AuthService.ASP
{
	[Route("api/Register")]
	public class RegisterController : Controller
	{
		private readonly UserManager<GladLiveApplicationUser> identityUserManager;

		public RegisterController(UserManager<GladLiveApplicationUser> userManager)
		{
			identityUserManager = userManager;
		}

		[HttpPost]
		public async Task<IActionResult> Register([FromQuery] string name, [FromServices] ILogger<RegisterController> logger)
		{
			logger.LogInformation($"User trying to register with name: {name}");

			if (!ModelState.IsValid)
				return new BadRequestResult();

			GladLiveApplicationUser user = new GladLiveApplicationUser { UserName = name, Email = "test@test.com" };
			IdentityResult result = await identityUserManager.CreateAsync(user, "Test123$");

			return new ObjectResult(result);
		}
	}
}
