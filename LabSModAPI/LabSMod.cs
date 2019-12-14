using Smod2.EventHandlers;
using Smod2.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LabSModAPI.API;
using Smod2;
using System.IO;
using LabSModAPI.Logging;

namespace LabSModAPI
{
	public class LabSMod
	{
		public static void Main()
		{
			PluginManager.Manager.Logger = new LabLogger();
			PluginManager.Manager.Server = new LabServer();
			PluginManager.Manager.LoadPlugins(Path.Combine(Directory.GetCurrentDirectory(), "sm_plugins"));
			On.Searching.CallCmdPickupItem += Searching_CallCmdPickupItem;
			On.Inventory.CallCmdDropItem += Inventory_CallCmdDropItem;
			On.NicknameSync.UpdateNickname += NicknameSync_UpdateNickname;
		}

		private static void Inventory_CallCmdDropItem(On.Inventory.orig_CallCmdDropItem orig, Inventory self, int itemInventoryIndex)
		{
			var evnt = new PlayerDropItemEvent(new LabPlayer(self.gameObject), new LabItem(self.items[itemInventoryIndex]), (Smod2.API.ItemType)self.items[itemInventoryIndex].id, true);
			EventManager.Manager.HandleEvent<IEventHandlerPlayerDropItem>(evnt);
			if (evnt.Allow)
			{
				if (evnt.ChangeTo != (Smod2.API.ItemType)self.items[itemInventoryIndex].id)
				{
					self.items.RemoveAt(itemInventoryIndex);
					self.items.Add(new Inventory.SyncItemInfo() { id = (ItemType)evnt.ChangeTo });
					orig(self, self.items.Count - 1);
				}
				else
				{
					orig(self, itemInventoryIndex);
				}
			}
		}

		private static void Searching_CallCmdPickupItem(On.Searching.orig_CallCmdPickupItem orig, Searching self, UnityEngine.GameObject t)
		{
			var evnt = new PlayerPickupItemEvent(new LabPlayer(self.gameObject), new LabItem(t), (Smod2.API.ItemType)t.GetComponent<Pickup>().ItemId, true);
			EventManager.Manager.HandleEvent<IEventHandlerPlayerPickupItem>(evnt);
			if (evnt.Allow)
			{
				t.GetComponent<Pickup>().info.itemId = (ItemType)evnt.ChangeTo;
				orig(self, t);
				var evnt2 = new PlayerPickupItemLateEvent(new LabPlayer(self.gameObject), new LabItem(t));
				EventManager.Manager.HandleEvent<IEventHandlerPlayerPickupItemLate>(evnt2);
			}
		}

		private static void NicknameSync_UpdateNickname(On.NicknameSync.orig_UpdateNickname orig, NicknameSync self, string n)
		{
			orig(self, n);
			EventManager.Manager.HandleEvent<IEventHandlerPlayerJoin>(new PlayerJoinEvent(new LabPlayer(self.gameObject)));
		}
	}
}
