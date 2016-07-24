using UnityEngine;
using AFKHero.Model;
using UnityEngine.UI;
using AFKHero.Core.Gear;
using AFKHero.Core.Event;
using AFKHero.Model.Affix;

namespace AFKHero.UI.Inventory
{
    [RequireComponent(typeof(Image))]
    public class WearableDetails : MonoBehaviour
    {
        public Wearable model;

        public Image icon;

        public Text nameText;

        public Text mainStatText;

        public Text mainStatValueText;

        public Text descriptionText;

        public Text AffixTextPrefab;

        public VerticalLayoutGroup affixZone;

        private Image bg;

        public void Show(Wearable w)
        {
            gameObject.SetActive(true);
            model = w;

            icon.sprite = w.icon;
            nameText.text = w.itemName;
            mainStatText.text = (w.type == GearType.WEAPON ? "Attack" : "Defense");
            mainStatValueText.text = w.mainStat.ToString();
            descriptionText.text = w.description;
            foreach (AffixModel affix in w.affixes)
            {
                Debug.Log(affix.affixName);
                Text affixDetails = Instantiate(AffixTextPrefab);
                affixDetails.text = affix.affixName + (affix.value > 0 ? "+ " : "- ") + Mathf.Abs(affix.value) + "% (" + affix.minValue + " - " + affix.maxValue + ")";
                affixDetails.transform.SetParent(affixZone.transform);
            }
            if (bg == null)
            {
                bg = GetComponent<Image>();
            }
            bg.color = GetItemColor(w.rarity);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        private Color GetItemColor(Rarity rarity)
        {
            switch (rarity)
            {
                case Rarity.COMMON:
                    return Color.white;
                case Rarity.RARE:
                    return new Color(40, 119, 79);
                case Rarity.EPIC:
                    return new Color(62, 18, 85);
                case Rarity.LEGENDARY:
                    return new Color(128, 70, 21);
                default:
                    return Color.grey;
            }
        }
    }
}
