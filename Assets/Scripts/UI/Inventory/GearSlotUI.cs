using UnityEngine;
using AFKHero.Inventory;
using UnityEngine.UI;
using AFKHero.Model;
using AFKHero.UI.Tools;

namespace AFKHero.UI.Inventory
{
    public class GearSlotUI : MonoBehaviour
    {
        private Image image;

        public Wearable item;

        public Image icon;

        public WearableDetails wearableDetailsPopup;

        public Sprite defaultIcon;

        public void UpdateDisplay(Wearable item)
        {
            this.item = item;
            if (image == null)
            {
                image = GetComponent<Image>();
            }
            if (item != null)
            {
                icon.sprite = item.icon;
                icon.color = Color.white;
                image.color = UITools.GetItemColor(item.rarity);
                icon.gameObject.SetActive(true);
            }
            else
            {
                icon.sprite = defaultIcon;
                icon.color = Color.black;
                image.color = Color.white;
            }
        }

        public void ShowDetails()
        {
            if (item == null)
                return;

            wearableDetailsPopup.Show(item);
        }
    }
}
