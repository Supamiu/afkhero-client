using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using AFKHero.Inventory;

namespace AFKHero.UI.Inventory
{
	public class Item : MonoBehaviour
	{
		public Storable storable;

		public Text amount;

		public Image icon;

		// Use this for initialization
		void Start ()
		{
			this.icon.sprite = this.storable.itemIcon;
		}
	}
}
