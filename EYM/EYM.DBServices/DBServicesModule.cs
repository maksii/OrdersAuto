using Autofac;
using EYM.DBServices.Interfaces;
using EYM.Repositories.Interfaces;

namespace EYM.DBServices
{
	class DBServicesModule : Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			builder.Register(c => new MyDBService(c.Resolve<IMyRepository>()))
				.As<IMyDBService>();
		}
	}
}
