using AFKHero.Core.Gear;
using AFKHero.Model;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AFKHero.Core.Database
{
    [System.Serializable]
    public class WearableDatabase : ScriptableObject
    {
        [SerializeField]
        public List<Wearable> wearables;

        public Wearable GetItem(int id)
        {
            return wearables.FirstOrDefault(w => w.GetId() == id);
        }

        public bool HasItem(int id)
        {
            return wearables.Any(w => w.GetId() == id);
        }

        public List<Wearable> GetAllItemsOfType(GearType type)
        {
            return wearables.Where(w => w.type == type).ToList();
        }
    }
}
