using AFKHero.Core.Tools;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using AFKHero.Model;
using System;
using System.Collections.Generic;
using AFKHero.Model.Affix;

namespace AFKHero.Core.Database
{
    public class ItemDatabaseConnector : Singleton<ItemDatabaseConnector>
    {

        public class ItemNotFoundException : Exception
        {
            public ItemNotFoundException(int id) : base("Objet non trouv� dans les bases : " + id) { }
        }

        WearableDatabase wearables = ResourceLoader.LoadWearableDatabase();
        ConsumableDatabase consumables = ResourceLoader.LoadConsumableDatabase();

        public List<Item> GetAllItems()
        {
            List<Item> allItems = new List<Item>();
            foreach (Wearable w in wearables.wearables)
            {
                allItems.Add(w);
            }
            foreach (Consumable c in consumables.consumables)
            {
                allItems.Add(c);
            }
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
        
        private Consumable CloneConsumable(Consumable c)
        {
            return c;
        }

        private Wearable CloneWearable(Wearable w)
        {
            Wearable wCopy = new Wearable();
            wCopy.affixPool = new List<AffixModel>();
            foreach (AffixModel a in w.affixPool)
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
