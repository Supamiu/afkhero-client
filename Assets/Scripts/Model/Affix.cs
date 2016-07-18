using UnityEngine;
using System.Collections;

namespace AFKHero.Model
{
    public class Affix
    {
        private Affix() { }

        /// <summary>
        /// La valeur actuelle du roll de l'affixe.
        /// </summary>
        public float value { get; private set; }

        /// <summary>
        /// Valeur minimale du roll de l'affixe.
        /// </summary>
        public float minRange { get; private set; }

        /// <summary>
        /// Valeur maximale du roll de l'affixe.
        /// </summary>
        public float maxRange { get; private set; }

        /// <summary>
        /// L'application de notre affixe.
        /// </summary>
        /// <param name="go"></param>
        public delegate void AttachListener(GameObject go);

        /// <summary>
        /// La méthode d'application, de l'affixe.
        /// </summary>
        public AttachListener OnAttach;
    }
}
