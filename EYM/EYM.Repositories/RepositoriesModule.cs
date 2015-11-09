using Autofac;
using EYM.Repositories.Interfaces;
 
namespace EYM.Repositories
{
	class RepositoriesModule : Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			/*
			Register your project dependencies here. Autofac will automatically process them on MVC app start
			Syntax example:
			builder.Register(c => new MyClass()).As<IMyInterface>();
			*/
			builder.Register(c => new MyRepository()).As<IMyRepository>();
		}
	}
}
