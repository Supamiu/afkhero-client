using UnityEngine;
using AFKHero.Behaviour.Monster;
using AFKHero.Core.Event;
using AFKHero.EventData;
using AFKHero.Stat;
using AFKHero.Tools;
using AFKHero.Core;


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
        private void Start()
        {
            cam = Camera.main;
            planes = GeometryUtility.CalculateFrustumPlanes(cam);
            attacker = GetComponent<Agressive>();
            maxTarget = 1;
        }

        public void AttackEnnemy()
        {
            var spawnables = spawnEngine.getSpawneds();
            var targetLocked = 0;

            foreach (var spawnable in spawnables)
            {
                // Si l'ennemi est visible
                if (!GeometryUtility.TestPlanesAABB(planes, spawnable.gameObject.GetComponent<BoxCollider2D>().bounds))
                    continue;
                var target = spawnable.GetComponent<Damageable>();
                var damage =
                ((GenericGameEvent<Attack>)
                    EventDispatcher.Instance.Dispatch(Events.Attack.COMPUTE,
                        new GenericGameEvent<Attack>(new Attack(attacker, target)))).Data.getDamage();
                var clickDamage = RatioEngine.Instance.GetClickDamage(damage.damage, GetComponent<Intelligence>().Value);

                damage = new Damage(attacker, target, clickDamage, damage.critical, true);
                EventDispatcher.Instance.Dispatch(Events.Attack.DAMAGE, new GenericGameEvent<Damage>(damage));

                targetLocked++;
                if (targetLocked >= maxTarget)
                {
                    break;
                }
            }
        }
    }
}