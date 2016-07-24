using AFKHero.Core.Event;
using AFKHero.Core.Tools;
using AFKHero.Model;
using AFKHero.Tools;
using System.Collections.Generic;
using UnityEngine;

namespace AFKHero.Core
{
    public class DropEngine : Singleton<DropEngine>
    {
        protected DropEngine()
        {
        }

        /// <summary>
        /// Gère les loots à la mort d'un monstre.
        /// </summary>
        /// <param name="monsterDropList"></param>
        public void Drop()
        {
            List<WearableDrop> wearableDropList = new List<WearableDrop>(GetStageWearableDropList());
            Debug.Log(wearableDropList.Count);
            foreach (WearableDrop drop in wearableDropList)
            {
                if (PercentageUtils.Instance.GetResult(drop.rate))
                {
                    Debug.Log("Drop");
                    EventDispatcher.Instance.Dispatch("drop.wearable", new GenericGameEvent<WearableDrop>(drop));
                }
            }
        }

        private List<WearableDrop> GetStageWearableDropList()
        {
            WorldManager wm = FindObjectOfType<WorldManager>();
            foreach (Stage stage in wm.GetAllStages())
            {
                if (wm.GetStageEnd(stage) > AFKHero.GetDistance())
                {
                    return stage.wearableDropList;
                }
            }
            return new List<WearableDrop>();
        }
    }
}
