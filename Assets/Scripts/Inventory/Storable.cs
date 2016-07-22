using UnityEngine;

namespace AFKHero.Inventory
{
    [System.Serializable]
	public class Storable
	{
		public string itemName;

		public string itemDescription;

		public int itemID;

		public int maxStack = 1;

		public Sprite itemIcon;

		public GameObject itemModel;

		public ItemType itemType;

		public Storable ()
		{
		}

		public Storable (string name, int id, string desc, Sprite icon, GameObject model, int maxStack, ItemType type)                 //function to create a instance of the Item
		{
			itemName = name;
			itemID = id;
			itemDescription = desc;
			itemIcon = icon;
			itemModel = model;
			itemType = type;
			this.maxStack = maxStack;
		}

		public Storable GetClone ()
		{
			return (Storable)MemberwiseClone();
		}
	}
}
