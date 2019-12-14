using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Smod2.API;

namespace LabSModAPI.API
{
	public class LabTeamRole : TeamRole
	{
		internal LabTeamRole(Role role)
		{
			_role = role;
		}

		internal LabTeamRole(RoleType role)
		{
			_role = PlayerManager.localPlayer.GetComponent<CharacterClassManager>().Classes.First(p => p.roleId == role);
		}

		public Smod2.API.Team GetTeam()
		{
			if (Role == Smod2.API.Role.CHAOS_INSURGENCY)
				return Smod2.API.Team.CHAOS_INSURGENCY;
			if (Role == Smod2.API.Role.CLASSD)
				return Smod2.API.Team.CLASSD;
			if (Role == Smod2.API.Role.NTF_CADET || Role == Smod2.API.Role.NTF_COMMANDER || Role == Smod2.API.Role.NTF_LIEUTENANT || Role == Smod2.API.Role.NTF_SCIENTIST || Role == Smod2.API.Role.FACILITY_GUARD)
				return Smod2.API.Team.NINETAILFOX;
			if (Role == Smod2.API.Role.UNASSIGNED)
				return Smod2.API.Team.NONE;
			if (Role == Smod2.API.Role.SCIENTIST)
				return Smod2.API.Team.SCIENTIST;
			if (Role.ToString().ToLower().StartsWith("SCP_"))
				return Smod2.API.Team.SCP;
			if (Role == Smod2.API.Role.SPECTATOR)
				return Smod2.API.Team.SPECTATOR;
			if (Role == Smod2.API.Role.TUTORIAL)
				return Smod2.API.Team.TUTORIAL;
			return Smod2.API.Team.NONE;
		}

		private Role _role = null;

		public override Smod2.API.Team Team { get => (Smod2.API.Team)(int)_role.team; }

		public override Smod2.API.Role Role { get => (Smod2.API.Role)(int)_role.roleId; }

		public override bool RoleDisallowed { get => _role.banClass; set => _role.banClass = value; }
		public override int MaxHP { get => _role.maxHP; set => _role.maxHP = value; }
		public override string Name { get => _role.fullName; set => _role.fullName = value; }

		public override object GetClass()
		{
			return _role;
		}
	}
}
