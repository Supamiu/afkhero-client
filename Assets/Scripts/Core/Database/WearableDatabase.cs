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
    }
}
