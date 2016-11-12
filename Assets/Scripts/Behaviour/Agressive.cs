using UnityEngine;
using Spine.Unity;
using AFKHero.Core.Event;
using AFKHero.EventData;
using AFKHero.Stat;

namespace AFKHero.Behaviour
{

    [RequireComponent(typeof(Strength))]
    public class Agressive : MonoBehaviour
    {

        private SkeletonAnimation anim;

        private Damageable target;

        public float attackAnimationScale = 1f;

        public Strength Strength { get; private set; }

        private Damage nextDamage;

        [Header("Animations")]
        [SpineEvent]
        public string hitEvent = "Hit";

        [SpineAnimation(dataField: "skeletonAnimation")]
        public string walkName = "Walk";

        [SpineAnimation(dataField: "skeletonAnimation")]
        public string attackName = "Attack";

        [SpineAnimation(dataField: "skeletonAnimation")]
        public string afterKillName = "Idle";

        private void Start()
        {
            Strength = GetComponent<Strength>();
            anim = GetComponent<SkeletonAnimation>();
            anim.state.Event += (state, trackIndex, e) =>
            {
                if (target != null && e.Data.Name == hitEvent && state.GetCurrent(trackIndex).Animation.Name == attackName)
                {
                    EventDispatcher.Instance.Dispatch(Events.Attack.DAMAGE, new GenericGameEvent<Damage>(nextDamage));
                }
            };
            //Avant le premier coup, on compute.
            anim.state.Start += (state, trackIndex) =>
            {
                if (state.GetCurrent(trackIndex).Animation.Name == attackName)
                {
                    ComputeDamage();
                }
            };
            //Après chaque coup, on compute le coup suivant.
            anim.state.Complete += (state, trackIndex, loopCount) =>
            {
                if (state.GetCurrent(trackIndex).Animation.Name == attackName)
                {
                    ComputeDamage();
                }
            };
        }

        private void OnCollisionEnter2D(Collision2D coll)
        {
            var goCollider = coll.gameObject.GetComponent<Damageable>();
            if (goCollider == null) return;
            target = goCollider;
            target.onDeath += OnTargetDeath;
            anim.AnimationName = attackName;
            anim.timeScale = attackAnimationScale;
        }

        private void OnTargetDeath()
        {
            target = null;
            anim.AnimationName = afterKillName;
            anim.skeleton.SetToSetupPose();
            anim.timeScale = 1f;
        }

        private void ComputeDamage()
        {
            nextDamage = ((GenericGameEvent<Attack>)EventDispatcher.Instance.Dispatch(Events.Attack.COMPUTE, new GenericGameEvent<Attack>(new Attack(this, target)))).Data.getDamage();
        }

        public Damage getNextDamage()
        {
            return nextDamage;
        }
    }
}
