using UnityEngine;
using AFKHero.Core.Database;
using UnityEditor;
using AFKHero.EditorExtension.Layout;
using AFKHero.Core;

public class DatabaseManager : EditorWindow
{
    private int selectedTab = 0;

    private static string[] Tabs = { "worlds", "wearables", "consumables", "items", "recipes" };

    private static WorldDatabaseLayout WorldLayout = new WorldDatabaseLayout();

    private static WearableDatabaseLayout WearablesLayout = new WearableDatabaseLayout();

    private static ConsumableDatabaseLayout ConsumableLayout = new ConsumableDatabaseLayout();

    [MenuItem("AFKHero/DatabaseManager")]
    private static void Init()
    {
        GetWindow(typeof(DatabaseManager));
        WorldDatabase resourceWorldsDatabase = ResourceLoader.LoadWorldDatabase();
        if (resourceWorldsDatabase == null)
            DatabaseCreator.CreateWorldDatabase();

        WearableDatabase resourceWearableDatabase = ResourceLoader.LoadWearableDatabase();
        if (resourceWearableDatabase == null)
            DatabaseCreator.CreateWearableDatabase();

        ConsumableDatabase resourceItemDatabase = ResourceLoader.LoadConsumableDatabase();
        if (resourceItemDatabase == null)
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
