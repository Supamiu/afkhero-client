using UnityEngine;
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
        [SerializeField]
        public float value { get; protected set; }

        /// <summary>
        /// Valeur minimale du roll de l'affixe.
        /// </summary>
        [SerializeField]
        public float minValue { get; set; }

        /// <summary>
        /// Valeur maximale du roll de l'affixe.
        /// </summary>
        [SerializeField]
        public float maxValue { get; set; }

        /// <summary>
        /// Le nom de l'affixe.
        /// </summary>
        [SerializeField]
        public string affixName { get; set; }

        public AffixModel(string name, float min, float max)
        {
            minValue = min;
            maxValue = max;
            affixName = name;
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
        public abstract void OnAttach(GameObject go);

        /// <summary>
        /// Détachement de l'affixe.
        /// </summary>
        public abstract void OnDetach();
    }
}
