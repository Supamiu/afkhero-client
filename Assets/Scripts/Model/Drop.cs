using System;
using UnityEngine;

namespace AFKHero.Model
{
    [Serializable]
    public class Drop<T> where T : Item
    {
        [SerializeField]
        public T item;

        [SerializeField]
        public int amount;

        [SerializeField]
        public float rate;

        public Drop(T item)
        {
            this.item = item;
        }

        public static float RateForRarity(Rarity rarity)
        {
            switch (rarity)
            {
                case Rarity.COMMON:
                    return 0.01f;
                case Rarity.RARE:
                    return 0.001f;
                case Rarity.EPIC:
                    return 0.0001f;
                case Rarity.LEGENDARY:
                    return 0.00001f;
                default:
                    return 0f;
            }
        }
    }
}
