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
		AssetDatabase.CreateAsset (asset, "Assets/Resources/Databases/WorldsDatabase.asset");
		AssetDatabase.SaveAssets ();
		return asset;
	}
}
