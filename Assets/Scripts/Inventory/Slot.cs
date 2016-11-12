using AFKHero.Model;

namespace AFKHero.Inventory
{
    [System.Serializable]
    public class Slot
    {
        public int stack { get; private set; }

        public Item item { get; private set; }

        public bool IsFree()
        {
            return item == null;
        }

        public void Empty()
        {
            item = null;
        }

        public void RemoveOne()
        {
            stack--;
        }

        public bool Store(Item pItem)
        {
            if (IsFree())
            {
                item = pItem;
                return true;
            }
            if (pItem.GetId() != item.GetId() || GetMaxStack() >= stack) return false;
            stack++;
            return true;
        }

        public int GetMaxStack()
        {
            return item.GetType() == typeof(Wearable) ? 1 : 99;
        }
    }
}
