using System;

namespace EYM.Logging.Interfaces
{
	public interface ILogging
	{
		void Info(string message, params object[] arguments);
		void Info(Exception exception, params object[] arguments);

		void Debug(string message, params object[] arguments);
		void Debug(Exception exception, params object[] arguments);

		void Warning(string message, params object[] arguments);
		void Warning(Exception exception, params object[] arguments);

		void Error(string message, params object[] arguments);
		void Error(Exception exception, params object[] arguments);
	}
}