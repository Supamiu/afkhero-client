using UnityEngine;
using AFKHero.Inventory;
using UnityEngine.UI;
using AFKHero.Model;

namespace AFKHero.UI.Inventory
{
    public class SlotUI : MonoBehaviour
    {

        public Slot slot;

        public Image icon;

        public Text stackNumber;

        public WearableDetails wearableDetailsPopup;

        public void UpdateDisplay()
        {
            if (slot.item != null)
            {
                icon.sprite = slot.item.icon;
                icon.gameObject.SetActive(true);
                if (slot.stack > 1)
                {
                    stackNumber.text = slot.stack.ToString();
                    stackNumber.gameObject.SetActive(true);
                }
                else
                {
                    stackNumber.gameObject.SetActive(false);
                }
            }
            else
            {
                icon.gameObject.SetActive(false);
                stackNumber.gameObject.SetActive(false);
            }
        }

        public void ShowDetails()
        {
            if(slot == null)
                return;

            if (slot.item == null)
                return;

            wearableDetailsPopup.Show((Wearable)slot.item);

            Debug.Log(slot.item.GetType());
            if(slot.item.GetType() == typeof(Wearable))
            {
                wearableDetailsPopup.Show((Wearable)slot.item);
            }
        }
    }
}
