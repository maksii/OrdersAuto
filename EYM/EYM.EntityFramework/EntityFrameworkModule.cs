using Autofac;
using EYM.EntityFramework.Interfaces;
using EYM.Repositories.Interfaces;

namespace EYM.EntityFramework
{
	class EntityFrameworkModule : Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			/*
			Register your project dependencies here. Autofac will automatically process them on MVC app start
			Syntax example:
			builder.Register(c => new MyClass()).As<IMyInterface>();
			*/
			builder.RegisterGeneric(typeof(EntityFrameworkRepository<>))
				.As(typeof(IGenericDataRepository<>))
				.InstancePerDependency();

			builder.RegisterType<EYMContext>()
				.As<IContext>();



		}
	}
}
