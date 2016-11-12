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
        public void Drop(List<Drop> spawnableDropList)
        {
            var dropList = new List<Drop>(GetStageDropList());
            dropList.AddRange(spawnableDropList);
            foreach (var drop in dropList)
            {
                if (!PercentageUtils.Instance.GetResult(drop.rate)) continue;
                EventDispatcher.Instance.Dispatch("drop", new GenericGameEvent<Drop>(drop));
                Handheld.Vibrate();
            }
        }

        private static IEnumerable<Drop> GetStageDropList()
        {
            var wm = FindObjectOfType<WorldManager>();
            foreach (var stage in wm.GetAllStages())
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
