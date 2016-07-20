using AFKHero.Core.Gear;
using AFKHero.Model.Affix;
using System.Collections.Generic;
using UnityEngine;

namespace AFKHero.Model
{
    [System.Serializable]
    public class Wearable : Item
    {
        public List<IAffix> affixes = new List<IAffix>();

        public Sprite sprite;

        public GearType type;

        public void Attach(GameObject go)
        {
            foreach(IAffix affix in affixes)
            {
                affix.OnAttach(go);
            }
        }

        public void Detach()
        {
            foreach (IAffix affix in affixes)
            {
                affix.OnDetach();
            }
        }
    }
}
