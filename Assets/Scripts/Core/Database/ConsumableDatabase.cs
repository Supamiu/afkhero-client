using UnityEngine;
using AFKHero.Model;
using System.Collections.Generic;

namespace AFKHero.Core.Database
{
    [System.Serializable]
    public class ConsumableDatabase : ScriptableObject
    {
        [SerializeField]
        public List<Consumable> consumables;

        public Consumable GetItem(uint id)
        {
            foreach (Consumable w in consumables)
            {
                if (w.GetId() == id)
                {
                    return w;
                }
            }
            return null;
        }
    }
}
