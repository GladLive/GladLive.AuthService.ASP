using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GladLive.Web.Payloads.Authentication;
using ProtoBuf;
using GladNet.Serializer;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace GladLive.AuthService.ASP
{
	/// <summary>
	/// Controller services authentication/login requests.
	/// </summary>
	[Route("api/" + nameof(AuthRequest))]
	public class AuthController : Controller
	{
		[HttpGet]
		public string Get()
		{
			return "Hello to you";
		}

		private ILogger classLogger { get; }

		public AuthController(ILogger<AuthController> logger)
		{
			if (logger == null)
				throw new ArgumentNullException(nameof(logger), "Provided logger service is null.");

			classLogger = logger;
		}

		/// <summary>
		/// POST event that attempts to authenticate a session based on the <see cref="AuthRequestModel"/>
		/// details provided to the controller.
		/// </summary>
		/// <param name="model">The authentication details provided.</param>
		/// <returns>A model containing result information about the authentication request.</returns>
		//[Route("Authenticate")]
		[HttpPost]
		public async Task<IActionResult> Authenticate([FromBody] AuthRequest model, [FromServices] IAuthService authService) //authe request model data should be sent in the body of the request
		{
			if(classLogger.IsEnabled(LogLevel.Information))
				classLogger.LogInformation("Reached auth method");

			//If the model isn't valid we should indicate a bad request result to the caller
			if (!ModelState.IsValid)
				return new BadRequestResult();


			//If the model is valid we can check for authentication
			AuthResponseCode responseCode = await authService.TryAuthenticateAsync(model.AuthDetails.LoginString, model.AuthDetails.EncryptedPassword);

			//return new ProtobufNetObjectResult(new AuthRequest(IPAddress.Any, new LoginDetails("helo", new byte[0])));
			//return new ProtobufNetObjectResult(new AuthResponse(responseCode));
			return new BadRequestResult();
		}
	}
}
