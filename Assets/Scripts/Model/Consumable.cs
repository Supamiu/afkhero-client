using AFKHero.Model.ConsumableEffect;
using System;
using System.Collections.Generic;

namespace AFKHero.Model
{
    [Serializable]
    public class Consumable : Item
    {
        public List<AbstractConsumableEffect> effects = new List<AbstractConsumableEffect>();        
    }
}
