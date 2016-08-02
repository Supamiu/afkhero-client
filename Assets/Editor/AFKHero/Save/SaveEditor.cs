using AFKHero.Core.Save;
using AFKHero.Core.Tools;
using AFKHero.Model;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEditor;
using UnityEngine;

public class SaveEditor : EditorWindow
{
    
    private static SaveData save;

    private static Vector2 scrollPosition;

    [MenuItem("AFKHero/Save/Editor")]
    private static void Init()
    {
        GetWindow(typeof(SaveEditor));
    }

    private void OnGUI()
    {
        if (File.Exists(Application.persistentDataPath + "/AFKHero.save"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/AFKHero.save", FileMode.Open);
            save = JsonUtility.FromJson<SaveData>(CryptoService.Xor(bf.Deserialize(file).ToString()));
            file.Close();
            DisplayLayout();
        }
        else
        {
            GUILayout.BeginVertical();
            EditorGUILayout.HelpBox("No save data !", MessageType.Error);
            if (GUILayout.Button("Create a save"))
            {
                save = new SaveData();
                Persist();
            }
            GUILayout.EndVertical();
        }
    }

    private void Persist()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/AFKHero.save");
        bf.Serialize(file, CryptoService.Xor(JsonUtility.ToJson(save)));
        file.Close();
    }

    private void DisplayLayout()
    {
        GUILayout.BeginVertical("Box");
        scrollPosition = GUILayout.BeginScrollView(scrollPosition);
        GUILayout.Label("Global", EditorStyles.centeredGreyMiniLabel);
        save.distance = EditorGUILayout.FloatField("distance", save.distance);
        GUILayout.Space(10);

        GUILayout.Label("Levels", EditorStyles.centeredGreyMiniLabel);
        save.xp = EditorGUILayout.DoubleField("xp", save.xp);
        save.level = EditorGUILayout.DoubleField("level", save.level);
        save.xpForNextLevel = EditorGUILayout.DoubleField("xp for next level", save.xpForNextLevel);
        GUILayout.Space(10);

        GUILayout.Label("Stats", EditorStyles.centeredGreyMiniLabel);
        save.vitality = EditorGUILayout.DoubleField("vitality", save.vitality);
        save.agility = EditorGUILayout.DoubleField("agility", save.agility);
        save.strength = EditorGUILayout.DoubleField("strength", save.strength);
        save.intelligence = EditorGUILayout.DoubleField("intelligence", save.intelligence);
        save.luck = EditorGUILayout.DoubleField("luck", save.luck);
        GUILayout.Space(10);


        GUILayout.Label("Inventory", EditorStyles.centeredGreyMiniLabel);
        save.capacity = EditorGUILayout.IntField("inventory capacity", save.capacity);
        save.gold = EditorGUILayout.DoubleField("gold", save.gold);

        GUILayout.Label("Wearables", EditorStyles.boldLabel);
        if (save.wearableInventory == null)
        {
            save.wearableInventory = new List<Wearable>();
        }
        int newCount = Mathf.Max(0, EditorGUILayout.IntField("size", save.wearableInventory.Count));
        while (newCount < save.wearableInventory.Count)
            save.wearableInventory.RemoveAt(save.wearableInventory.Count - 1);
        while (newCount > save.wearableInventory.Count)
        {
            save.wearableInventory.Add(new Wearable());
        }
        for (int i = 0; i < save.wearableInventory.Count; i++)
        {
            GUILayout.BeginVertical("Box");
            GUILayout.Label(save.wearableInventory[i].GetId().ToString());
            GUILayout.Label(save.wearableInventory[i].itemName);
            save.wearableInventory[i].mainStat = EditorGUILayout.IntField(save.wearableInventory[i].mainStat);
            GUILayout.EndVertical();
        }

        GUILayout.Label("Consumables", EditorStyles.boldLabel);
        if (save.consumableInventory == null)
        {
            save.consumableInventory = new List<Consumable>();
        }
        int newConsumableCount = Mathf.Max(0, EditorGUILayout.IntField("size", save.consumableInventory.Count));
        while (newConsumableCount < save.consumableInventory.Count)
            save.consumableInventory.RemoveAt(save.consumableInventory.Count - 1);
        while (newConsumableCount > save.consumableInventory.Count)
        {
            save.consumableInventory.Add(new Consumable());
        }
        for (int i = 0; i < save.consumableInventory.Count; i++)
        {
            GUILayout.BeginVertical("Box");
            GUILayout.Label(save.consumableInventory[i].GetId().ToString());
            GUILayout.Label(save.consumableInventory[i].itemName);
            //TODO
            GUILayout.EndVertical();
        }

        GUILayout.Label("Other", EditorStyles.boldLabel);
        if (save.otherInventory == null)
        {
            save.otherInventory = new List<Item>();
        }
        int newItemCount = Mathf.Max(0, EditorGUILayout.IntField("size", save.otherInventory.Count));
        while (newItemCount < save.otherInventory.Count)
            save.otherInventory.RemoveAt(save.otherInventory.Count - 1);
        while (newItemCount > save.otherInventory.Count)
        {
            save.otherInventory.Add(new Item());
        }
        for (int i = 0; i < save.otherInventory.Count; i++)
        {
            GUILayout.BeginVertical("Box");
            GUILayout.Label(save.otherInventory[i].GetId().ToString());
            GUILayout.Label(save.otherInventory[i].itemName);
            //TODO
            GUILayout.EndVertical();
        }


        GUILayout.Space(10);

        GUILayout.Label("Gear", EditorStyles.centeredGreyMiniLabel);
        for (int i = 0; i < save.gear.Length; i++)
        {
            GUILayout.BeginVertical("Box");
            GUILayout.Label(save.gear[i].GetId().ToString());
            GUILayout.Label(save.gear[i].itemName);
            //TODO
            GUILayout.EndVertical();
        }
        GUILayout.Space(10);

        GUILayout.EndScrollView();
        GUILayout.EndVertical();

        if (GUILayout.Button("Save"))
        {
            Persist();
        }
    }
}
