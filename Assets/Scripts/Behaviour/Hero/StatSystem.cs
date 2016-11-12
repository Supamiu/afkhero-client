using UnityEngine;
using System.Collections.Generic;
using AFKHero.Stat;
using AFKHero.Core.Event;
using AFKHero.EventData;
using AFKHero.Core.Save;

namespace AFKHero.Behaviour.Hero
{
    public class StatSystem : MonoBehaviour, Saveable
    {
        private int points;

        private readonly Dictionary<string, AbstractStat> stats = new Dictionary<string, AbstractStat>();

        private void Start()
        {
            var currentStats = GetComponents<AbstractStat>();
            foreach (var s in currentStats)
            {
                stats.Add(s.GetName(), s);
            }
            EventDispatcher.Instance.Register(Events.Level.UP,
                new Listener<GenericGameEvent<LevelUp>>(
                    (ref GenericGameEvent<LevelUp> e) => { AddPoints(AFKHero.Config.STAT_POINTS_PER_LEVEL); }));
            EventDispatcher.Instance.Register(Events.UI.STAT_INCREASE,
                new Listener<GenericGameEvent<StatIncrease>>((ref GenericGameEvent<StatIncrease> e) =>
                {
                    if (points - e.Data.value < 0) return;
                    stats[e.Data.stat.GetName()].Add(e.Data.value);
                    RemovePoints(e.Data.value);
                    EventDispatcher.Instance.Dispatch(Events.UI.STAT_UPDATED,
                        new GenericGameEvent<AbstractStat>(stats[e.Data.stat.GetName()]));
                }));
        }

        private void AddPoints(int amount)
        {
            points += amount;
            EventDispatcher.Instance.Dispatch(Events.Stat.Points.UPDATED, new GenericGameEvent<int>(points));
        }

        private void RemovePoints(int amount)
        {
            points -= amount;
            EventDispatcher.Instance.Dispatch(Events.Stat.Points.UPDATED, new GenericGameEvent<int>(points));
        }

        public SaveData Save(SaveData data)
        {
            data.statPoints = points;
            return data;
        }

        public void Load(SaveData data)
        {
            points = data.statPoints;
            EventDispatcher.Instance.Dispatch(Events.Stat.Points.UPDATED, new GenericGameEvent<int>(points));
        }
    }
}