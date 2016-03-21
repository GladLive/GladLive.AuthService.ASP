using GladLive.Security.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GladLive.AuthService.ASP
{
	/// <summary>
	/// Concrete hashing service that uses a Bcrypt implementation for .Net
	/// </summary>
	public class BCryptHashingService : IHashingService
	{
		public string Hash(string valueToHash)
		{
			return BCrypt.Net.BCrypt.HashPassword(valueToHash);
		}

		public bool isHashValuesEqual(string hashOne, string hashTwo)
		{
			return BCrypt.Net.BCrypt.Verify(hashOne, hashTwo);
		}
	}
}
