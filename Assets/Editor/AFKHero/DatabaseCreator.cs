using UnityEngine;
using AFKHero.Core.Database;
using UnityEditor;
using AFKHero.Model;
using System.Collections.Generic;

public class DatabaseCreator {

	public static WorldDatabase CreateWorldDatabase ()
	{
        WorldDatabase asset = ScriptableObject.CreateInstance<WorldDatabase> ();
        asset.worlds = new List<World>();
		AssetDatabase.CreateAsset (asset, "Assets/Resources/Databases/WorldDatabase.asset");
		AssetDatabase.SaveAssets ();
		return asset;
	}

    public static WearableDatabase CreateWearableDatabase()
    {
        WearableDatabase asset = ScriptableObject.CreateInstance<WearableDatabase>();
        asset.wearables = new List<Wearable>();
        AssetDatabase.CreateAsset(asset, "Assets/Resources/Databases/WearableDatabase.asset");
        AssetDatabase.SaveAssets();
        return asset;
    }

    public static ConsumableDatabase CreateConsumableDatabase()
    {
        ConsumableDatabase asset = ScriptableObject.CreateInstance<ConsumableDatabase>();
        asset.consumables = new List<Consumable>();
        AssetDatabase.CreateAsset(asset, "Assets/Resources/Databases/ConsumableDatabase.asset");
        AssetDatabase.SaveAssets();
        return asset;
    }
}
