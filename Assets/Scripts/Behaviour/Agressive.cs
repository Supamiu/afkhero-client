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

        void Start()
        {
            Strength = GetComponent<Strength>();
            anim = GetComponent<SkeletonAnimation>();
            anim.state.Event += (Spine.AnimationState state, int trackIndex, Spine.Event e) =>
            {
                if (target != null && e.Data.Name == hitEvent && state.GetCurrent(trackIndex).Animation.Name == attackName)
                {
                    EventDispatcher.Instance.Dispatch("attack.damage", new GenericGameEvent<Damage>(nextDamage));
                }
            };
            //Avant le premier coup, on compute.
            anim.state.Start += (Spine.AnimationState state, int trackIndex) =>
            {
                if (state.GetCurrent(trackIndex).Animation.Name == attackName)
                {
                    ComputeDamage();
                }
            };
            //Après chaque coup, on compute le coup suivant.
            anim.state.Complete += (Spine.AnimationState state, int trackIndex, int loopCount) =>
            {
                if (state.GetCurrent(trackIndex).Animation.Name == attackName)
                {
                    ComputeDamage();
                }
            };
        }

        void OnCollisionEnter2D(Collision2D coll)
        {
            Damageable collider = coll.gameObject.GetComponent<Damageable>();
            if (collider != null)
            {
                target = collider;
                target.onDeath += OnTargetDeath;
                anim.AnimationName = attackName;
                anim.timeScale = attackAnimationScale;
            }
        }

        void OnTargetDeath()
        {
            target = null;
            anim.AnimationName = afterKillName;
            anim.skeleton.SetToSetupPose();
            anim.timeScale = 1f;
        }

        void ComputeDamage()
        {
            nextDamage = ((GenericGameEvent<Attack>)EventDispatcher.Instance.Dispatch("attack.compute", new GenericGameEvent<Attack>(new Attack(this, target)))).Data.getDamage();
        }

        public Damage getNextDamage()
        {
            return nextDamage;
        }
    }
}
