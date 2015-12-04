using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity;

namespace EYM.Presentation.Admin.Models
{
	public class ExternalLoginModel
	{
		public string Email
		{
			get;
			set;
		}

		public string FirstName
		{
			get;
			set;
		}

		public string LastName
		{
			get;
			set;
		}
	}

	public class ExternalLoginListViewModel
	{
		public string ReturnUrl
		{
			get;
			set;
		}
	}

	public class ExternalUserLoginResult
	{
		public bool Result
		{
			get;
			set;
		}

		public IdentityResult IdentityResult
		{
			get; set;
		}
	}
}
