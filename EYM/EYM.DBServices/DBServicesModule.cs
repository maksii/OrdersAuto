using Autofac;
using EYM.DBServices.Interfaces;
using EYM.Repositories.Interfaces;

namespace EYM.DBServices
{
	class DBServicesModule : Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			builder.RegisterGeneric(typeof(DBService<>))
				.As(typeof(IDBService<>))
				.InstancePerDependency();
		}
	}
}
