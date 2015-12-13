using System.Collections.Generic;
using System.Threading.Tasks;
using EYM.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;

namespace EYM.LoginProvider.Interfaces
{
	public interface ILoginProvider
	{
		IEnumerable<string> AllowedUsers
		{
			get;
		}

		IEnumerable<string> AllowedDomains
		{
			get;
		}

		bool IsUserAllowedToLogin(string email);

		Task<ExternalLoginInfo> GetLoginInfo(IAuthenticationManager authenticationManager);

        Task<ExternalUserLoginResult> Login(bool isModelStateValid, IAuthenticationManager authenticationManager, UserManager<User, int> userManager, SignInManager<User, int> signInManager);
	}
}
