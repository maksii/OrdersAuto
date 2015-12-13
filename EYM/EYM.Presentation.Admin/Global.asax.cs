using System.Reflection;
using System.Web.Http.SelfHost;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.WebApi;
using System.Web.Compilation;
using System.Linq;
using System.Web.Http;
using Autofac.Integration.Mvc;
using EYM.Presentation.Admin.Controllers;

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

			//Get all referenced assemblies and register modules
			Assembly[] ass = BuildManager.GetReferencedAssemblies().Cast<Assembly>().ToArray();
			builder.RegisterAssemblyModules(ass);

			// Get your HttpConfiguration.
			var config = GlobalConfiguration.Configuration;

			// Register your Web API controllers.
			builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
			// Register your Web controllers.
			builder.RegisterControllers(Assembly.GetExecutingAssembly());

			// Set the dependency resolver to be Autofac.
			var container = builder.Build();
			config.DependencyResolver = new AutofacWebApiDependencyResolver(container);

			DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
		}
	}
}
