using UnityEngine;
using AFKHero.Core.Event;
using UnityStandardAssets.Utility;
using AFKHero.Common;

namespace AFKHero.Behaviour
{
    [RequireComponent(typeof(AutoMoveAndRotate))]
    public class FollowScrolling : MonoBehaviour, IOnDeath
    {
        private AutoMoveAndRotate movement;

        public float relativeSpeed;

        private IListener listener;
        private IListener speedBonusListener;

        private ScrollingScript scrolling;

        // Use this for initialization
        private void Start()
        {
            scrolling = GameObject.FindGameObjectsWithTag("Ground")[0].GetComponent<ScrollingScript>();
            movement = GetComponent<AutoMoveAndRotate>();
            movement.enabled = scrolling.moving;
            movement.moveUnitsPerSecond.value.x = -1 * (scrolling.Speed + relativeSpeed);
            listener = new Listener<GenericGameEvent<bool>>((ref GenericGameEvent<bool> e) =>
            {
                movement.enabled = e.Data;
            }, 10);

            speedBonusListener = new Listener<GenericGameEvent<float>>((ref GenericGameEvent<float> e) =>
            {
                movement.moveUnitsPerSecond.value.x = -1 * (scrolling.Speed + relativeSpeed);
            }, 10);
            EventDispatcher.Instance.Register(Events.Movement.ENABLED, listener);
            EventDispatcher.Instance.Register(Events.Stat.Movespeed.BONUS, speedBonusListener);
        }

        public void OnDeath()
        {
            EventDispatcher.Instance.Unregister(Events.Movement.ENABLED, listener);
            EventDispatcher.Instance.Unregister(Events.Stat.Movespeed.BONUS, speedBonusListener);
        }
    }
}
