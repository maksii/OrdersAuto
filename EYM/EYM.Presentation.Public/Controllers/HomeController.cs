using System.Web.Mvc;
using Autofac;
using EYM.AppServices.Interfaces;
using System.Web;

namespace EYM.Presentation.Public.Controllers
{
	public class HomeController : Controller
	{
		public ActionResult Index()
		{
			
		
				var service = DependencyResolver.Current.GetService<IAppService>();
				//service.Serve();
		
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