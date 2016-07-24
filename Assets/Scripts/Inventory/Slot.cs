using AFKHero.Model;

namespace AFKHero.Inventory
{
    public class Slot
    {
        public int stack { get; private set; }

        public Item item { get; private set; }

        public bool IsFree()
        {
            return item == null;
        }

        public bool Store(Item item)
        {
            if (IsFree())
            {
                this.item = item;
                return true;
            }
            else if (item.GetId() == this.item.GetId() && GetMaxStack() < stack)
            {
                stack++;
                return true;
            }
            return false;
        }

        public int GetMaxStack()
        {
            return item.GetType() == typeof(Wearable) ? 1 : 99;
        }
    }
}
