﻿using AFKHero.Core.Save;
using AFKHero.Model;
using Spine;
using Spine.Unity;
using Spine.Unity.Modules;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace AFKHero.Core.Gear
{
    [RequireComponent(typeof(SkeletonRenderer))]
    public class GearSystem : MonoBehaviour, Saveable
    {
        private Skeleton skeleton;

        private Dictionary<GearSlot, Wearable> currentGear;

        //Slots Spine
        [SpineSlot]
        public string weapon;
        //Fin slots Spine.

        void Start()
        {
            skeleton = GetComponent<SkeletonRenderer>().skeleton;
            //TODO gérer les slots un par un pour mettre à jour leur spineSlot.
            //Si le stuff est inexistant, on créé sa structure.
            if (currentGear == null)
            {
                currentGear = new Dictionary<GearSlot, Wearable>();
                foreach (GearSlot slot in GearSlot.Slots)
                {
                    if (slot == GearSlot.WEAPON)
                    {
                        slot.spineSlot = weapon;
                    }
                    currentGear.Add(slot, null);
                }
            }
        }

        /// <summary>
        /// Vérifie si un slot est disponible.
        /// </summary>
        /// <param name="slot"></param>
        /// <returns></returns>
        public bool IsSlotFree(GearSlot slot)
        {
            return currentGear[slot] == null;
        }

        /// <summary>
        /// Equipe un item dans son slot.
        /// </summary>
        /// <param name="wearable"></param>
        public void Equip(Wearable wearable)
        {
            Debug.Log("Equipping wearable " + wearable.itemName);
            List<GearSlot> slots = GetSlotsForType(wearable.type);
            foreach (GearSlot slot in slots)
            {
                if (currentGear[slot] != null)
                {
                    //Si le slot est occupé et que c'est le dernier disponible
                    if (slots.IndexOf(slot) == slots.Count - 1)
                    {
                        UnEquip(slot);
                    }
                    //Si le slot est occupé mais qu'un autre slot est dispo, on passe au prochain slot
                    else
                    {
                        continue;
                    }
                }
                else
                {
                    //Si c'est un item qui contient un sprite, on l'attach à notre squelette
                    if (slot.spineSlot != null)
                    {
                        skeleton.AttachUnitySprite(slot.spineSlot, wearable.sprite);
                    }
                    currentGear[slot] = wearable;
                    wearable.Attach(gameObject);
                }
            }
        }

        /// <summary>
        /// Déséquipe un item contenu dans un slot donné.
        /// </summary>
        /// <param name="slot"></param>
        public void UnEquip(GearSlot slot)
        {
            //TODO Créer cette méthode pour de vrai.
            currentGear[slot].Detach();
            currentGear[slot] = null;
        }

        /// <summary>
        /// Récupère une liste de slots disponibles pour un certain type d'équipement.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private List<GearSlot> GetSlotsForType(GearType type)
        {
            List<GearSlot> slots = new List<GearSlot>();
            foreach (KeyValuePair<GearSlot, Wearable> slot in currentGear)
            {
                if (slot.Key.type == type)
                {
                    slots.Add(slot.Key);
                }
            }
            return slots;
        }

        public SaveData Save(SaveData save)
        {
            save.gear = currentGear;
            return save;
        }

        public void Load(SaveData data)
        {
            skeleton = GetComponent<SkeletonRenderer>().skeleton;
            currentGear = data.gear;
            foreach (KeyValuePair<GearSlot, Wearable> row in currentGear)
            {
                GearSlot slot = row.Key;
                Wearable wearable = row.Value;
                if (slot == GearSlot.WEAPON)
                {
                    slot.spineSlot = weapon;
                }
                if (slot.spineSlot != null)
                {
                    skeleton.AttachUnitySprite(slot.spineSlot, wearable.sprite);
                }
                if(wearable != null)
                {
                    wearable.Attach(gameObject);
                }
            }
        }
    }
}
