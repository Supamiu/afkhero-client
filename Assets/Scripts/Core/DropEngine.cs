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
            List<Drop> dropList = new List<Drop>(GetStageDropList());
            foreach (Drop drop in dropList)
            {
                if (PercentageUtils.Instance.GetResult(drop.rate))
                {
                    EventDispatcher.Instance.Dispatch("drop", new GenericGameEvent<Drop>(drop));
                    Handheld.Vibrate();
                }
            }
        }

        private List<Drop> GetStageDropList()
        {
            WorldManager wm = FindObjectOfType<WorldManager>();
            foreach (Stage stage in wm.GetAllStages())
            {
                if (wm.GetStageEnd(stage) > AFKHero.GetDistance())
                {
                    return stage.dropList;
                }
            }
            return new List<Drop>();
        }
    }
}
