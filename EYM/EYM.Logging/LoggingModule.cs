using Autofac;
using EYM.Logging.Interfaces;

namespace EYM.Logging
{
	class LoggingModule : Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			builder.Register(c => new NLogLogger()).As<ILogging>();
		}
	}
}
