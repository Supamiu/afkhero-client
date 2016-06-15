﻿using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using AFKHero.Stat;
using AFKHero.Core.Event;
using AFKHero.EventData;

namespace AFKHero.Behaviour.Hero
{
	public class StatSystem : MonoBehaviour
	{
		private int points;

		private Dictionary<string, AbstractStat> stats = new Dictionary<string, AbstractStat>();

		void Start(){
			AbstractStat[] stats = GetComponents<AbstractStat> ();
			foreach (AbstractStat s in stats) {
				this.stats.Add (s.GetName (), s);
			}
			EventDispatcher.Instance.Register("level.up", new Listener<GenericGameEvent<LevelUp> >((ref GenericGameEvent<LevelUp>  e) => {
				this.AddPoints(AFKHero.Config.STAT_POINTS_PER_LEVEL);
			}));
			EventDispatcher.Instance.Register("ui.stat.increase", new Listener<GenericGameEvent<StatIncrease>>((ref GenericGameEvent<StatIncrease> e) => {
				if(this.points - e.Data.value >= 0){
					this.stats[e.Data.stat.GetName()].amount += e.Data.value;
					this.RemovePoints(e.Data.value);
					EventDispatcher.Instance.Dispatch("ui.stat.updated", new GenericGameEvent<AbstractStat>(this.stats[e.Data.stat.GetName()]));
				}
			}));
		}

		void AddPoints(int amount){
			this.points += amount;
			EventDispatcher.Instance.Dispatch ("stat.points.updated", new GenericGameEvent<int>(this.points));
		}

		void RemovePoints(int amount){
			this.points -= amount;
			EventDispatcher.Instance.Dispatch ("stat.points.updated", new GenericGameEvent<int>(this.points));
		}


	}
}
