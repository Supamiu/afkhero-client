﻿using System;
using UnityEngine;

namespace AFKHero.Model.Affix
{
    [Serializable]
    public class ItemAffix<T> : IAffix where T : AffixModel
    {
        private AffixModel affixModel;

        /// <summary>
        /// Valeur minimale du roll de l'affixe.
        /// </summary>
        public float minValue { get; private set; }

        /// <summary>
        /// Valeur maximale du roll de l'affixe.
        /// </summary>
        public float maxValue { get; private set; }

        /// <summary>
        /// Le nom de l'affixe.
        /// </summary>
        public string affixName { get; private set; }

        public float value
        {
            get
            {
                return affixModel.value;
            }
        }


        public ItemAffix(string name, float min, float max)
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
            affixModel = Activator.CreateInstance<T>();
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
