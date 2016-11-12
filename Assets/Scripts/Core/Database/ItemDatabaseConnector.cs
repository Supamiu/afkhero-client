using AFKHero.Core.Tools;
using AFKHero.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using AFKHero.Model.Affix;

namespace AFKHero.Core.Database
{
    public class ItemDatabaseConnector : Singleton<ItemDatabaseConnector>
    {

        public class ItemNotFoundException : Exception
        {
            public ItemNotFoundException(int id) : base("Objet non trouvé dans les bases : " + id) { }
        }

        private readonly WearableDatabase wearables = ResourceLoader.Instance.LoadWearableDatabase();
        private readonly ConsumableDatabase consumables = ResourceLoader.Instance.LoadConsumableDatabase();

        public List<Item> GetAllItems()
        {
            var allItems = wearables.wearables.Cast<Item>().ToList();
            allItems.AddRange(consumables.consumables.Cast<Item>());
            return allItems;
        }

        public Item GetItem(int id)
        {
            if (wearables.HasItem(id))
            {
                return GetWearable(id);
            }
            if (consumables.HasItem(id))
            {

            }
            throw new ItemNotFoundException(id);
        }

        public Wearable GetWearable(int id)
        {
            return CloneWearable(wearables.GetItem(id));
        }

        public Consumable GetConsumable(int id)
        {
            return CloneConsumable(consumables.GetItem(id));
        }
        
        private static Consumable CloneConsumable(Consumable c)
        {
            return c;
        }

        private static Wearable CloneWearable(Wearable w)
        {
            var wCopy = new Wearable
            {
                id = w.GetId(),
                affixPool = new List<AffixModel>()
            };
            foreach (var a in w.affixPool)
            {
                wCopy.affixPool.Add(new AffixModel(a.type, a.minValue, a.maxValue));
            }
            wCopy.legendaryAffix = w.legendaryAffix;
            wCopy.description = w.description;
            wCopy.icon = w.icon;
            wCopy.itemName = w.itemName;
            wCopy.mainStat = w.mainStat;
            wCopy.mainStatRatio = w.mainStatRatio;
            wCopy.rarity = w.rarity;
            wCopy.sprite = w.sprite;
            wCopy.type = w.type;
            return wCopy;
        }
    }
}
