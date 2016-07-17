using UnityEngine;
using System.Collections.Generic;
using AFKHero.Model;

namespace AFKHero.Core.Database
{
    [System.Serializable]
    public class WorldDatabase : ScriptableObject
    {
        [SerializeField]
        public List<World> worlds;
    }
}
