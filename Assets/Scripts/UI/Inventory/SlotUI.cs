using UnityEngine;
using AFKHero.Inventory;
using UnityEngine.UI;
using AFKHero.Model;
using AFKHero.UI.Tools;

namespace AFKHero.UI.Inventory
{
    public class SlotUI : MonoBehaviour
    {
        private Image image;

        public Slot slot;

        public Image icon;

        public Text stackNumber;

        public WearableDetails wearableDetailsPopup;

        public void UpdateDisplay()
        {
            if(image == null)
            {
                image = GetComponent<Image>();
            }
            if (slot.item != null)
            {
                icon.sprite = slot.item.icon;
                image.color = UITools.GetItemColor(slot.item.rarity);
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
                image.color = Color.white;
            }
        }

        public void ShowDetails()
        {
            if(slot == null)
                return;

            if (slot.item == null)
                return;

            wearableDetailsPopup.Show((Wearable)slot.item);
            
            if(slot.item.GetType() == typeof(Wearable))
            {
                wearableDetailsPopup.Show((Wearable)slot.item);
            }
        }
    }
}
