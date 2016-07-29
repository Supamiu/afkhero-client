using System.IO;
using UnityEditor;
using UnityEngine;

public class SaveCleaner : ScriptableObject
{
    [MenuItem("AFKHero/Save/Remove save")]
    public static void DoDeselect()
    {
        if (EditorUtility.DisplayDialog("Delete save?",
            "Are you sure you want to delete the save? this action cannot be undone.",
            "Yes",
            "No"))
            if(File.Exists(Application.persistentDataPath + "/AFKHero.save"))
            {
                File.Delete(Application.persistentDataPath + "/AFKHero.save");
            }
    }
}
