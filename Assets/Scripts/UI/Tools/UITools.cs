using UnityEngine;
using AFKHero.Model;

namespace AFKHero.UI.Tools
{
    public class UITools
    {
        public static Color GetItemColor(Rarity rarity)
        {
            switch (rarity)
            {
                case Rarity.COMMON:
                    return Color.white;
                case Rarity.MAGIC:
                    return new Color32(45, 134, 51, 255);
                case Rarity.RARE:
                    return new Color32(40, 119, 79, 255);
                case Rarity.EPIC:
                    return new Color32(62, 18, 85, 255);
                case Rarity.LEGENDARY:
                    return new Color32(128, 70, 21, 255);
                default:
                    return Color.grey;
            }
        }

    }
}
