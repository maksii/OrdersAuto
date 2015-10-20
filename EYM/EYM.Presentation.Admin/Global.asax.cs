using System.Reflection;
using System.Web.Http;
using System.Web.Http.SelfHost;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.WebApi;


namespace EYM.Presentation.Admin
{
	public class MvcApplication : System.Web.HttpApplication
	{
		protected void Application_Start()
		{
			AreaRegistration.RegisterAllAreas();
			FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
			RouteConfig.RegisterRoutes(RouteTable.Routes);
			BundleConfig.RegisterBundles(BundleTable.Bundles);

			//IoC initialization
			var builder = new ContainerBuilder();

			// Get your HttpConfiguration.
			var config = new HttpSelfHostConfiguration("http://localhost:8080");
			//var config = GlobalConfiguration.Configuration;

			// Register your Web API controllers.
			builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

			// OPTIONAL: Register the Autofac filter provider.
			//builder.RegisterWebApiFilterProvider(config);

			// Set the dependency resolver to be Autofac.
			var container = builder.Build();
			config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
		}
	}
}
