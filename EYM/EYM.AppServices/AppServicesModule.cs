using Autofac;
using EYM.AppServices.Interfaces;
using EYM.DBServices.Interfaces;


namespace EYM.AppServices
{
	public class AppServicesModule : Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			//builder.Register(c => new MyService(c.Resolve<IDBService>()))
			//	.As<IAppService>();

		}
	}
}
