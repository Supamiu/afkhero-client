using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using AFKHero.Model;
using AFKHero.Core.Database;
using AFKHero.Core;
using AFKHero.Core.Gear;
using AFKHero.Model.Affix;
using System;

namespace AFKHero.Editor.Layout
{
    public class WearableDatabaseLayout : AbstractDatabaseLayout
    {
        private int selectedInnerTab = 0;
        private Vector2 scrollPosition;
        private List<bool> managedItem = new List<bool>();
        private static Wearable createdWearable = new Wearable();

        private static readonly Dictionary<string, Type> affixTypes = new Dictionary<string, Type>()
        {
            {"Damage",  typeof(ItemAffix<DamageBonus>) },
            {"Crit Damage", typeof(ItemAffix<CritDamageBonus>) },
            {"Crit chances",  typeof(ItemAffix<HPBonus>) }
        };

        private static readonly GUIContent[] affixes =
        {
            new GUIContent("Damage"),
            new GUIContent("Crit Damage"),
            new GUIContent("Crit chances")
        };

        //Création d'affixe
        private int selectedAffixTypeForCreation = 0;
        private float minValueForAffix = 0f;
        private float maxValueForAffix = 0f;
        private string affixName = "";
        private IAffix createdAffix;
        //Fin

        private string[] innerTabs = { "View database", "Add Wearable" };

        private WearableDatabase wearableDatabase;

        public WearableDatabaseLayout()
        {
            wearableDatabase = ResourceLoader.LoadWearableDatabase();
        }

        public override void DrawDatabase()
        {
            if (wearableDatabase == null)
            {
                wearableDatabase = ResourceLoader.LoadWearableDatabase();
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
                            DrawWearableEdit(ref edit, true);
                        }
                        GUILayout.EndVertical();
                    }
                    GUILayout.EndScrollView();
                }
            }
            else
            {//Si on est en mode création
                DrawWearableEdit(ref createdWearable, false);
            }
        }

        private void DrawWearableEdit(ref Wearable subject, bool edit)
        {
            GUILayout.BeginVertical("Box");
            GUILayout.Space(20);
            subject.itemName = EditorGUILayout.TextField("Wearable name", subject.itemName);
            subject.description = EditorGUILayout.TextField("Wearable description", subject.description);
            GUILayout.BeginHorizontal();
            subject.icon = (Sprite)EditorGUILayout.ObjectField("Icon", subject.icon, typeof(Sprite), true);
            subject.sprite = (Sprite)EditorGUILayout.ObjectField("Sprite (optional)", subject.sprite, typeof(Sprite), true);
            GUILayout.EndHorizontal();
            subject.rarity = (Rarity)EditorGUILayout.EnumPopup("Rarity", subject.rarity);
            subject.type = (GearType)EditorGUILayout.EnumPopup("Slot", subject.type);

            GUILayout.BeginVertical("Box");
            GUILayout.Label("Affixes");
            List<IAffix> list = subject.affixes;
            if (list == null)
            {
                list = new List<IAffix>();
            }
            for (int j = 0; j < list.Count; j++)
            {
                GUILayout.BeginHorizontal();
                GUILayout.Label(list[j].affixName + "(" + list[j].minValue + " - " + list[j].maxValue + ")");
                GUILayout.FlexibleSpace();
                if (GUILayout.Button("X")) {
                    list.RemoveAt(j);
                }
                GUILayout.EndHorizontal();
            }

            GUILayout.BeginVertical("Box");
            GUILayout.Space(10);
            affixName = EditorGUILayout.TextField("name", affixName);
            GUILayout.Space(10);
            GUILayout.BeginHorizontal();
            GUILayout.Space(10);
            selectedAffixTypeForCreation = EditorGUILayout.Popup(selectedAffixTypeForCreation, affixes);
            minValueForAffix = EditorGUILayout.FloatField("Min value", minValueForAffix);
            maxValueForAffix = EditorGUILayout.FloatField("Max value", maxValueForAffix);
            GUILayout.Space(10);
            GUILayout.EndHorizontal();
            GUILayout.Space(20);
            if (GUILayout.Button("Add affix"))
            {
                Type affixType = affixTypes[affixes[selectedAffixTypeForCreation].text];
                createdAffix = (IAffix)Activator.CreateInstance(affixType);
                createdAffix.affixName = affixName;
                createdAffix.minValue = minValueForAffix;
                createdAffix.maxValue = maxValueForAffix;
                createdWearable.affixes.Add(createdAffix);
                affixName = "";
                minValueForAffix = 0f;
                maxValueForAffix = 0f;
                createdAffix = null;
            }
            GUILayout.EndVertical();
            GUILayout.Space(10);
            GUILayout.EndVertical();
            GUILayout.EndVertical();
            if (!edit)
            {
                if (GUILayout.Button("Create"))
                {
                    EditorUtility.SetDirty(wearableDatabase);
                    wearableDatabase.wearables.Add(subject);
                    createdWearable = new Wearable();
                    EditorUtility.SetDirty(wearableDatabase);
                }
            }
        }


    }
}
