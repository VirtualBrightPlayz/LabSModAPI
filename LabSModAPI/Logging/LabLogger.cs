using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Smod2.Logging;

namespace LabSModAPI.Logging
{
	public class LabLogger : Logger
	{
		public override void Debug(string tag, string message)
		{
			ServerConsole.AddLog("[DEBUG-" + tag + "] " + message);
		}

		public override void Error(string tag, string message)
		{
			ServerConsole.AddLog("[ERROR-" + tag + "] " + message);
		}

		public override void Info(string tag, string message)
		{
			ServerConsole.AddLog("[INFO-" + tag + "] " + message);
		}

		public override void Warn(string tag, string message)
		{
			ServerConsole.AddLog("[WARN-" + tag + "] " + message);
		}
	}
}
