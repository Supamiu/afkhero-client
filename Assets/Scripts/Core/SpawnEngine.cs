using UnityEngine;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using AFKHero.Behaviour.Monster;
using AFKHero.Tools;
using AFKHero.Behaviour;
using AFKHero.Common;

namespace AFKHero.Core
{
    public class SpawnEngine : MonoBehaviour
    {
        public WorldManager worldManager;

        public float spawnInterval = 2f;

        [Range(0, 1)]
        public float spawnChances = 0.5f;

        [Header("Juste pour récupérer l'offset")]
        public Transform hero;

        private Vector3 spawnPosition;

        private float moved = 0f;

        public ScrollingScript ground;

        private List<Spawnable> spawneds = new List<Spawnable>();

        public bool spawnEnabled = true;

        /// <summary>
        /// L'offset de spawn (la distance entre le héro et le spawnEngine pour avoir des monstres bien scale en damage.
        /// </summary>
        public float offset { get; private set; }

        // Use this for initialization
        private void Start()
        {
            spawnPosition = transform.position;
            offset = Vector2.Distance(hero.position, transform.position);
        }

        private void Update()
        {
            if (!ground.moving) return;
            moved += ground.Speed * Time.deltaTime;
            if (moved >= spawnInterval && PercentageUtils.Instance.GetResult(spawnChances))
            {
                Spawn(PercentageUtils.Instance.GetItemFromPonderables(worldManager.GetSpawnList(AFKHero.GetDistance() + offset)));
                moved = 0f;
            }
            else if (moved >= spawnInterval)
            {
                moved = 0f;
            }
        }

        [SuppressMessage("ReSharper", "SuggestBaseTypeForParameter")]
        private void Spawn(Spawnable s)
        {
            if (!spawnEnabled) return;
            var spawned = Instantiate(s.gameObject, spawnPosition, Quaternion.identity) as GameObject;
            if (spawned == null) return;
            var spawn = spawned.GetComponent<Spawnable>().Init(AFKHero.GetDistance() + offset);
            var damageable = spawned.GetComponent<Damageable>();
            if (spawn.isBoss)
            {
                spawnEnabled = false;
                if (damageable != null)
                {
                    damageable.onDeath += () =>
                    {
                        spawneds.Remove(spawn);
                        spawnEnabled = true;
                    };
                }
            }
            else
            {
                if (damageable != null)
                {
                    damageable.onDeath += () =>
                    {
                        spawneds.Remove(spawn);
                    };
                }
            }
            spawneds.Add(spawn);
        }
        /// <summary>
        /// Reset entièrement le SpawnEngine.
        /// </summary>
        public void Clear()
        {
            spawneds.ForEach((s) =>
            {
                Destroy(s.gameObject);
            });
        }

        public List<Spawnable> getSpawneds()
        {
            return spawneds;
        }
    }
}
