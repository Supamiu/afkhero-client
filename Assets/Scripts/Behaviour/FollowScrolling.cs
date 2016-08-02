using UnityEngine;
using AFKHero.Core.Event;
using UnityStandardAssets.Utility;
using AFKHero.Common;

namespace AFKHero.Behaviour
{
    [RequireComponent(typeof(AutoMoveAndRotate))]
    public class FollowScrolling : MonoBehaviour, IOnDeath
    {
        AutoMoveAndRotate movement;

        public float relativeSpeed = 0f;

        IListener listener;
        IListener speedBonusListener;

        ScrollingScript scrolling;

        // Use this for initialization
        void Start()
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
            EventDispatcher.Instance.Register("movement.enabled", listener);
            EventDispatcher.Instance.Register("movespeed.bonus", speedBonusListener);
        }

        public void OnDeath()
        {
            EventDispatcher.Instance.Unregister("movement.enabled", listener);
            EventDispatcher.Instance.Unregister("movespeed.bonus", speedBonusListener);
        }
    }
}
