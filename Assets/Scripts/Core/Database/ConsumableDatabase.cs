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

        public bool HasItem(int id)
        {
            foreach (Consumable c in consumables)
            {
                if (c.GetId() == id)
                {
                    return true;
                }
            }
            return false;
        }

        public Consumable GetItem(int id)
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
