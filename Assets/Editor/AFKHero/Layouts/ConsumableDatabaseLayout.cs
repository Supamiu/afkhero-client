using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using AFKHero.Model;
using AFKHero.Core.Database;
using AFKHero.Core;
using UnityEditor;

namespace AFKHero.EditorExtension.Layout
{
    public class ConsumableDatabaseLayout : AbstractDatabaseLayout
    {
        private int selectedInnerTab = 0;
        private Vector2 scrollPosition;
        private List<bool> managedItem = new List<bool>();
        private static Consumable createdItem = (Consumable)new Consumable().GenerateId();

        private string[] innerTabs = { "View database", "Add Consumable" };

        private ConsumableDatabase itemDatabase;

        public ConsumableDatabaseLayout()
        {
            itemDatabase = ResourceLoader.LoadConsumableDatabase();
        }

        public override void DrawDatabase()
        {
            if (itemDatabase == null)
            {
                itemDatabase = ResourceLoader.LoadConsumableDatabase();
            }
            GUILayout.BeginHorizontal();
            selectedInnerTab = GUILayout.Toolbar(selectedInnerTab, innerTabs);
            GUILayout.EndHorizontal();

            if (selectedInnerTab == 0)
            {
                //Si on est en mode view
                GUILayout.Space(10);
                if (itemDatabase.consumables.Count == 0)
                {
                    GUILayout.Label("There is no Item in the Database!");
                }
                else
                {
                    scrollPosition = GUILayout.BeginScrollView(scrollPosition);
                    for (int i = 0; i < itemDatabase.consumables.Count; i++)
                    {
                        managedItem.Add(false);
                        GUILayout.BeginVertical("Box");
                        GUILayout.BeginHorizontal();
                        managedItem[i] = EditorGUILayout.Foldout(managedItem[i], itemDatabase.consumables[i].itemName);
                        GUILayout.FlexibleSpace();
                        if (i > 0)
                        {
                            if (GUILayout.Button("▲"))
                            {//UP
                                managedItem.ForEach(e =>
                                {
                                    e = false;
                                });
                                Consumable tmp = itemDatabase.consumables[i];
                                itemDatabase.consumables[i] = itemDatabase.consumables[i - 1];
                                itemDatabase.consumables[i - 1] = tmp;
                            }
                        }
                        if (i < itemDatabase.consumables.Count - 1)
                        {
                            if (GUILayout.Button("▼"))
                            {//DOWN
                                managedItem.ForEach(e =>
                                {
                                    e = false;
                                });
                                Consumable tmp = itemDatabase.consumables[i];
                                itemDatabase.consumables[i] = itemDatabase.consumables[i + 1];
                                itemDatabase.consumables[i + 1] = tmp;
                            }
                        }
                        if (GUILayout.Button("X"))
                        {
                            itemDatabase.consumables.RemoveAt(i);
                        }
                        GUILayout.EndHorizontal();
                        if (managedItem[i])
                        { //Si on est entrain de manage cet item.
                            Consumable edit = itemDatabase.consumables[i];
                            DrawItemEdit(ref edit, true);
                        }
                        GUILayout.EndVertical();
                    }
                    GUILayout.EndScrollView();
                }
            }
            else
            {//Si on est en mode création
                DrawItemEdit(ref createdItem, false);
            }
        }

        private void DrawItemEdit(ref Consumable subject, bool edit)
        {
            GUILayout.BeginVertical("Box");
            GUILayout.Space(20);
            EditorGUILayout.LabelField("ID : " + subject.GetId().ToString());
            GUILayout.Space(10);
            subject.itemName = EditorGUILayout.TextField("Item name", subject.itemName);
            subject.description = EditorGUILayout.TextField("Item description", subject.description);
            GUILayout.BeginHorizontal();
            subject.icon = (Sprite)EditorGUILayout.ObjectField("Icon", subject.icon, typeof(Sprite), true);
            GUILayout.EndHorizontal();
            subject.rarity = (Rarity)EditorGUILayout.EnumPopup("Rarity", subject.rarity);
        }
    }
}
