using UnityEngine;
using System.Collections.Generic;

namespace AFKHero.Inventory
{
	public class ItemDatabase : ScriptableObject
	{
		[SerializeField]
		public List<Item> itemList = new List<Item> ();

		public Item GetItemByID (int id)
		{
			for (int i = 0; i < itemList.Count; i++) {
				if (itemList [i].itemID == id)
					return itemList [i].getCopy ();
			}
			return null;
		}

		public Item GetItemByName (string name)
		{
			for (int i = 0; i < itemList.Count; i++) {
				if (itemList [i].itemName.ToLower ().Equals (name.ToLower ()))
					return itemList [i].getCopy ();
			}
			return null;
		}
	}
}
