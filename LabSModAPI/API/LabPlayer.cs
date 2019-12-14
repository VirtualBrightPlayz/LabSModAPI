using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grenades;
using Smod2.API;
using UnityEngine;

namespace LabSModAPI.API
{
	public class LabPlayer : Player
	{
		internal GameObject UnityPlayerObject;

		internal CharacterClassManager ccm => UnityPlayerObject.GetComponent<CharacterClassManager>();
		internal NicknameSync nicks => UnityPlayerObject.GetComponent<NicknameSync>();
		internal ServerRoles sroles => UnityPlayerObject.GetComponent<ServerRoles>();
		internal PlayerStats stats => UnityPlayerObject.GetComponent<PlayerStats>();
		internal Radio radio => UnityPlayerObject.GetComponent<Radio>();
		internal Inventory inv => UnityPlayerObject.GetComponent<Inventory>();

		internal LabPlayer(GameObject go)
		{
			UnityPlayerObject = go;
		}

		[Obsolete("Use int value")]
		public static RoleType ConvertTeamRole(TeamRole teamRole)
		{
			switch (teamRole.Role)
			{
				case Smod2.API.Role.CHAOS_INSURGENCY:
					return RoleType.ChaosInsurgency;
				case Smod2.API.Role.CLASSD:
					return RoleType.ClassD;
				case Smod2.API.Role.FACILITY_GUARD:
					return RoleType.FacilityGuard;
				case Smod2.API.Role.NTF_CADET:
					return RoleType.NtfCadet;
				case Smod2.API.Role.NTF_COMMANDER:
					return RoleType.NtfCommander;
				case Smod2.API.Role.NTF_LIEUTENANT:
					return RoleType.NtfLieutenant;
				case Smod2.API.Role.NTF_SCIENTIST:
					return RoleType.NtfScientist;
				case Smod2.API.Role.SCIENTIST:
					return RoleType.Scientist;
				case Smod2.API.Role.SCP_049:
					return RoleType.Scp049;
				case Smod2.API.Role.SCP_049_2:
					return RoleType.Scp0492;
				case Smod2.API.Role.SCP_079:
					return RoleType.Scp079;
				case Smod2.API.Role.SCP_096:
					return RoleType.Scp096;
				case Smod2.API.Role.SCP_106:
					return RoleType.Scp106;
				case Smod2.API.Role.SCP_173:
					return RoleType.Scp173;
				case Smod2.API.Role.SCP_939_53:
					return RoleType.Scp93953;
				case Smod2.API.Role.SCP_939_89:
					return RoleType.Scp93989;
				case Smod2.API.Role.SPECTATOR:
					return RoleType.Spectator;
				case Smod2.API.Role.TUTORIAL:
					return RoleType.Tutorial;
				case Smod2.API.Role.UNASSIGNED:
					return RoleType.None;
				default:
					return RoleType.None;
			}
		}

		public override TeamRole TeamRole { get => new LabTeamRole(ccm.NetworkCurClass); set => ccm.SetClassID((RoleType)(int)value.Role); }

		public override string Name { get => nicks.Network_myNickSync; }

		public override string IpAddress { get => ccm.connectionToClient.address; }

		public override int PlayerId { get => (int)ccm.netId; }

		public override string SteamId { get => ccm.NetworkSyncedUserId; }

		public override RadioStatus RadioStatus { get => (RadioStatus)radio.NetworkcurPreset; set => radio.CmdUpdatePreset((int)value); }
		public override bool OverwatchMode { get => sroles.IsOverwatchEnabled(); set => sroles.TargetSetOverwatch(sroles.connectionToClient, value); }

		public override bool DoNotTrack { get => sroles.DoNotTrack; }

		public override Scp079Data Scp079Data => throw new NotImplementedException();

		public override bool BypassMode { get => sroles.BypassMode; set => sroles.BypassMode = value; }
		public override bool Muted { get => ccm.NetworkMuted; set => ccm.NetworkMuted = value; }
		public override bool IntercomMuted { get => ccm.NetworkIntercomMuted; set => ccm.NetworkIntercomMuted = value; }

		public override void AddHealth(int amount)
		{
			AddHealth((float)amount);
		}

		public override void AddHealth(float amount)
		{
			stats.HealHPAmount(amount);
		}

		public override void Ban(int duration)
		{
			PlayerManager.localPlayer.GetComponent<BanPlayer>().BanUser(UnityPlayerObject, duration, "[LabSModAPI] Reason not given.", "Server");
		}

		public override void Ban(int duration, string message)
		{
			PlayerManager.localPlayer.GetComponent<BanPlayer>().BanUser(UnityPlayerObject, duration, "[LabSModAPI] " + message, "Server");
		}

		public override void ChangeRole(Smod2.API.Role role, bool full = true, bool spawnTeleport = true, bool spawnProtect = true, bool removeHandcuffs = false)
		{
			if (full)
				ccm.SetPlayersClass((RoleType)role, UnityPlayerObject, spawnTeleport);
			else
				ccm.SetClassIDAdv((RoleType)role, spawnTeleport);
		}

		public override void Damage(int amount, DamageType type = DamageType.NUKE)
		{
			Damage((float)amount, type);
		}

		public override void Damage(float amount, DamageType type = DamageType.NUKE)
		{
			stats.HurtPlayer(new PlayerStats.HitInfo(amount, "Server", GetDamageType(type), -1), UnityPlayerObject);
		}

		public DamageTypes.DamageType GetDamageType(DamageType type)
		{
			switch (type)
			{
				case DamageType.NONE:
					return DamageTypes.None;
				case DamageType.LURE:
					return DamageTypes.Lure;
				case DamageType.NUKE:
					return DamageTypes.Nuke;
				case DamageType.WALL:
					return DamageTypes.Wall;
				case DamageType.DECONT:
					return DamageTypes.Decont;
				case DamageType.TESLA:
					return DamageTypes.Tesla;
				case DamageType.FALLDOWN:
					return DamageTypes.Falldown;
				case DamageType.FLYING:
					return DamageTypes.Flying;
				case DamageType.CONTAIN:
					return DamageTypes.Contain;
				case DamageType.POCKET:
					return DamageTypes.Pocket;
				case DamageType.RAGDOLLLESS:
					return DamageTypes.RagdollLess;
				case DamageType.COM15:
					return DamageTypes.Com15;
				case DamageType.P90:
					return DamageTypes.P90;
				case DamageType.E11_STANDARD_RIFLE:
					return DamageTypes.E11StandardRifle;
				case DamageType.MP7:
					return DamageTypes.Mp7;
				case DamageType.LOGICER:
					return DamageTypes.Logicer;
				case DamageType.USP:
					return DamageTypes.Usp;
				case DamageType.FRAG:
					return DamageTypes.Grenade;
				case DamageType.SCP_049:
					return DamageTypes.Scp049;
				case DamageType.SCP_049_2:
					return DamageTypes.Scp0492;
				case DamageType.SCP_096:
					return DamageTypes.Scp096;
				case DamageType.SCP_106:
					return DamageTypes.Scp106;
				case DamageType.SCP_173:
					return DamageTypes.Scp173;
				case DamageType.SCP_939:
					return DamageTypes.Scp939;
				case DamageType.SCP_207:
					return DamageTypes.Scp207;
				case DamageType.RECONTAIN:
					return DamageTypes.Recontainment;
				default:
					return DamageTypes.None;
			}
		}

		public override void Disconnect()
		{
			PlayerManager.localPlayer.GetComponent<BanPlayer>().KickUser(UnityPlayerObject, "[LabSModAPI] Reason not given.", "Server");
		}

		public override void Disconnect(string message)
		{
			PlayerManager.localPlayer.GetComponent<BanPlayer>().KickUser(UnityPlayerObject, "[LabSModAPI] " + message, "Server");
		}

		public override Vector Get106Portal()
		{
			return new Vector(ccm.Scp106.portalPosition.x, ccm.Scp106.portalPosition.y, ccm.Scp106.portalPosition.z);
		}

		public override int GetAmmo(AmmoType type)
		{
			return ccm.GetComponent<AmmoBox>().GetAmmo((int)type);
		}

		public override string GetAuthToken()
		{
			return ccm.GetAuthToken();
		}

		public override bool GetBypassMode()
		{
			return BypassMode;
		}

		public override Smod2.API.Item GetCurrentItem()
		{
			return new LabItem(inv.GetItemInHand());
		}

		public override int GetCurrentItemIndex()
		{
			return inv.GetItemIndex();
		}

		public override object GetGameObject()
		{
			return UnityPlayerObject;
		}

		public override bool GetGhostMode()
		{
			throw new NotImplementedException(); // not in vanilla scpsl
		}

		public override bool GetGodmode()
		{
			return ccm.GodMode;
		}

		public override int GetHealth()
		{
			return -1;
		}

		public override float GetRealHealth()
		{
			return stats.health;
		}

		public override float GetFakeHealth()
		{
			return stats.NetworksyncArtificialHealth;
		}

		public override List<Smod2.API.Item> GetInventory()
		{
			List<Smod2.API.Item> items = new List<Smod2.API.Item>();
			foreach (var item in inv.items.ToList())
			{
				items.Add(new LabItem(item));
			}
			return items;
		}

		public override int GetItemIndex(Smod2.API.ItemType type)
		{
			return inv.items.FindIndex(p => p.id == (ItemType)type);
		}

		public override Vector GetPosition()
		{
			return new Vector(UnityPlayerObject.transform.position.x, UnityPlayerObject.transform.position.y, UnityPlayerObject.transform.position.z);
		}

		public override string GetRankName()
		{
			// TODO: figure out if this is the right badge
			return sroles.GlobalBadge;
		}

		public override Vector GetRotation()
		{
			return new Vector(UnityPlayerObject.transform.rotation.eulerAngles.x, UnityPlayerObject.transform.rotation.eulerAngles.y, UnityPlayerObject.transform.rotation.eulerAngles.z);
		}

		public override Smod2.API.UserGroup GetUserGroup()
		{
			// TODO
			throw new NotImplementedException();
		}

		public override Smod2.API.Item GiveItem(Smod2.API.ItemType type)
		{
			inv.AddNewItem((ItemType)type);
			return new LabItem(inv.items[GetItemIndex(type)]);
		}

		public override void HandcuffPlayer(Player playerToHandcuff)
		{
			PlayerManager.localPlayer.GetComponent<Handcuffs>().CallCmdCuffTarget(((LabPlayer)playerToHandcuff).UnityPlayerObject);
		}

		public override bool HasItem(Smod2.API.ItemType type)
		{
			return GetItemIndex(type) != -1;
		}

		public override void HideTag(bool enable)
		{
			throw new NotImplementedException();
		}

		public override void Infect(float time)
		{
			throw new NotImplementedException();
		}

		public override bool IsHandcuffed()
		{
			return UnityPlayerObject.GetComponent<Handcuffs>().NetworkCufferId != -1;
		}

		public override void Kill(DamageType type = DamageType.NUKE)
		{
			stats.HurtPlayer(new PlayerStats.HitInfo(100000000, "Server", GetDamageType(type), -1), UnityPlayerObject);
		}

		public override void PersonalBroadcast(uint duration, string message, bool isMonoSpaced)
		{
			UnityPlayerObject.GetComponent<Broadcast>().TargetAddElement(UnityPlayerObject.GetComponent<Broadcast>().connectionToClient, message, duration, isMonoSpaced);
		}

		public override void PersonalClearBroadcasts()
		{
			UnityPlayerObject.GetComponent<Broadcast>().TargetClearElements(UnityPlayerObject.GetComponent<Broadcast>().connectionToClient);
		}

		public override void RemoveHandcuffs()
		{
			UnityPlayerObject.GetComponent<Handcuffs>().NetworkCufferId = -1;
		}

		public override string[] RunCommand(string command, string[] args)
		{
			throw new NotImplementedException();
		}

		public override void SendConsoleMessage(string message, string color = "green")
		{
			ccm.TargetConsolePrint(ccm.connectionToClient, message, color);
		}

		public override void SetAmmo(AmmoType type, int amount)
		{
			UnityPlayerObject.GetComponent<AmmoBox>().SetOneAmount((int)type, amount.ToString());
		}

		public override void SetCurrentItem(Smod2.API.ItemType type)
		{
			inv.SetCurItem((ItemType)type);
		}

		public override void SetCurrentItemIndex(int index)
		{
			inv.SetCurItem(inv.items[index].id);
		}

		public override void SetGhostMode(bool ghostMode, bool visibleToSpec = true, bool visibleWhenTalking = true)
		{
			throw new NotImplementedException(); // not in vanilla scpsl
		}

		public override void SetGodmode(bool godmode)
		{
			ccm.GodMode = godmode;
		}

		public override void SetHealth(int amount, DamageType type = DamageType.NUKE)
		{
			SetHealth((float)amount, type);
		}

		public override void SetHealth(float amount, DamageType type = DamageType.NUKE)
		{
			stats.SetHPAmount((int)amount);
		}

		public override void SetRadioBattery(int battery)
		{
			// TODO: find a fix?
			throw new NotImplementedException();
		}

		public override void SetRank(string color = null, string text = null, string group = null)
		{
			// TODO
			throw new NotImplementedException();
		}

		public override void Teleport(Vector pos, bool unstuck = false)
		{
			UnityPlayerObject.GetComponent<PlyMovementSync>().OverridePosition(new Vector3(pos.x, pos.y, pos.z), 0f, unstuck);
		}

		public override void ThrowGrenade(GrenadeType grenadeType, bool isCustomDirection, Vector direction, bool isEnvironmentallyTriggered, Vector position, bool isCustomForce, float throwForce, bool slowThrow = false)
		{
			//var setting = new GrenadeSettings();
			ServerConsole.AddLog(UnityPlayerObject.GetComponent<GrenadeManager>().availableGrenades.Length.ToString());
			//UnityPlayerObject.GetComponent<GrenadeManager>().availableGrenades.Append(setting);
			//UnityPlayerObject.GetComponent<GrenadeManager>().CallCmdThrowGrenade((int)grenadeType, slowThrow, );
			throw new NotImplementedException();
		}

		public override void ThrowGrenade(Smod2.API.ItemType grenadeType, bool isCustomDirection, Vector direction, bool isEnvironmentallyTriggered, Vector position, bool isCustomForce, float throwForce, bool slowThrow = false)
		{
			throw new NotImplementedException();
		}
	}
}
