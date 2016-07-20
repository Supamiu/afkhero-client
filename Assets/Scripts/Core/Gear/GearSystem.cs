using AFKHero.Model;
using Spine;
using Spine.Unity;
using Spine.Unity.Modules;
using System.Collections.Generic;
using UnityEngine;

namespace AFKHero.Core.Gear
{
    [RequireComponent(typeof(SkeletonRenderer))]
    public class GearSystem : MonoBehaviour
    {
        private Skeleton skeleton;

        private Dictionary<GearSlot, Wearable> currentGear = new Dictionary<GearSlot, Wearable>();

        //Slots Spine
        [SpineSlot]
        public string weapon;
        //Fin slots Spine.

        void Start()
        {
            skeleton = GetComponent<SkeletonRenderer>().skeleton;
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
                        Debug.Log(slot.spineSlot);
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
    }
}
