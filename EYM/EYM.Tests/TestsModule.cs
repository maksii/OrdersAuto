using Autofac;
using EYM.EntityFramework;

namespace EYM.Tests
{
	class TestsModule : Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			builder.RegisterType<EYMContext>().As<Context>().WithParameter("connectionString", "EYMContext");
		}
	}
}
