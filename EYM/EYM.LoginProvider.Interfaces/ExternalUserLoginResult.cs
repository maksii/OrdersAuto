using Microsoft.AspNet.Identity;

namespace EYM.LoginProvider.Interfaces
{
	public class ExternalUserLoginResult
	{
		public bool Result
		{
			get;
			set;
		}

		public IdentityResult IdentityResult
		{
			get;
			set;
		}
	}
}
