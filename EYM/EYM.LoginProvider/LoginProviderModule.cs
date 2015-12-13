using Autofac;
using EYM.LoginProvider.Interfaces;

namespace EYM.LoginProvider
{
	class LoginProviderModule : Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			builder.RegisterType<LoginProvider>().As<ILoginProvider>();
		}
	}
}
