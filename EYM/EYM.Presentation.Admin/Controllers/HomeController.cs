using System.Web.Mvc;
using System.Web.Http;

namespace EYM.Presentation.Admin.Controllers
{
	public class HomeController : Controller
	{
		public ActionResult Index()
		{

			//var service = GlobalConfiguration.Configuration.DependencyResolver.GetService(typeof(IAppService));
			//((IAppService)service).GetAll();

			return View();
		}

		public ActionResult About()
		{
			ViewBag.Message = "Your application description page.";

			return View();
		}

		[System.Web.Mvc.Authorize]
		public ActionResult Contact()
		{
			ViewBag.Message = "Your contact page.";

			return View();
		}
	}
}