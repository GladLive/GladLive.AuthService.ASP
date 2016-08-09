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
using GladNet.ASP.Server;
using GladNet.Payload;

namespace GladLive.AuthService.ASP
{
	/// <summary>
	/// Controller services authentication/login requests.
	/// </summary>
	[PayloadRoute(typeof(AuthRequest))]
	public class AuthController : RequestController<AuthRequest>
	{
		[HttpGet]
		public string Get()
		{
			return "Hello to you";
		}

		/// <summary>
		/// Provided class logger.
		/// </summary>
		private ILogger classLogger { get; }

		/// <summary>
		/// User authentication service.
		/// </summary>
		private IAuthService authenticationService { get; }

		public AuthController(ILogger<AuthController> logger, IAuthService authService)
		{
			if (logger == null)
				throw new ArgumentNullException(nameof(logger), "Provided logger service is null.");

			if (authService == null)
				throw new ArgumentNullException(nameof(authService), $"Provided {nameof(IAuthService)} is null.");

			classLogger = logger;
			authenticationService = authService;
		}

		/// <summary>
		/// POST event that attempts to authenticate a session based on the <see cref="AuthRequest"/>
		/// details provided to the controller.
		/// </summary>
		/// <param name="payloadInstance">The authentication details provided.</param>
		/// <returns>A <see cref="PacketPayload"/> containing result information about the authentication request.</returns>
		public async override Task<PacketPayload> HandlePost(AuthRequest payloadInstance)
		{
			if (classLogger.IsEnabled(LogLevel.Information))
				classLogger.LogInformation("Reached auth method");

			//If the model isn't valid we should indicate a bad request result to the caller
			//WARNING: Doesn't really work with GladNet because empty deserialization is a thing
			if (!ModelState.IsValid)
				return null;

			//If the model is valid we can check for authentication
			AuthResponseCode responseCode = await authenticationService.TryAuthenticateAsync(payloadInstance.AuthDetails.LoginString, payloadInstance.AuthDetails.EncryptedPassword);

			return new AuthResponse(responseCode);
		}
	}
}
