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
        float time = 0f;

        float tickInterval = 2f;

        Vitality health;

        public bool regenActive = true;

        void Start()
        {
            health = GetComponent<Vitality>();
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            time += Time.fixedDeltaTime;
            if (time >= tickInterval && regenActive)
            {
                Tick();
                time = 0f;
            }
        }

        void Tick()
        {
            double regen = health.Value * AFKHero.Config.BASE_REGEN_RATIO;
            float bonus = ((GenericGameEvent<float>)EventDispatcher.Instance.Dispatch("regen.bonus", new GenericGameEvent<float>(1f))).Data;
            regen *= bonus;
            double effectiveRegen = Math.Round(health.heal(regen));
            if (effectiveRegen > 0)
            {
                EventDispatcher.Instance.Dispatch("heal", new GenericGameEvent<Heal>(new Heal(effectiveRegen, this)));
            }
        }

        public void OnDeath()
        {
            regenActive = false;
        }
    }
}
