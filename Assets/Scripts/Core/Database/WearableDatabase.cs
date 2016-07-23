using AFKHero.Core.Gear;
using AFKHero.Model;
using System.Collections.Generic;
using UnityEngine;

namespace AFKHero.Core.Database
{
    [System.Serializable]
    public class WearableDatabase : ScriptableObject
    {
        [SerializeField]
        public List<Wearable> wearables;

        public Wearable GetItem(uint id)
        {
            foreach(Wearable w in wearables)
            {
                if(w.id == id)
                {
                    return w;
                }
            }
            return null;
        }

        public List<Wearable> GetAllItemsOfType(GearType type)
        {
            List<Wearable> res = new List<Wearable>();
            foreach(Wearable w in wearables)
            {
                if(w.type == type)
                {
                    res.Add(w);
                }
            }
            return res;
        }
    }
}
