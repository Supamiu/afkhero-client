using UnityEngine;
using System.Collections;
using AFKHero.Stat;
using AFKHero.Core.Event;
using AFKHero.EventData;
using System;

namespace AFKHero.Behaviour
{
	[RequireComponent (typeof(Vitality))]
	public class Regen : MonoBehaviour
	{
		float time = 0f;

		float tickInterval = 2f;

		Vitality health;

		void Start ()
		{
			this.health = GetComponent<Vitality> ();
		}

		// Update is called once per frame
		void FixedUpdate ()
		{
			this.time += Time.fixedDeltaTime;
			if (this.time >= this.tickInterval) {
				this.Tick ();
				this.time = 0f;
			}
		}

		void Tick ()
		{
			double regen = ((GenericGameEvent<double>)EventDispatcher.Instance.Dispatch ("regen.compute", new GenericGameEvent<double> (this.health.Value * AFKHero.Config.BASE_REGEN_RATIO))).Data;
			double effectiveRegen = Math.Round(this.health.heal (regen));
			if (effectiveRegen > 0) {
				EventDispatcher.Instance.Dispatch ("heal", new GenericGameEvent<Heal> (new Heal (effectiveRegen, this)));
			}
		}
	}
}
