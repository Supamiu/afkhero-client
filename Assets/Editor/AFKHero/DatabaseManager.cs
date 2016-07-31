using UnityEngine;
using AFKHero.Core.Database;
using UnityEditor;
using AFKHero.EditorExtension.Layout;
using AFKHero.Core;
using System.IO;

public class DatabaseManager : EditorWindow
{
    private int selectedTab = 0;

    private static string[] Tabs = { "worlds", "wearables", "consumables", "items"};

    private static WorldDatabaseLayout WorldLayout = new WorldDatabaseLayout();

    private static WearableDatabaseLayout WearablesLayout = new WearableDatabaseLayout();

    private static ConsumableDatabaseLayout ConsumableLayout = new ConsumableDatabaseLayout();

    [MenuItem("AFKHero/DatabaseManager")]
    private static void Init()
    {
        GetWindow(typeof(DatabaseManager));
        WorldDatabase resourceWorldsDatabase = ResourceLoader.LoadWorldDatabase();
        if (resourceWorldsDatabase == null && !File.Exists(ResourceLoader.WORLD_DATABASE_PATH))
            DatabaseCreator.CreateWorldDatabase();

        WearableDatabase resourceWearableDatabase = ResourceLoader.LoadWearableDatabase();
        if (resourceWearableDatabase == null && !File.Exists(ResourceLoader.WEARABLE_DATABASE_PATH))
            DatabaseCreator.CreateWearableDatabase();

        ConsumableDatabase resourceItemDatabase = ResourceLoader.LoadConsumableDatabase();
        if (resourceItemDatabase == null && !File.Exists(ResourceLoader.CONSUMABLE_DATABASE_PATH))
            DatabaseCreator.CreateConsumableDatabase();
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
            case "consumables":
                ConsumableLayout.DrawDatabase();
                break;
        }
    }
}
