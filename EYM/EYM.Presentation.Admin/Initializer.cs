using System.IO;
using System.Reflection;
using System.Web.Compilation;
using System.Web.Hosting;

namespace EYM.Presentation.Admin
{
	public static class Initializer
	{
		public static void Initialize()
		{
			var pluginFolder = new DirectoryInfo(HostingEnvironment.MapPath("~/Plugins"));
			try
			{
				var pluginAssemblyFiles = pluginFolder.GetFiles("*.dll", SearchOption.AllDirectories);
				foreach (var pluginAssemblyFile in pluginAssemblyFiles)
				{
					var asm = Assembly.LoadFrom(pluginAssemblyFile.FullName);
					BuildManager.AddReferencedAssembly(asm);
				}
			}
			catch
			{
			}
		}
	}
}