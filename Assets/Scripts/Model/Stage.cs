using UnityEngine;
using System.Collections;
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

        public bool done = false;

        public bool Equals(Stage o)
        {
            return this.bestiary.Equals(o.bestiary) && this.boss == o.boss;
        }
    }
}
