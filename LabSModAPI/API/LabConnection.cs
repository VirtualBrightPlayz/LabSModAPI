using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Smod2.API;
using Mirror;

namespace LabSModAPI.API
{
	public class LabConnection : Connection
	{
		internal NetworkConnection _conn;

		public LabConnection(NetworkConnection conn)
		{
			_conn = conn;
		}

		public override string IpAddress { get => _conn.address; }

		public override bool IsBanned { get => BanHandler.GetBans(BanHandler.BanType.IP).Find(p => p.Id == IpAddress) != null; }

		public override void Disconnect()
		{
			_conn.Disconnect();
		}
	}
}
