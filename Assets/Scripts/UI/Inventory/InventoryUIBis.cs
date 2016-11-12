using AFKHero.Common;
using AFKHero.Inventory;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace AFKHero.UI.Inventory
{
	public class InventoryUIBis : MonoBehaviour
	{
		public GameObject slotPrefab;

		public WearableDetails prefab;

		public InventorySystem inventorySystem;

		public HorizontalLayoutGroup content;

		public Text dustValue;

		private readonly GameObject[] currentSlots = new GameObject[0];

		private Action OnContentChanged;

	    private void Awake()
		{
			OnContentChanged = () =>
			{
				if (inventorySystem.slots.Length != currentSlots.Length)
				{
					SetCapacity();
				}
			    // ReSharper disable once EmptyForStatement
				for (var i = 0; i < inventorySystem.capacity - 1; i++)
				{
					
					//currentSlots[i].Show(inventorySystem.slots[i].item);
					//currentSlots[i].slot = inventorySystem.slots[i];
					//if(currentSlots[i].wearableDetailsPopup == null)
					//{
					//	currentSlots[i].wearableDetailsPopup = prefab;
					//}
					//currentSlots[i].UpdateDisplay();
				}
				dustValue.text = Formatter.Format(inventorySystem.dust);
			};
			inventorySystem.OnContentChanged += OnContentChanged;
		}

	    private void OnEnable()
		{
			OnContentChanged.Invoke();
		}

	    /// <summary>
	    /// Met � jout la capacit� de l'inventaire.
	    /// </summary>
	    private static void SetCapacity()
		{
			/*
			GameObject[] newSlots = new GameObject[capacity];
			currentSlots.CopyTo(newSlots, 0);
			for (int i = 0; i < newSlots.Length; i++)
			{
				GameObject newSlot = Instantiate(slotPrefab);
				newSlot.transform.SetParent(content.transform);
				newSlot.transform.localScale = Vector3.one;
				newSlots[i] = newSlot;
			}
			currentSlots = newSlots;
			*/
		}
	}
}
