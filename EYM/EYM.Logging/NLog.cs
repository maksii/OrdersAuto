using System;
using EYM.Logging.Interfaces;
using NLog;

namespace EYM.Logging
{
	public class NLog : ILogging
	{
		private readonly Logger _logger;

		public NLog()
		{
			Enabled = true;

			LogManager.ReconfigExistingLoggers();
			_logger = LogManager.GetCurrentClassLogger();
		}

		public void Log(string message, LogLevelEnum logLevel)
		{
			if (!Enabled)
				return;

			switch (logLevel)
			{
				case LogLevelEnum.Fatal:
					_logger.Fatal(message);
					break;
				case LogLevelEnum.Error:
					_logger.Error(message);
					break;
				case LogLevelEnum.Warn:
					_logger.Warn(message);
					break;
				case LogLevelEnum.Info:
					_logger.Info(message);
					break;
				case LogLevelEnum.Debug:
					_logger.Debug(message);
					break;
				case LogLevelEnum.Trace:
					_logger.Trace(message);
					break;
				default:
					throw new ArgumentOutOfRangeException(nameof(logLevel), logLevel, null);
			}
		}

		public void Log(Exception exception, LogLevelEnum logLevel)
		{
			if (!Enabled)
				return;

			switch (logLevel)
			{
				case LogLevelEnum.Fatal:
					_logger.Fatal(exception);
					break;
				case LogLevelEnum.Error:
					_logger.Error(exception);
					break;
				case LogLevelEnum.Warn:
					_logger.Warn(exception);
					break;
				case LogLevelEnum.Info:
					_logger.Info(exception);
					break;
				case LogLevelEnum.Debug:
					_logger.Debug(exception);
					break;
				case LogLevelEnum.Trace:
					_logger.Trace(exception);
					break;
				default:
					throw new ArgumentOutOfRangeException(nameof(logLevel), logLevel, null);
			}
		}

		public bool Enabled
		{
			get;
			set;
		}
	}
}