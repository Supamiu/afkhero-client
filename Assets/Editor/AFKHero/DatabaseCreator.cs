using UnityEngine;
using AFKHero.Core.Database;
using UnityEditor;

public class DatabaseCreator {

	public static WorldDatabase CreateWorldDatabase ()
	{
        WorldDatabase asset = ScriptableObject.CreateInstance<WorldDatabase> ();
		AssetDatabase.CreateAsset (asset, "Assets/Resources/Databases/WorldsDatabase.asset");
		AssetDatabase.SaveAssets ();
		return asset;
	}
}
