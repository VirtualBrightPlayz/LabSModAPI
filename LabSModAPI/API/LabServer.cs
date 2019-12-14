using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Smod2.API;
using GameCore;

namespace LabSModAPI.API
{
	public class LabServer : Server
	{
		internal Round _round;

		public LabServer()
		{
			_round = new LabRound();
		}

		public override string Name { get => ServerConsole.singleton.ServerName; set => ServerConsole.singleton.ServerName = value; }

		public override int Port { get => ServerConsole.Port; }

		public override string IpAddress { get => ServerConsole.Ip; }

		public override Round Round { get => _round; }

		public override Map Map => throw new NotImplementedException();

		public override int NumPlayers { get => PlayerManager.players.Count; }

		public override int MaxPlayers { get => CustomNetworkManager.singleton.maxConnections; set => CustomNetworkManager.singleton.maxConnections = value; }

		public override bool Verified => throw new NotImplementedException();

		public override bool Visible { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
		public override string PlayerListTitle { get => ServerConsole.singleton.ServerName; set => ServerConsole.singleton.ServerName = value; }

		public override bool BanIpAddress(string username, string ipAddress, int duration, string reason = "", string issuer = "Server")
		{
			var details = new BanDetails();
			details.Expires = DateTime.Now.Ticks + duration;
			details.IssuanceTime = DateTime.Now.Ticks;
			details.Issuer = issuer;
			details.Reason = reason;
			details.Id = ipAddress;
			return BanHandler.IssueBan(details, BanHandler.BanType.IP);
		}

		public override bool BanSteamId(string username, string steamId, int duration, string reason = "", string issuer = "Server")
		{
			var details = new BanDetails();
			details.Expires = DateTime.Now.Ticks + duration;
			details.IssuanceTime = DateTime.Now.Ticks;
			details.Issuer = issuer;
			details.Reason = reason;
			details.Id = steamId;
			return BanHandler.IssueBan(details, BanHandler.BanType.IP);
		}

		public override string GetAppFolder(bool shared = false, bool addSeparator = false, bool addPort = false, bool addConfigs = false)
		{
			// TODO: use other values
			return FileManager.GetAppFolder(addSeparator);
		}

		public override List<Connection> GetConnections(string filter = "")
		{
			List<Connection> list = new List<Connection>();
			foreach (var plr in PlayerManager.players)
			{
				list.Add(new LabConnection(plr.GetComponent<CharacterClassManager>().connectionToClient));
			}
			return list;
		}

		public override Player GetPlayer(int playerId)
		{
			var plr = PlayerManager.players.Find(p => p.GetComponent<CharacterClassManager>().netId == playerId);
			if (plr != null)
			{
				return new LabPlayer(plr);
			}
			return null;
		}

		public override List<Player> GetPlayers()
		{
			List<Player> list = new List<Player>();
			foreach (var plr in PlayerManager.players)
			{
				list.Add(new LabPlayer(plr));
			}
			return list;
		}

		public override List<Player> GetPlayers(string filter)
		{
			List<Player> list = new List<Player>();
			foreach (var plr in PlayerManager.players)
			{
				var plr2 = new LabPlayer(plr);
				if (plr2.Name.Contains(filter))
					list.Add(plr2);
			}
			return list;
		}

		public override List<Player> GetPlayers(Smod2.API.Role role)
		{
			List<Player> list = new List<Player>();
			foreach (var plr in PlayerManager.players)
			{
				var plr2 = new LabPlayer(plr);
				if (plr2.TeamRole.Role.Equals(role))
					list.Add(plr2);
			}
			return list;
		}

		public override List<Player> GetPlayers(Smod2.API.Role[] roles)
		{
			List<Player> list = new List<Player>();
			foreach (var plr in PlayerManager.players)
			{
				var plr2 = new LabPlayer(plr);
				if (roles.Contains(plr2.TeamRole.Role))
					list.Add(plr2);
			}
			return list;
		}

		public override List<Player> GetPlayers(Smod2.API.Team team)
		{
			List<Player> list = new List<Player>();
			foreach (var plr in PlayerManager.players)
			{
				var plr2 = new LabPlayer(plr);
				if (plr2.TeamRole.Team == team)
					list.Add(plr2);
			}
			return list;
		}

		public override List<Player> GetPlayers(Predicate<Player> predicate)
		{
			List<Player> list = new List<Player>();
			foreach (var plr in PlayerManager.players)
			{
				var plr2 = new LabPlayer(plr);
				if (predicate(plr2))
					list.Add(plr2);
			}
			return list;
		}

		public override List<TeamRole> GetRoles(string filter = "")
		{
			// TODO
			throw new NotImplementedException();
		}
	}
}
