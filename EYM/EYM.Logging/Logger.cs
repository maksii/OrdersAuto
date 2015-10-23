using System;
using EYM.Logging.Interfaces;
using NLog;

namespace EYM.Logging
{
	public class Logger : ILogging
	{
		private readonly NLog.Logger _logger;

		public Logger()
		{
			LogManager.ReconfigExistingLoggers();
			_logger = LogManager.GetCurrentClassLogger();
		}

		public void Info(string message, params object[] arguments)
		{
			_logger.Info(message);
		}

		public void Info(Exception exception, params object[] arguments)
		{
			//llll
			_logger.Info(exception);
		}

		public void Debug(string message, params object[] arguments)
		{
#if DEBUG
			_logger.Debug(message);
#endif
		}

		public void Debug(Exception exception, params object[] arguments)
		{
#if DEBUG
			_logger.Debug(exception);
#endif
		}

		public void Warning(string message, params object[] arguments)
		{
			_logger.Warn(message);
		}

		public void Warning(Exception exception, params object[] arguments)
		{
			_logger.Warn(exception);
		}

		public void Error(string message, params object[] arguments)
		{
			_logger.Error(message);
		}

		public void Error(Exception exception, params object[] arguments)
		{
			_logger.Error(exception);
		}
	}
}