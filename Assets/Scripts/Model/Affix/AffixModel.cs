using UnityEngine;
using System.Collections;
using AFKHero.Tools;

namespace AFKHero.Model.Affix
{
    [System.Serializable]
    public abstract class AffixModel
    {
        /// <summary>
        /// Constructeur pour préparer une affixe.
        /// </summary>
        protected AffixModel() { }

        /// <summary>
        /// La valeur actuelle du roll de l'affixe.
        /// </summary>
        public float value { get; protected set; }

        public void Roll(float minValue, float maxValue)
        {
            value = Mathf.Ceil(PercentageUtils.Instance.GetFloatInRange(minValue, maxValue));
        }

        /// <summary>
        /// Application de l'affixe.
        /// </summary>
        /// <param name="go"></param>
        public abstract void OnAttach(GameObject go);

        /// <summary>
        /// Détachement de l'affixe.
        /// </summary>
        public abstract void OnDetach();
    }
}
