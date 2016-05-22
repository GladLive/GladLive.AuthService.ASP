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
		/// <summary>
		/// Hashes the <paramref name="valueToHash"/> using standard BCrypt.
		/// </summary>
		/// <param name="valueToHash"></param>
		/// <returns>Bcrypt hashed string.</returns>
		public string Hash(string valueToHash)
		{
			return BCrypt.Net.BCrypt.HashPassword(valueToHash);
		}

		/// <summary>
		/// Verifies if the given strings represent the same underlying data.
		/// </summary>
		/// <param name="hashOne">Plaintext string to compare.</param>
		/// <param name="hashTwo">Hash to compare plaintext against.</param>
		/// <returns>True if both sources represent the same underlying data.</returns>
		public bool isHashValuesEqual(string hashOne, string hashTwo)
		{
			return BCrypt.Net.BCrypt.Verify(hashOne, hashTwo);
		}
	}
}
