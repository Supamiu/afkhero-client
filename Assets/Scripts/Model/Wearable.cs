using AFKHero.Core.Gear;
using AFKHero.Model.Affix;
using System.Collections.Generic;
using UnityEngine;

namespace AFKHero.Model
{
    [System.Serializable]
    public class Wearable : Item
    {
        public List<ItemAffix> affixes = new List<ItemAffix>();

        public Sprite sprite;

        public GearSlot slot;

        public void Attach(GameObject go)
        {
            foreach(ItemAffix affix in affixes)
            {
                affix.OnAttach(go);
            }
        }

        public void Detach()
        {
            foreach (ItemAffix affix in affixes)
            {
                affix.OnDetach();
            }
        }
    }
}
