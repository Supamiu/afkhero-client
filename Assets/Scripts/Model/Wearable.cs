using System.Collections.Generic;
using UnityEngine;

namespace AFKHero.Model
{
    public abstract class Wearable : Item
    {
        List<Affix> affixes;

        public void Attach(GameObject go)
        {
            foreach(Affix affix in this.affixes)
            {
                affix.OnAttach(go);
            }
            OnAttach(go);
        }

        public abstract void OnAttach(GameObject o);

        public abstract void OnDetach();
    }
}
