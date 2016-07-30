using UnityEngine;
using AFKHero.Core.Database;
using System;
using System.IO;
using AFKHero.Model;
using System.Collections.Generic;

namespace AFKHero.Core
{
    /// <summary>
    /// Classe utilitaire pour récupérer des resources de façon uniforme.
    /// </summary>
    public class ResourceLoader
    {
        public static readonly string WORLD_DATABASE_PATH = "Databases/WorldDatabase";
        public static readonly string WEARABLE_DATABASE_PATH = "Databases/WearableDatabase";
        public static readonly string CONSUMABLE_DATABASE_PATH = "Databases/ConsumableDatabase";

        public static WorldDatabase LoadWorldDatabase()
        {
            return Load<WorldDatabase>(WORLD_DATABASE_PATH);
        }

        public static WearableDatabase LoadWearableDatabase()
        {
            return Load<WearableDatabase>(WEARABLE_DATABASE_PATH);
        }

        public static ConsumableDatabase LoadConsumableDatabase()
        {
            return Load<ConsumableDatabase>(CONSUMABLE_DATABASE_PATH);
        }

        private static T Load<T>(string path) where T : UnityEngine.Object
        {
            for(int trys = 0; trys < 10; trys++)
            {
                T data = Resources.Load<T>(path);
                if(data == null && File.Exists(path + ".asset"))
                {
                    continue;
                }
                return data;
            }
            throw new Exception("Resource not found : "+path);
        }
    }
}
