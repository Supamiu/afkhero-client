using UnityEngine;
using AFKHero.Model;
using AFKHero.Common;
using System.Collections.Generic;
using System.Linq;
using AFKHero.Behaviour.Monster;
using AFKHero.Core.Event;
using AFKHero.Core.Save;

namespace AFKHero.Core
{
    public class WorldManager : MonoBehaviour
    {

        [Header("1er plan du parallax")]
        public ScrollingScript parallaxFirstPlan;

        [Header("2nd plan du parallax")]
        public ScrollingScript parallaxSecondPlan;

        [Header("3e plan du parallax")]
        public ScrollingScript parallaxThirdPlan;

        [Header("Mondes existants")]
        private List<World> worlds;

        private void Start()
        {
            worlds = new List<World>(ResourceLoader.Instance.LoadWorldDatabase().worlds);
            if (worlds.Count == 0)
            {
                Debug.LogError("WorldManager has 0 worlds, this should never happen !");
                return;
            }
            SetWorld(GetCurrentWorld());
            EventDispatcher.Instance.Register("boss.killed", new Listener<GenericGameEvent<Spawnable>>((ref GenericGameEvent<Spawnable> e) =>
            {
                foreach(var s in GetAllStages())
                {
                    if(GetStageEnd(s) <= e.Data.Distance)
                    {
                        Progression.Instance.StageDone(s, e.Data.Distance);
                    }
                }
            }));
        }

        public float GetCheckpoint()
        {
            var w = GetCurrentWorld();
            var stageDistance = AFKHero.WORLD_LENGTH / w.stages.Length;
            var distance = 0f;
            foreach (var s in w.stages)
            {
                if (Progression.Instance.IsStageDone(s, distance))
                {
                    distance += stageDistance;
                }
                else
                {
                    return distance;
                }
            }
            return distance;
        }

        public int GetCurrentWorldIndex()
        {
            return worlds.IndexOf(GetCurrentWorld());
        }

        public World GetCurrentWorld()
        {
            foreach (var w in worlds)
            {
                if (w.stages.Any(s => !Progression.Instance.IsStageDone(s, GetStageEnd(s))))
                {
                    return w;
                }
            }
            throw new System.Exception("Empty worlds or Game finished.");
        }

        /// <summary>
        /// Récupère la liste de spazwns pour une distance donnée.
        /// </summary>
        /// <param name="distance"></param>
        /// <returns></returns>
        public List<Spawnable> GetSpawnList(float distance)
        {
            foreach (var s in GetAllStages())
            {
                if (GetStageEnd(s) > distance)
                {
                    //Si on est dans ce stage.
                    return s.bestiary;
                }
                //Si on a dépassé ce stage, on vérifie qu'il a bien été fait.
                if (Progression.Instance.IsStageDone(s, GetStageEnd(s))) continue;
                var bossSpawn = new List<Spawnable> {s.boss};
                return bossSpawn;
            }
            throw new System.Exception("Empty world or Game finished.");
        }

        /// <summary>
        /// Récupère la début d'un stage.
        /// </summary>
        /// <param name="stage"></param>
        /// <returns></returns>
        public float GetStageStart(Stage stage)
        {
            var distance = 0f;
            foreach (var w in worlds)
            {
                var stageDistance = AFKHero.WORLD_LENGTH / w.stages.Length;
                foreach (var s in w.stages)
                {
                    if (stage.Equals(s))
                    {
                        return distance;
                    }
                    distance += stageDistance;
                }
            }
            return distance;
        }

        /// <summary>
        /// Récupère la fin d'un stage.
        /// </summary>
        /// <param name="stage"></param>
        /// <returns></returns>
        public float GetStageEnd(Stage stage)
        {
            var distance = 0f;
            foreach (var w in worlds)
            {
                var stageDistance = AFKHero.WORLD_LENGTH / w.stages.Length;
                foreach (var s in w.stages)
                {
                    distance += stageDistance;
                    if (stage.Equals(s))
                    {
                        return distance;
                    }
                }
            }
            return distance;
        }

        /// <summary>
        /// Récupère la liste de tous les stages.
        /// </summary>
        /// <returns></returns>
        public List<Stage> GetAllStages()
        {
            return worlds.SelectMany(w => w.stages).ToList();
        }

        /// <summary>
        /// Met en place un monde.
        /// </summary>
        /// <param name="world"></param>
        private void SetWorld(World world)
        {
            var firstPlanSprites = parallaxFirstPlan.GetComponentsInChildren<SpriteRenderer>();
            var secondPlanSprites = parallaxSecondPlan.GetComponentsInChildren<SpriteRenderer>();
            var thirdPlanSprites = parallaxThirdPlan.GetComponentsInChildren<SpriteRenderer>();
            foreach (var s in firstPlanSprites)
            {
                s.sprite = world.parallaxFirstPlan;
            }
            foreach (var s in secondPlanSprites)
            {
                s.sprite = world.parallaxSecondPlan;
            }
            foreach (var s in thirdPlanSprites)
            {
                s.sprite = world.parallaxThirdPlan;
            }
        }
    }
}
