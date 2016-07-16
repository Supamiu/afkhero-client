using UnityEngine;
using System.Collections;
using AFKHero.Model;
using AFKHero.Common;
using System.Collections.Generic;
using AFKHero.Core.Database;
using AFKHero.Behaviour.Monster;

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
        public List<World> worlds;

        [Header("Monde en cours")]
        public int currentWorld = 0;

        void Start()
        {
            this.worlds = Resources.Load<WorldDatabase>("Databases/WorldsDatabase").worlds;
            if (this.worlds.Count == 0)
            {
                Debug.LogError("WorldManager has 0 worlds, this should never happen !");
                return;
            }
            this.SetWorld(this.GetCurrentWorld());
        }

        public float GetCheckpoint()
        {
            World w = this.GetCurrentWorld();
            float stageDistance = AFKHero.WORLD_LENGTH / w.stages.Length;
            float distance = 0f;
            foreach (Stage s in w.stages)
            {
                if (s.done)
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

        public World GetCurrentWorld()
        {
            foreach (World w in this.worlds)
            {
                foreach (Stage s in w.stages)
                {
                    //Si on a dépassé ce stage, on vérifie qu'il a bien été fait.
                    if (!s.done)
                    {
                        return w;
                    }
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
            foreach (Stage s in GetAllStages())
            {
                if (GetStageEnd(s) > distance)
                {
                    //Si on est dans ce stage.
                    return s.bestiary;
                }
                else
                {
                    //Si on a dépassé ce stage, on vérifie qu'il a bien été fait.
                    if (!s.done)
                    {
                        List<Spawnable> bossSpawn = new List<Spawnable>();
                        bossSpawn.Add(s.boss);
                        return bossSpawn;
                    }
                }
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
            float distance = 0f;
            foreach (World w in this.worlds)
            {
                float stageDistance = AFKHero.WORLD_LENGTH / w.stages.Length;
                foreach (Stage s in w.stages)
                {
                    if (stage.Equals(s))
                    {
                        return distance;
                    }
                    else
                    {
                        distance += stageDistance;
                    }
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
            float distance = 0f;
            foreach (World w in this.worlds)
            {
                float stageDistance = AFKHero.WORLD_LENGTH / w.stages.Length;
                foreach (Stage s in w.stages)
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
            List<Stage> stages = new List<Stage>();
            foreach (World w in this.worlds)
            {
                foreach (Stage s in w.stages)
                {
                    stages.Add(s);
                }
            }
            return stages;
        }

        /// <summary>
        /// Met en place un monde.
        /// </summary>
        /// <param name="world"></param>
        private void SetWorld(World world)
        {
            SpriteRenderer[] firstPlanSprites = this.parallaxFirstPlan.GetComponentsInChildren<SpriteRenderer>();
            SpriteRenderer[] secondPlanSprites = this.parallaxSecondPlan.GetComponentsInChildren<SpriteRenderer>();
            SpriteRenderer[] thirdPlanSprites = this.parallaxThirdPlan.GetComponentsInChildren<SpriteRenderer>();
            foreach (SpriteRenderer s in firstPlanSprites)
            {
                s.sprite = world.parallaxFirstPlan;
            }
            foreach (SpriteRenderer s in secondPlanSprites)
            {
                s.sprite = world.parallaxSecondPlan;
            }
            foreach (SpriteRenderer s in thirdPlanSprites)
            {
                s.sprite = world.parallaxThirdPlan;
            }
        }
    }
}
