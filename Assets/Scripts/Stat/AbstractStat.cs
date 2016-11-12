using UnityEngine;
using AFKHero.Core.Event;
using AFKHero.Core.Save;
using AFKHero.EventData;

namespace AFKHero.Stat
{
    public abstract class AbstractStat : MonoBehaviour, Saveable
    {
        /// <summary>
        /// Montant actuel contenu dans la stat
        /// </summary>
        public double amount = 1;

        /// <summary>
        /// Ratio qui régis la relation entre le montant et la valeur servant aux calculs.
        /// </summary>
        public float ratio = 1;

        public abstract void Add(int amount);

        public abstract string GetName();

        public abstract SaveData Save(SaveData save);

        public abstract void DoLoad(SaveData data);

        public abstract StatType GetStatType();

        public abstract string GetAbreviation();

        public void Load(SaveData data)
        {
            DoLoad(data);
            EventDispatcher.Instance.Dispatch(Events.UI.STAT_UPDATED, new GenericGameEvent<AbstractStat>(this));
        }

        public double Value
        {
            get
            {
                var data =
                ((GenericGameEvent<StatCompute>)
                    EventDispatcher.Instance.Dispatch(Events.Stat.STAT_COMPUTE_BASE + GetName(),
                        new GenericGameEvent<StatCompute>(new StatCompute(gameObject, this, amount, ratio)))).Data;
                return data.amount * data.ratio;
            }
        }
    }
}