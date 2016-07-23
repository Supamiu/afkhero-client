namespace AFKHero.Model
{
    [System.Serializable]
    public class Drop
    {
        public Item item { get; private set; }   
        
        public int amount { get; set; }
        
        public float rate { get; set; }
        
        public Drop(Item item)
        {
            this.item = item;
            amount = 1;
            rate = Drop.RateForRarity(item.rarity);
        }

        public static float RateForRarity(Rarity rarity)
        {
            switch (rarity)
            {
                case Rarity.COMMON:
                    return 0.01f;
                case Rarity.RARE:
                    return 0.001f;
                case Rarity.EPIC:
                    return 0.0001f;
                case Rarity.LEGENDARY:
                    return 0.00001f;
                default:
                    return 0f;
            }
        }
    }
}
