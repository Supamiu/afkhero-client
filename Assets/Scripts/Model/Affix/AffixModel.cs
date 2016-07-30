using UnityEngine;
using AFKHero.Tools;
using AFKHero.Model.Affix;
using AFKHero.Core.Affix;

namespace AFKHero.Model.Affix
{
    [System.Serializable]
    public class AffixModel
    {
        /// <summary>
        /// Constructeur pour préparer une affixe.
        /// </summary>
        public AffixModel() { }

        /// <summary>
        /// La valeur actuelle du roll de l'affixe.
        /// </summary>
        [SerializeField]
        public float value;

        /// <summary>
        /// Valeur minimale du roll de l'affixe.
        /// </summary>
        [SerializeField]
        public float minValue;

        /// <summary>
        /// Valeur maximale du roll de l'affixe.
        /// </summary>
        [SerializeField]
        public float maxValue;

        /// <summary>
        /// Type de l'affixe.
        /// </summary>
        [SerializeField]
        public AffixType type;

        public AffixModel(AffixType type, float min, float max)
        {
            minValue = min;
            maxValue = max;
            this.type = type;
        }

        /// <summary>
        /// Permet de mettre en place la valeur actuelle de notre affixe effective.
        /// </summary>
        public void Roll()
        {
            value = Mathf.Ceil(PercentageUtils.Instance.GetFloatInRange(minValue, maxValue));
        }

        /// <summary>
        /// Application de l'affixe.
        /// </summary>
        /// <param name="go"></param>
        public void OnAttach(GameObject go)
        {
            AffixEngine.Instance.AttachAffix(this, go);
        }

        /// <summary>
        /// Détachement de l'affixe.
        /// </summary>
        public void OnDetach()
        {
            AffixEngine.Instance.DetachAffix(this);
        }
    }
}
