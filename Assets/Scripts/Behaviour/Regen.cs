using UnityEngine;
using AFKHero.Stat;
using AFKHero.Core.Event;
using AFKHero.EventData;
using System;

namespace AFKHero.Behaviour
{
    [RequireComponent(typeof(Vitality))]
    public class Regen : MonoBehaviour, IOnDeath
    {
        private float time;

        private const float tickInterval = 2f;

        private Vitality health;

        public bool regenActive = true;

        private void Start()
        {
            health = GetComponent<Vitality>();
        }

        // Update is called once per frame
        private void FixedUpdate()
        {
            time += Time.fixedDeltaTime;
            if (!(time >= tickInterval) || !regenActive) return;
            Tick();
            time = 0f;
        }

        private void Tick()
        {
            var regen = health.Value * AFKHero.Config.BASE_REGEN_RATIO;
            var bonus = ((GenericGameEvent<float>)EventDispatcher.Instance.Dispatch(Events.Stat.Regen.BONUS, new GenericGameEvent<float>(1f))).Data;
            regen *= bonus;
            var effectiveRegen = Math.Round(health.heal(regen));
            if (effectiveRegen > 0)
            {
                EventDispatcher.Instance.Dispatch(Events.HEAL, new GenericGameEvent<Heal>(new Heal(effectiveRegen, this)));
            }
        }

        public void OnDeath()
        {
            regenActive = false;
        }
    }
}
