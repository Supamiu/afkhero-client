using UnityEngine;
using AFKHero.Model;
using UnityEngine.UI;
using AFKHero.Core.Gear;
using AFKHero.Model.Affix;
using System.Collections.Generic;
using AFKHero.Inventory;
using AFKHero.UI.Tools;

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

        private Image bg;

        private List<GameObject> detailsRows = new List<GameObject>();

        public void Show(Wearable w)
        {
            gameObject.SetActive(true);
            model = w;

            icon.sprite = w.icon;
            nameText.text = w.itemName;
            mainStatText.text = (w.type == GearType.WEAPON ? "Attack" : "Defense");
            mainStatValueText.text = w.mainStat.ToString();
            descriptionText.text = w.description;
            if (detailsRows.Count < w.affixes.Count)
            {
                foreach (AffixModel affix in w.affixes)
                {
                    Text affixDetails = Instantiate(AffixTextPrefab);
                    affixDetails.text = affix.type.ToString() + (affix.value > 0 ? "+ " : "- ") + Mathf.Abs(affix.value) + "% (" + affix.minValue + " - " + affix.maxValue + ")";
                    affixDetails.transform.SetParent(affixZone.transform);
                    affixDetails.transform.localScale = Vector3.one;
                    detailsRows.Add(affixDetails.gameObject);
                }
            }
            if (model.rarity == Rarity.LEGENDARY)
            {
                string affixText = model.legendaryAffix.description.Replace("{value}", model.legendaryAffix.value.ToString());
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

        public void Hide()
        {
            gameObject.SetActive(false);
            foreach (GameObject row in detailsRows)
            {
                Destroy(row);
            }
            detailsRows.Clear();
        }

        public void Equip()
        {
            gearSystem.Equip(model);
            inventory.Remove(model);
            Hide();
        }

        public void Throw()
        {
            inventory.RemoveAll(model);
            Hide();
        }
    }
}
