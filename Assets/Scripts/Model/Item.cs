using UnityEngine;

namespace AFKHero.Model
{
    [System.Serializable]
    public class Item
    {
        /// <summary>
        /// L'id unique de l'item.
        /// </summary>
        public int id;

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

        public Item GenerateId()
        {
            if (id == 0)
            {
                id = Random.Range(0, int.MaxValue);
            }
            return this;
        }

        public int GetId()
        {
            return id;
        }
    }
}
