using System;
using AFKHero.Core.Save;
using AFKHero.EventData;
using UnityEngine;

namespace AFKHero.Stat
{
    public class Defense : AbstractStat
    {
        public override void Add(int amount){}

        public override void DoLoad(SaveData data){}

        public override string GetName()
        {
            return "Defense";
        }

        public override StatType GetStatType()
        {
            return StatType.SECONDARY;
        }

        public override SaveData Save(SaveData save)
        {
            return save;
        }
    }
}
