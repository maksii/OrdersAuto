using Autofac;
using EYM.DBServices.Interfaces;
using EYM.Repositories.Interfaces;

namespace EYM.DBServices
{
	class DBServicesModule : Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			//builder.Register(c => new DBService<IEntity>(c.Resolve<IGenericDataRepository<IEntity>>()))
			//	.As<IDBService<IEntity>>();
			builder.RegisterGeneric(typeof(DBService<>))
				.As(typeof(IDBService<>))
				.InstancePerDependency();
		}
	}
}
