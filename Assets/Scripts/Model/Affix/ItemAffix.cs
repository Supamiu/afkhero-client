using System;
using UnityEngine;

namespace AFKHero.Model.Affix
{
    [System.Serializable]
    public class ItemAffix
    {
        private AffixModel affixModel;

        private Type affixType;

        /// <summary>
        /// Valeur minimale du roll de l'affixe.
        /// </summary>
        public float minValue { get; private set; }

        /// <summary>
        /// Valeur maximale du roll de l'affixe.
        /// </summary>
        public float maxValue { get; private set; }


        public ItemAffix(Type affixType, float min, float max)
        {
            minValue = min;
            maxValue = max;
            this.affixType = affixType;
        }

        /// <summary>
        /// Permet de mettre en place la valeur actuelle de notre affixe effective.
        /// </summary>
        public void Roll()
        {
            affixModel = (AffixModel)Activator.CreateInstance(affixType);
            affixModel.Roll(minValue, maxValue);
        }

        public void OnAttach(GameObject go)
        {
            affixModel.OnAttach(go);
        }

        public void OnDetach()
        {
            affixModel.OnDetach();
        }
    }
}
