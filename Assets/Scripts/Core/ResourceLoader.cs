using UnityEngine;
using AFKHero.Core.Database;

namespace AFKHero.Core
{
    /// <summary>
    /// Classe utilitaire pour récupérer des resources de façon uniforme.
    /// </summary>
    public class ResourceLoader
    {
        public static WorldDatabase LoadWorldDatabase()
        {
            return Resources.Load<WorldDatabase>("Databases/WorldDatabase");
        }

        public static WearableDatabase LoadWearableDatabase()
        {
            return Resources.Load<WearableDatabase>("Databases/WearableDatabase");
        }

        public static ConsumableDatabase LoadConsumableDatabase()
        {
            return Resources.Load<ConsumableDatabase>("Databases/ConsumableDatabase");
        }
    }
}
