using UnityEngine;
using AFKHero.Stat;
using AFKHero.Core.Event;
using AFKHero.EventData;
using System.Linq;
using Spine.Unity;
using System;

namespace AFKHero.Behaviour
{
    [RequireComponent(typeof(Vitality))]
    public class Damageable : MonoBehaviour
    {
        public delegate void Damaged();

        public event Damaged OnDamaged;

        public bool isMortal = true;

        private Vitality vitality;

        public delegate void DeathEvent();

        public event DeathEvent onDeath;

        private SkeletonAnimation anim;

        private IListener listener;

        [SpineAnimation(dataField: "skeletonAnimation")]
        public string deathAnimation = "Die";

        [Header("Doit-on détruire le gameObject à sa mort physique?")]
        public bool destroyOnDeath = true;

        private void Start()
        {
            anim = GetComponent<SkeletonAnimation>();
            vitality = GetComponent<Vitality>();
            listener = new Listener<GenericGameEvent<Damage>>((ref GenericGameEvent<Damage> gameEvent) =>
            {
                if (gameEvent.Data.target != this) return;
                if (gameEvent.Data.hits)
                {
                    Damage(gameEvent.Data.damage);
                }
            }, 0);
            EventDispatcher.Instance.Register(Events.Attack.DAMAGE, listener);
            anim.state.Complete += (state, trackIndex, loopCount) =>
            {
                if (state.GetCurrent(trackIndex).Animation.Name == deathAnimation)
                {
                    Die();
                }
            };
        }

        private void Damage(double amount)
        {
            amount = Math.Round(amount);
            vitality.currentHp -= amount;
            if (vitality.currentHp <= 0 && isMortal)
            {
                anim.loop = false;
                if (anim.AnimationName != null)
                {
                    anim.AnimationName = deathAnimation;
                }else
                {
                    Die();
                }
            }
            if (OnDamaged != null)
            {
                OnDamaged.Invoke();
            }
        }

        private void Die()
        {
            var deathListeners = gameObject.GetComponents<Component>().OfType<IOnDeath>();
            foreach (var dlistener in deathListeners)
            {
                dlistener.OnDeath();
            }
            EventDispatcher.Instance.Unregister(Events.Attack.DAMAGE, listener);
            if (onDeath != null)
            {
                onDeath.Invoke();
            }
            if (destroyOnDeath)
            {
                Destroy(gameObject);
            }
        }
    }
}
