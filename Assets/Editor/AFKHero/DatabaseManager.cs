using UnityEngine;
using AFKHero.Core.Database;
using UnityEditor;
using AFKHero.Editor.Layout;

public class DatabaseManager : EditorWindow
{
    private int selectedTab = 0;

    private static string[] Tabs = { "worlds", "wearables", "items", "recipes" };

    private static WorldDatabaseLayout WorldLayout = new WorldDatabaseLayout();

    private static WearableDatabaseLayout WearablesLayout = new WearableDatabaseLayout();

    [MenuItem("AFKHero/DatabaseManager")]
    private static void Init()
    {
        GetWindow(typeof(DatabaseManager));
        WorldDatabase resourceWorldsDatabase = Resources.Load<WorldDatabase>("Databases/WorldDatabase");
        if (resourceWorldsDatabase == null)
            DatabaseCreator.CreateWorldDatabase();
        WearableDatabase resourceWearableDatabase = Resources.Load<WearableDatabase>("Databases/WearableDatabase");
        if (resourceWearableDatabase == null)
            DatabaseCreator.CreateWearableDatabase();
    }

    private void OnGUI()
    {
        selectedTab = GUILayout.Toolbar(selectedTab, Tabs);
        DrawDatabaseMenu();
    }

    private void DrawDatabaseMenu()
    {
        string databaseName = Tabs[selectedTab];
        switch (databaseName)
        {
            case "worlds":
                WorldLayout.DrawDatabase();
                break;
            case "wearables":
                WearablesLayout.DrawDatabase();
                break;
        }
    }
}
