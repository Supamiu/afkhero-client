using UnityEngine;
using AFKHero.Behaviour.Monster;
using AFKHero.Core.Event;
using AFKHero.EventData;
using AFKHero.Stat;
using AFKHero.Tools;
using AFKHero.Core;
using System.Collections.Generic;


namespace AFKHero.Behaviour.Hero
{
    [RequireComponent(typeof(Agressive))]
    public class ClickAttack : MonoBehaviour
    {
        private Camera cam;
        private Plane[] planes;
        private Agressive attacker;
        public SpawnEngine spawnEngine;
        public int maxTarget;

        // Use this for initialization
        void Start()
        {
            cam = Camera.main;
            planes = GeometryUtility.CalculateFrustumPlanes(cam);
            attacker = GetComponent<Agressive>();
            maxTarget = 1;
        }
        
        public void AttackEnnemy()
        {
            List<Spawnable> spawnables = spawnEngine.getSpawneds();
            Damageable target;
            int targetLocked = 0;

            foreach (Spawnable spawnable in spawnables)
            {
                // Si l'ennemi est visible
                if (GeometryUtility.TestPlanesAABB(planes, spawnable.gameObject.GetComponent<BoxCollider2D>().bounds))
                {
                    target = spawnable.GetComponent<Damageable>();
                    Damage damage = ((GenericGameEvent<Attack>)EventDispatcher.Instance.Dispatch("attack.compute", new GenericGameEvent<Attack>(new Attack(attacker, target)))).Data.getDamage();
                    double clickDamage = RatioEngine.Instance.GetClickDamage(damage.damage, GetComponent<Intelligence>().Value);

                    damage = new Damage(attacker, target, clickDamage, damage.critical, true);
                    EventDispatcher.Instance.Dispatch("attack.damage", new GenericGameEvent<Damage>(damage));

                    targetLocked++;
                    if (targetLocked >= maxTarget)
                    {
                        break;
                    }
                }
            }
        }
    }
}
