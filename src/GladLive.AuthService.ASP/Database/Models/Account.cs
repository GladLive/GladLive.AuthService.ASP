using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GladLive.AuthService.ASP
{
	/// <summary>
	/// POCO model for Account table object.
	/// </summary>
	public class Account
	{
		public enum Standing : byte
		{
			Active = 0,
			Suspended = 1,
			Closed = 2
		}

		[Key]
		[ScaffoldColumn(false)]
		public int AccountID { get; set; }

		[Required]
		[MaxLength(15)]
		public string CreationIP { get; set; }

		[Required]
		public string AccountName { get; set; }

		[Required]
		public string PasswordHash { get; set; }

		[Required]
		[EnumDataType(typeof(Standing))]
		[Range(0, 2)]
		public Account.Standing AccountStanding { get; set; }

	}
}
