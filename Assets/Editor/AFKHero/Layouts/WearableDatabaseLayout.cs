using UnityEngine;
using System.Collections;
using System;
using UnityEditor;
using System.Collections.Generic;
using AFKHero.Model;
using AFKHero.Core.Database;

namespace AFKHero.Editor.Layout
{
    public class WearableDatabaseLayout : AbstractDatabaseLayout
    {
        private int selectedInnerTab = 0;
        private Vector2 scrollPosition;
        private List<bool> managedItem = new List<bool>();
        private static Wearable createdWearable = new Wearable();

        private string[] innerTabs = { "View database", "Add Wearable" };

        private WearableDatabase wearableDatabase;

        public WearableDatabaseLayout()
        {
            wearableDatabase = Resources.Load<WearableDatabase>("Databases/WearableDatabase");
        }

        public override void DrawDatabase()
        {
            if (wearableDatabase == null)
            {
                wearableDatabase = Resources.Load<WearableDatabase>("Databases/WearableDatabase");
            }
            GUILayout.BeginHorizontal();
            selectedInnerTab = GUILayout.Toolbar(selectedInnerTab, innerTabs);
            GUILayout.EndHorizontal();

            if (selectedInnerTab == 0)
            {
                //Si on est en mode view
                GUILayout.Space(10);
                if (wearableDatabase.wearables.Count == 0)
                {
                    GUILayout.Label("There is no Item in the Database!");
                }
                else
                {
                    scrollPosition = GUILayout.BeginScrollView(scrollPosition);
                    for (int i = 0; i < wearableDatabase.wearables.Count; i++)
                    {
                        managedItem.Add(false);
                        GUILayout.BeginVertical("Box");
                        GUILayout.BeginHorizontal();
                        managedItem[i] = EditorGUILayout.Foldout(managedItem[i], wearableDatabase.wearables[i].itemName);
                        GUILayout.FlexibleSpace();
                        if (i > 0)
                        {
                            if (GUILayout.Button("▲"))
                            {//UP
                                managedItem.ForEach(e =>
                                {
                                    e = false;
                                });
                                Wearable tmp = wearableDatabase.wearables[i];
                                wearableDatabase.wearables[i] = wearableDatabase.wearables[i - 1];
                                wearableDatabase.wearables[i - 1] = tmp;
                            }
                        }
                        if (i < wearableDatabase.wearables.Count - 1)
                        {
                            if (GUILayout.Button("▼"))
                            {//DOWN
                                managedItem.ForEach(e =>
                                {
                                    e = false;
                                });
                                Wearable tmp = wearableDatabase.wearables[i];
                                wearableDatabase.wearables[i] = wearableDatabase.wearables[i + 1];
                                wearableDatabase.wearables[i + 1] = tmp;
                            }
                        }
                        if (GUILayout.Button("X"))
                        {
                            wearableDatabase.wearables.RemoveAt(i);
                        }
                        GUILayout.EndHorizontal();
                        if (managedItem[i])
                        { //Si on est entrain de manage cet item.
                            Wearable edit = wearableDatabase.wearables[i];
                        }
                        GUILayout.EndVertical();
                    }
                    GUILayout.EndScrollView();
                }
            }
            else
            {//Si on est en mode création
                
            }
        }

        
    }
}
