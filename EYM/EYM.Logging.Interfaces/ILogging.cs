using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EYM.Logging.Interfaces
{
	public interface ILogging
	{
		void Log(string message, LogLevelEnum level);

		void Log(Exception exception, LogLevelEnum level);

		bool Enabled
		{
			get;
			set;
		}
	}
}
