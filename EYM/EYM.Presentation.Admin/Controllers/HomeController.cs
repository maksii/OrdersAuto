using System.Web.Mvc;
using EYM.AppServices.Interfaces;
using System.Web.Http;

namespace EYM.Presentation.Admin.Controllers
{
	public class HomeController : Controller
	{
		public ActionResult Index()
		{

			//var service = GlobalConfiguration.Configuration.DependencyResolver.GetService(typeof(IAppService));
			//((IAppService)service).Serve();

			return View();
		}

		public ActionResult About()
		{
			ViewBag.Message = "Your application description page.";

			return View();
		}

		public ActionResult Contact()
		{
			ViewBag.Message = "Your contact page.";

			return View();
		}
	}
}