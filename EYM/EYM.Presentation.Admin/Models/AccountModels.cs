using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity;

namespace EYM.Presentation.Admin.Models
{
	public class ExternalLoginListViewModel
	{
		public string ReturnUrl
		{
			get;
			set;
		}
	}
}
