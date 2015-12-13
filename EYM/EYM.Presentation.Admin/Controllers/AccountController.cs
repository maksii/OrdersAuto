using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using EYM.LoginProvider.Interfaces;
using EYM.UserStore;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;

namespace EYM.Presentation.Admin.Controllers
{
	[Authorize]
	public class AccountController : Controller
	{
		private readonly ILoginProvider _loginProvider;

		public AccountController(ILoginProvider loginProvider)
		{
			_loginProvider = loginProvider;
		}

		public ApplicationSignInManager SignInManager => HttpContext.GetOwinContext().Get<ApplicationSignInManager>();

		public ApplicationUserManager UserManager => HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();

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

			var loginInfo = await _loginProvider.GetLoginInfo(AuthenticationManager);
			if (string.IsNullOrEmpty(loginInfo?.Email))
				return RedirectToAction("Login");

			if (!_loginProvider.IsUserAllowedToLogin(loginInfo.Email))
				return RedirectToAction("LoginFailed");

			var user = await UserManager.FindByEmailAsync(loginInfo.Email);

			if (user != null)
			{
				await SignInManager.SignInAsync(user, false, false);
				return RedirectToLocal(returnUrl);
			}

			var result = await _loginProvider.Login(ModelState.IsValid, AuthenticationManager, UserManager, SignInManager);
			if (result.Result)
				return RedirectToLocal(returnUrl);

			if (result.IdentityResult != null)
				AddErrors(result.IdentityResult);

			return View("ExternalLoginFailure");
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
			base.Dispose(disposing);
		}

		#region Helpers

		// Used for XSRF protection when adding external logins
		private const string XsrfKey = "XsrfId";

		private IAuthenticationManager AuthenticationManager => HttpContext.GetOwinContext().Authentication;

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