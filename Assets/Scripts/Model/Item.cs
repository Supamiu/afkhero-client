using UnityEngine;

namespace AFKHero.Model
{
    [System.Serializable]
    public class Item
    {
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
    }
}