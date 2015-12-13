using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Compilation;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.Mvc;

namespace EYM.Presentation.Public
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

			// Register your MVC controllers.
			builder.RegisterControllers(typeof(MvcApplication).Assembly);
			
			builder.RegisterControllers(typeof(MvcApplication).Assembly);
			
			// Set the dependency resolver to be Autofac.
			var container = builder.Build();

			DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}
