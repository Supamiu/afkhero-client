using UnityEngine;
using System.Collections.Generic;
using AFKHero.Behaviour.Monster;

namespace AFKHero.Model
{
    [System.Serializable]
    public class Stage
    {
        [Header("Les monstres du palier")]
        public List<Spawnable> bestiary;

        [Header("Le boss du palier")]
        public Spawnable boss;

        [Header("Les items équipables en loot dans le palier")]
        public List<Drop> dropList;

        public bool done = false;

        public bool Equals(Stage o)
        {
            return bestiary.Equals(o.bestiary) && boss == o.boss;
        }
    }
}
