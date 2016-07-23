using UnityEngine;
using System;

namespace AFKHero.Model
{
    [Serializable]
    public class Item
    {
        /// <summary>
        /// L'id unique de l'item.
        /// </summary>
        public double id { get; private set; }

        /// <summary>
        /// L'icône pour le display.
        /// </summary>
        public Sprite icon;

        /// <summary>
        /// Le nom de l'objet.
        /// </summary>
        public string itemName;

        /// <summary>
        /// La rareté de l'objet.
        /// </summary>
        public Rarity rarity;

        /// <summary>
        /// La description de l'objet.
        /// </summary>
        public string description;

        public Item()
        {
            System.Random random = new System.Random();
            uint thirtyBits = (uint)random.Next(1 << 30);
            uint twoBits = (uint)random.Next(1 << 2);
            id = (thirtyBits << 2) | twoBits;
        }
    }
}
