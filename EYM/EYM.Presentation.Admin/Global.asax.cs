using System.Reflection;
using System.Web.Http.SelfHost;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using System.Web.Compilation;
using System.Linq;
using System.Web.Http;

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
			//var config = GlobalConfiguration.Configuration;
			////var config = GlobalConfiguration.Configuration;

			// Register your Web API controllers.
			//builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

			// OPTIONAL: Register the Autofac filter provider.
			//builder.RegisterWebApiFilterProvider(config);
			builder.RegisterControllers(typeof(MvcApplication).Assembly);
			

			// Set the dependency resolver to be Autofac.
			var container = builder.Build();
			DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
		}
	}
}
