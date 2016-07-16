using UnityEngine;
using AFKHero.Core.Database;
using UnityEditor;

public class DatabaseManager : EditorWindow
{
    private int selectedTab = 0;

    private static string[] Tabs = { "worlds", "weapons", "items", "recipes" };

    private static WorldDatabaseLayout WorldLayout = new WorldDatabaseLayout();

    [MenuItem("AFKHero/DatabaseManager")]
    private static void Init()
    {
        GetWindow(typeof(DatabaseManager));
        WorldDatabase resourceWorldsDatabase = Resources.Load<WorldDatabase>("Databases/WorldsDatabase");
        if (resourceWorldsDatabase == null)
            DatabaseCreator.CreateWorldDatabase();
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
                WorldLayout.DrawWorldDatabase();
                break;
        }
    }
}
