using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using EYM.Entities;
using EYM.LoginProvider.Interfaces;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;

namespace EYM.LoginProvider
{
	public class LoginProvider : ILoginProvider
	{
		public LoginProvider()
		{
			Initialization();
		}

		readonly List<string> _allowedDomains = new List<string>();
		readonly List<string> _allowedUsers = new List<string>();

		private void Initialization()
		{
			_allowedDomains.AddRange((ConfigurationManager.AppSettings["AllowedDomainsToLogin"] ?? "dev-pro.net").Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries));
			_allowedUsers.AddRange((ConfigurationManager.AppSettings["AllowedUsersToLogin"] ?? "").Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries));
		}

		public IEnumerable<string> AllowedUsers
		{
			get { return _allowedUsers; }
		}

		public IEnumerable<string> AllowedDomains
		{
			get { return _allowedDomains; }
		}

		public bool IsUserAllowedToLogin(string email)
		{
			var emailChunks = email.Split('@');
			return emailChunks.Length == 2 && (_allowedUsers.Any(email.Contains) || _allowedDomains.Any(emailChunks[1].Contains));
		}

		public async Task<ExternalLoginInfo> GetLoginInfo(IAuthenticationManager authenticationManager)
		{
			return await authenticationManager.GetExternalLoginInfoAsync();
		}

		private ExternalLoginModel GetLoginInfo(ClaimsIdentity claimsIdentity, string email)
		{
			var givenName = claimsIdentity.FindFirstValue(ClaimTypes.GivenName);
			var lastName = claimsIdentity.FindFirstValue(ClaimTypes.Surname);

			return new ExternalLoginModel
			{
				Email = email,
				FirstName = givenName,
				LastName = lastName,
			};
		}

		public async Task<ExternalUserLoginResult> Login(bool isModelStateValid, IAuthenticationManager authenticationManager, UserManager<User, int> userManager, SignInManager<User, int> signInManager)
		{
			var result = new ExternalUserLoginResult();

			if (!isModelStateValid)
				return result;

			var loginInfo = await GetLoginInfo(authenticationManager);
			if (loginInfo == null)
				return result;

			var model = GetLoginInfo(loginInfo.ExternalIdentity, loginInfo.Email);

			var user = new User
			{
				FirstName = model.FirstName,
				LastName = model.LastName,
				Email = model.Email,
				UserName = model.Email,
				RoleId = 1
			};
			result.IdentityResult = await userManager.CreateAsync(user);
			if (result.IdentityResult.Succeeded)
			{
				await signInManager.SignInAsync(user, false, false);
				result.Result = true;
				return result;
			}

			return result;
		}
	}
}
