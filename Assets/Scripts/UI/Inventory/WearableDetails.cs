using System.Collections.Generic;
using System.Globalization;
using AFKHero.Core.Event;
using AFKHero.Core.Gear;
using AFKHero.Inventory;
using AFKHero.Model;
using AFKHero.Tools;
using AFKHero.UI.Tools;
using UnityEngine;
using UnityEngine.UI;

namespace AFKHero.UI.Inventory
{
    [RequireComponent(typeof(Image))]
    public class WearableDetails : MonoBehaviour
    {
        public GearSystem gearSystem;

        public InventorySystem inventory;

        public Wearable model;

        public Image icon;

        public Text nameText;

        public Text mainStatText;

        public Text mainStatValueText;

        public Text descriptionText;

        public Text AffixTextPrefab;

        public VerticalLayoutGroup affixZone;

        public Text legendaryAffixText;

        public Text upgradeText;

        public Button upgradeButton;

        public Text upgradeLevel;

        private Image bg;

        private readonly List<GameObject> detailsRows = new List<GameObject>();

        /// <summary>
        /// Met en place de layout de d�tails.
        /// </summary>
        /// <param name="w"></param>
        public void Show(Wearable w)
        {
            gameObject.SetActive(true);
            model = w;
            if (model.upgrade <= 0)
            {
                upgradeLevel.gameObject.SetActive(false);
            }
            else
            {
                upgradeLevel.gameObject.SetActive(true);
                upgradeLevel.text = "+" + model.upgrade;
            }

            icon.sprite = w.icon;
            nameText.text = w.itemName;
            mainStatText.text = w.type == GearType.WEAPON ? "Attack" : "Defense";
            mainStatValueText.text = w.mainStat.ToString();
            descriptionText.text = w.description;
            upgradeText.text = "Upgrade (" + RatioEngine.Instance.GetUpgradeCost(w).ToString(CultureInfo.CurrentCulture) + ")";

            if (model.upgrade == 12 || !model.IsUpgradeable())
            {
                upgradeButton.gameObject.SetActive(false);
            }
            else
            {
                upgradeButton.gameObject.SetActive(true);
            }

            if (RatioEngine.Instance.GetUpgradeCost(w) > inventory.dust)
            {
                upgradeButton.interactable = false;
            }
            else
            {
                upgradeButton.interactable = true;
            }

            ClearDetails();
            foreach (var affix in w.affixes)
            {
                var affixDetails = Instantiate(AffixTextPrefab);
                affixDetails.text = affix.type + (affix.value > 0 ? "+ " : "- ") + Mathf.Abs(affix.value) + "% (" + affix.minValue + " - " + affix.maxValue + ")";
                affixDetails.transform.SetParent(affixZone.transform);
                affixDetails.transform.localScale = Vector3.one;
                detailsRows.Add(affixDetails.gameObject);
            }

            if (model.rarity == Rarity.LEGENDARY)
            {
                var affixText = model.legendaryAffix.description.Replace("{value}", model.legendaryAffix.value.ToString(CultureInfo.CurrentCulture));
                legendaryAffixText.text = affixText + "(" + model.legendaryAffix.minValue + " - " + model.legendaryAffix.maxValue + ")";
                legendaryAffixText.gameObject.SetActive(true);
            }
            else
            {
                legendaryAffixText.gameObject.SetActive(false);
            }
            if (bg == null)
            {
                bg = GetComponent<Image>();
            }
            bg.color = UITools.GetItemColor(w.rarity);
        }

        /// <summary>
        /// Cache la popup.
        /// </summary>
        public void Hide()
        {
            gameObject.SetActive(false);
            ClearDetails();
        }

        public void ClearDetails()
        {
            foreach (var row in detailsRows)
            {
                Destroy(row);
            }
            detailsRows.Clear();
        }

        /// <summary>
        /// 2quipe l'item actuellement montr�.
        /// </summary>
        public void Equip()
        {
            gearSystem.Equip(model);
            inventory.Remove(model);
            Hide();
        }

        /// <summary>
        /// D�s�quipe l'item actuellement montr�.
        /// </summary>
        public void UnEquip()
        {
            gearSystem.UnEquip(model);
            Hide();
        }

        /// <summary>
        /// Jette l'item actuellement montr�.
        /// </summary>
        public void Throw()
        {
            inventory.RemoveAll(model);
            Hide();
        }

        /// <summary>
        /// Recycle l'item actuellement montr�.
        /// </summary>
        public void Dez()
        {
            inventory.Remove(model);
            EventDispatcher.Instance.Dispatch(Events.DUST, new GenericGameEvent<double>(RatioEngine.Instance.GetDust(model)));
            Hide();
        }

        /// <summary>
        /// Am�liore l'item actuellement montr�.
        /// </summary>
        public void Upgrade()
        {
            EventDispatcher.Instance.Dispatch(Events.DUST, new GenericGameEvent<double>(-1 * RatioEngine.Instance.GetUpgradeCost(model)));
            if (model.Upgrade())
            {
                upgradeLevel.text = "+" + model.upgrade;
            }
            Show(model);
            gearSystem.NotifyGearChange();
        }
    }
}
