using System;
using System.Configuration;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using EYM.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using EYM.Presentation.Admin.Models;

namespace EYM.Presentation.Admin.Controllers
{
	[Authorize]
	public class AccountController : Controller
	{
		private ApplicationSignInManager _signInManager;
		private ApplicationUserManager _userManager;

		public AccountController()
		{
		}

		public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
		{
			UserManager = userManager;
			SignInManager = signInManager;
		}

		public ApplicationSignInManager SignInManager
		{
			get
			{
				return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
			}
			private set
			{
				_signInManager = value;
			}
		}

		public ApplicationUserManager UserManager
		{
			get
			{
				return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
			}
			private set
			{
				_userManager = value;
			}
		}

		//
		// GET: /Account/Login
		[AllowAnonymous]
		public ActionResult Login(string returnUrl)
		{
			ViewBag.ReturnUrl = returnUrl;
			return View();
		}

		[AllowAnonymous]
		public ActionResult LoginFailed(string returnUrl)
		{
			ViewBag.ReturnUrl = returnUrl;
			ViewBag.ErrorMessage = "You are not allowed to login with this credentials!";
			return View("Login");
		}

		//
		// POST: /Account/ExternalLogin
		[HttpPost]
		[AllowAnonymous]
		[ValidateAntiForgeryToken]
		public ActionResult ExternalLogin(string provider, string returnUrl)
		{
			// Request a redirect to the external login provider
			return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new
			{
				ReturnUrl = returnUrl
			}));
		}

		//
		// GET: /Account/ExternalLoginCallback
		[AllowAnonymous]
		public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
		{
			if (User.Identity.IsAuthenticated)
				return RedirectToLocal(returnUrl);

			var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
			if (loginInfo == null || string.IsNullOrEmpty(loginInfo.Email))
				return RedirectToAction("Login");

			var domains = (ConfigurationManager.AppSettings["AllowedDomainsToLogin"] ?? "dev-pro.net").Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
			var users = (ConfigurationManager.AppSettings["AllowedUsersToLogin"] ?? "").Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries);

			var emailChunks = loginInfo.Email.Split('@');
			if (emailChunks.Length != 2 && (!users.Any(loginInfo.Email.Contains) || !domains.Any(emailChunks[1].Contains)))
				return RedirectToAction("LoginFailed");

			//TODO: https://developers.google.com/+/web/people/
			//GET https://www.googleapis.com/plus/v1/people/me
			//loginInfo.DefaultUserName = loginInfo.ExternalIdentity.GetUserName();

			var user = await UserManager.FindByEmailAsync(loginInfo.Email);

			if (user != null)
			{
				await SignInManager.SignInAsync(user, false, false);
				return RedirectToLocal(returnUrl);
			}

			var givenName = loginInfo.ExternalIdentity.FindFirstValue(ClaimTypes.GivenName);
			var lastName = loginInfo.ExternalIdentity.FindFirstValue(ClaimTypes.Surname);
			var externalLoginInfo = new ExternalLoginModel
			{
				Email = loginInfo.Email,
				FirstName = givenName,
				LastName = lastName,
			};

			var result = await _externalUserLogin(externalLoginInfo);
			if (result.Result)
				return RedirectToLocal(returnUrl);

			if (result.IdentityResult != null)
				AddErrors(result.IdentityResult);

			return View("ExternalLoginFailure");
		}

		private async Task<ExternalUserLoginResult> _externalUserLogin(ExternalLoginModel model)
		{
			var result = new ExternalUserLoginResult();

			if (!ModelState.IsValid)
				return result;

			var info = await AuthenticationManager.GetExternalLoginInfoAsync();
			if (info == null)
				return result;

			var user = new User
			{
				FirstName = model.FirstName,
				LastName = model.LastName,
				Email = model.Email,
				UserName = model.Email
			};
			result.IdentityResult = await UserManager.CreateAsync(user);
			if (result.IdentityResult.Succeeded)
			{
				await SignInManager.SignInAsync(user, false, false);
				result.Result = true;
				return result;
			}

			return result;
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult LogOff()
		{
			AuthenticationManager.SignOut();
			return RedirectToAction("Index", "Home");
		}

		//
		// GET: /Account/ExternalLoginFailure
		[AllowAnonymous]
		public ActionResult ExternalLoginFailure()
		{
			return View();
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (_userManager != null)
				{
					_userManager.Dispose();
					_userManager = null;
				}

				if (_signInManager != null)
				{
					_signInManager.Dispose();
					_signInManager = null;
				}
			}

			base.Dispose(disposing);
		}

		#region Helpers

		// Used for XSRF protection when adding external logins
		private const string XsrfKey = "XsrfId";

		private IAuthenticationManager AuthenticationManager
		{
			get
			{
				return HttpContext.GetOwinContext().Authentication;
			}
		}

		private void AddErrors(IdentityResult result)
		{
			foreach (var error in result.Errors)
			{
				ModelState.AddModelError("", error);
			}
		}

		private ActionResult RedirectToLocal(string returnUrl)
		{
			if (Url.IsLocalUrl(returnUrl))
			{
				return Redirect(returnUrl);
			}
			return RedirectToAction("Index", "Home");
		}

		internal class ChallengeResult : HttpUnauthorizedResult
		{
			public ChallengeResult(string provider, string redirectUri)
				: this(provider, redirectUri, null)
			{
			}

			public ChallengeResult(string provider, string redirectUri, string userId)
			{
				LoginProvider = provider;
				RedirectUri = redirectUri;
				UserId = userId;
			}

			public string LoginProvider
			{
				get; set;
			}
			public string RedirectUri
			{
				get; set;
			}
			public string UserId
			{
				get; set;
			}

			public override void ExecuteResult(ControllerContext context)
			{
				var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
				if (UserId != null)
				{
					properties.Dictionary[XsrfKey] = UserId;
				}
				context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
			}
		}

		#endregion
	}
}