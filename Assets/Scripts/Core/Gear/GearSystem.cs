using AFKHero.Core.Save;
using AFKHero.Inventory;
using AFKHero.Model;
using Spine;
using Spine.Unity;
using Spine.Unity.Modules;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace AFKHero.Core.Gear
{
    [RequireComponent(typeof(SkeletonRenderer))]
    [RequireComponent(typeof(InventorySystem))]
    public class GearSystem : MonoBehaviour, Saveable
    {
        private Skeleton skeleton;

        private Dictionary<GearSlot, Wearable> currentGear;

        public Action GearChangeEvent;

        private InventorySystem inventory;

        //Slots Spine
        [SpineSlot]
        public string weapon;
        //Fin slots Spine.

        void Start()
        {
            //TODO gérer les slots un par un pour mettre à jour leur spineSlot.
            //Si le stuff est inexistant, on créé sa structure.

            InitGear();
        }

        void InitGear()
        {
            inventory = GetComponent<InventorySystem>();
            skeleton = GetComponent<SkeletonRenderer>().skeleton;
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
        /// Récupère le wearable à un slot donné. 
        /// Retourne null si le slot est vide ou s'il n'existe pas.
        /// </summary>
        /// <param name="slot"></param>
        /// <returns></returns>
        public Wearable GetWearableAtSlot(GearSlot slot)
        {
            if (currentGear.ContainsKey(slot))
            {
                return currentGear[slot];
            }
            return null;
        }

        /// <summary>
        /// Equipe un item dans son slot.
        /// </summary>
        /// <param name="wearable"></param>
        public void Equip(Wearable wearable)
        {
            List<GearSlot> slots = GetSlotsForType(wearable.type);
            foreach (GearSlot slot in slots)
            {
                if (currentGear[slot] != null)
                {
                    //Si le slot est occupé et que c'est le dernier disponible
                    if (slots.IndexOf(slot) == slots.Count - 1)
                    {
                        if (UnEquip(slot))
                        {
                            Equip(wearable);
                        }
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
                    break;
                }
                NotifyGearChange();
            }
        }

        /// <summary>
        /// Déséquipe un item contenu dans un slot donné.
        /// </summary>
        /// <param name="slot"></param>
        public bool UnEquip(GearSlot slot)
        {
            if (inventory.HasFreeSlot())
            {
                currentGear[slot].Detach();
                inventory.AddItem(currentGear[slot]);
                currentGear[slot] = null;
                NotifyGearChange();
                return true;
            }
            return false;
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

        /// <summary>
        /// Notifie les listeners de l'event de changement de stuff.
        /// </summary>
        private void NotifyGearChange()
        {
            if (GearChangeEvent != null)
            {
                GearChangeEvent.Invoke();
            }
        }

        public SaveData Save(SaveData save)
        {
            Wearable[] gear = new Wearable[currentGear.Values.Count];
            currentGear.Values.CopyTo(gear, 0);
            save.gear = gear;
            return save;
        }

        public void Load(SaveData data)
        {
            InitGear();
            foreach (Wearable item in data.gear)
            {
                if (item != null)
                {
                    Equip(item);
                }
            }
        }
    }
}
