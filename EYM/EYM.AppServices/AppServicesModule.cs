using Autofac;
using EYM.AppServices.Interfaces;

namespace EYM.AppServices
{
	public class AppServicesModule : Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			builder.Register(c => new MyService()).As<IAppService>();
		}
	}
}
