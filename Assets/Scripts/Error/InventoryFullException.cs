using System;

namespace AFKHero.Error
{
    public class InventoryFullException : Exception
    {

        public InventoryFullException() : base("Inventaire complet")
        {
        }
    }
}
