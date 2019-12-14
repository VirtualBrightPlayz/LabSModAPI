using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Smod2.API;
using UnityEngine;

namespace LabSModAPI.API
{
	public class LabItem : Smod2.API.Item
	{
		//internal Item _item;
		internal Inventory.SyncItemInfo _item;
		internal GameObject go;

		public LabItem(Inventory.SyncItemInfo info)
		{
			_item = info;
		}

		public LabItem(GameObject pickup)
		{
			go = pickup;
			_item = new Inventory.SyncItemInfo() {
				id = pickup.GetComponent<Pickup>().info.itemId,
				durability = pickup.GetComponent<Pickup>().info.durability,
				modSight = pickup.GetComponent<Pickup>().info.weaponMods[0],
				modBarrel = pickup.GetComponent<Pickup>().info.weaponMods[1],
				modOther = pickup.GetComponent<Pickup>().info.weaponMods[2]
			};
		}

		public override bool InWorld { get => go != null; }

		public override Smod2.API.ItemType ItemType { get => (Smod2.API.ItemType)(int)_item.id; }

		public override bool IsWeapon { get => IsWeaponFunc(); }

		public bool IsWeaponFunc()
		{
			switch (_item.id)
			{
				case global::ItemType.GunCOM15:
					return true;
				case global::ItemType.GunMP7:
					return true;
				case global::ItemType.GunLogicer:
					return true;
				case global::ItemType.GunE11SR:
					return true;
				case global::ItemType.GunProject90:
					return true;
				case global::ItemType.GunUSP:
					return true;
				default:
					return false;
			}
		}

		public override void Drop()
		{
			if (!InWorld)
			{
				go = Pickup.Inv.SetPickup(_item.id, _item.durability, Vector3.zero, Quaternion.identity, 0, 0, 0).gameObject;
			}
		}

		public override object GetComponent()
		{
			if (InWorld)
				return go.GetComponent<Pickup>();
			return null;
		}

		public override bool GetKinematic()
		{
			return go.GetComponent<Rigidbody>().isKinematic;
		}

		public override Vector GetPosition()
		{
			return new Vector(go.transform.position.x, go.transform.position.y, go.transform.position.z);
		}

		public override void Remove()
		{
			go.GetComponent<Pickup>().Delete();
		}

		public override void SetKinematic(bool doPhysics)
		{
			go.GetComponent<Rigidbody>().isKinematic = doPhysics;
		}

		public override void SetPosition(Vector pos)
		{
			go.transform.position = new Vector3(pos.x, pos.y, pos.z);
		}

		public override Weapon ToWeapon()
		{
			throw new NotImplementedException();
		}
	}
}
