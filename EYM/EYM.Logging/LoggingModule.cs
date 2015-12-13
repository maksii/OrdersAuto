using Autofac;
using EYM.Logging.Interfaces;

namespace EYM.Logging
{
	class LoggingModule : Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			builder.RegisterType<NLogLogger>()
				.As<ILogging>();
		}
	}
}
