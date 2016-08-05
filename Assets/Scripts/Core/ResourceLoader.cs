using UnityEngine;
using AFKHero.Core.Database;
using System;
using System.IO;
using AFKHero.Model;
using System.Collections.Generic;
using AFKHero.Core.Tools;

namespace AFKHero.Core
{
    /// <summary>
    /// Classe utilitaire pour récupérer des resources de façon uniforme.
    /// </summary>
    public class ResourceLoader : Singleton<ResourceLoader>
    {
        public static readonly string WORLD_DATABASE_PATH = "Databases/WorldDatabase";
        public static readonly string WEARABLE_DATABASE_PATH = "Databases/WearableDatabase";
        public static readonly string CONSUMABLE_DATABASE_PATH = "Databases/ConsumableDatabase";

        private WorldDatabase worldDB;
        private WearableDatabase wearableDB;
        private ConsumableDatabase consumableDB;

        public WorldDatabase LoadWorldDatabase()
        {
            return worldDB;
        }

        public WearableDatabase LoadWearableDatabase()
        {
            return wearableDB;
        }

        public ConsumableDatabase LoadConsumableDatabase()
        {
            return consumableDB;
        }

        void Awake()
        {
            worldDB = Load<WorldDatabase>(WORLD_DATABASE_PATH);
            wearableDB = Load<WearableDatabase>(WEARABLE_DATABASE_PATH);
            consumableDB = Load<ConsumableDatabase>(CONSUMABLE_DATABASE_PATH);
        }

        public static T Load<T>(string path) where T : UnityEngine.Object
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
