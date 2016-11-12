using UnityEngine;
using AFKHero.Model;
using System.Collections.Generic;
using System.Linq;

namespace AFKHero.Core.Database
{
    [System.Serializable]
    public class ConsumableDatabase : ScriptableObject
    {
        [SerializeField]
        public List<Consumable> consumables;

        public bool HasItem(int id)
        {
            return consumables.Any(c => c.GetId() == id);
        }

        public Consumable GetItem(int id)
        {
            return consumables.FirstOrDefault(w => w.GetId() == id);
        }
    }
}
