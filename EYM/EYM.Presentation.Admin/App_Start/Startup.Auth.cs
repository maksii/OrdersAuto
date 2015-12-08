using System;
using EYM.Entities;
using EYM.EntityFramework;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Google;
using Owin;
using EYM.Presentation.Admin.Models;

namespace EYM.Presentation.Admin
{
	public partial class Startup
	{
		// For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301864
		public void ConfigureAuth(IAppBuilder app)
		{
			// Configure the db context, user manager and signin manager to use a single instance per request
			app.CreatePerOwinContext(EYMContext.Create);
			app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
			app.CreatePerOwinContext<ApplicationSignInManager>(ApplicationSignInManager.Create);

			// Enable the application to use a cookie to store information for the signed in user
			// and to use a cookie to temporarily store information about a user logging in with a third party login provider
			// Configure the sign in cookie
			app.UseCookieAuthentication(new CookieAuthenticationOptions
			{
				AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
				LoginPath = new PathString("/Account/Login"),
				Provider = new CookieAuthenticationProvider
				{
					// Enables the application to validate the security stamp when the user logs in.
					// This is a security feature which is used when you change a password or add an external login to your account.
					//TODO: check!!! it is not in workable condition!!!
					OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<ApplicationUserManager, User, int>(
						validateInterval: TimeSpan.FromMinutes(30),
						regenerateIdentityCallback: (manager, user) => user.GenerateUserIdentityAsync(manager),
						getUserIdCallback: (id) => id.GetUserId<int>())
				}
			});
			app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

			app.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions()
			{
				ClientId = "1871389728-d13m2tk7rokbcv44t3vosn5k35fp454r.apps.googleusercontent.com",
				ClientSecret = "LkqxcmQeAC7fE0W35a-AHaso"
			});
		}
	}
}