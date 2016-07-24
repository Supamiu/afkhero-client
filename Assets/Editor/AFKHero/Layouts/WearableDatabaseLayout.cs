using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using AFKHero.Model;
using AFKHero.Core.Database;
using AFKHero.Core;
using AFKHero.Core.Gear;
using AFKHero.Model.Affix;
using System;

namespace AFKHero.EditorExtension.Layout
{
    public class WearableDatabaseLayout : AbstractDatabaseLayout
    {
        private int selectedInnerTab = 0;
        private Vector2 scrollPosition;
        private List<bool> managedItem = new List<bool>();
        private static Wearable createdWearable = (Wearable)new Wearable().GenerateId();

        private static readonly Dictionary<GUIContent, Type> affixTypes = new Dictionary<GUIContent, Type>()
        {
            {new GUIContent("Damage"),  typeof(DamageBonus) },
            {new GUIContent("Crit. Damage"), typeof(CritDamageBonus) },
            {new GUIContent("Crit. Chances"),  typeof(CritChancesBonus) },
            {new GUIContent("HP"),  typeof(HPBonus) }
        };

        //Création d'affixe
        private int selectedAffixTypeForCreation = 0;
        private float minValueForAffix = 0f;
        private float maxValueForAffix = 0f;
        private AffixModel createdAffix;
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
            EditorGUILayout.LabelField("ID : " + subject.GetId().ToString());
            GUILayout.Space(10);
            subject.itemName = EditorGUILayout.TextField("Wearable name", subject.itemName);
            subject.description = EditorGUILayout.TextField("Wearable description", subject.description);
            GUILayout.BeginHorizontal();
            subject.icon = (Sprite)EditorGUILayout.ObjectField("Icon", subject.icon, typeof(Sprite), true);
            subject.sprite = (Sprite)EditorGUILayout.ObjectField("Sprite (optional)", subject.sprite, typeof(Sprite), true);
            GUILayout.EndHorizontal();
            subject.rarity = (Rarity)EditorGUILayout.EnumPopup("Rarity", subject.rarity);
            subject.type = (GearType)EditorGUILayout.EnumPopup("Slot", subject.type);

            string mainStatName;
            if (subject.type == GearType.WEAPON)
            {
                mainStatName = "Attack";
            }
            else
            {
                mainStatName = "Defense";
            }
            subject.mainStat = EditorGUILayout.IntField(mainStatName, subject.mainStat);

            GUILayout.BeginVertical("Box");
            GUILayout.Label("Affixes");
            if (subject.affixes == null)
            {
                subject.affixes = new List<AffixModel>();
            }
            for (int j = 0; j < subject.affixes.Count; j++)
            {
                GUILayout.BeginHorizontal();
                GUILayout.Label(subject.affixes[j].affixName + "(" + subject.affixes[j].minValue + " - " + subject.affixes[j].maxValue + ")");
                GUILayout.FlexibleSpace();
                if (GUILayout.Button("X"))
                {
                    subject.affixes.RemoveAt(j);
                }
                GUILayout.EndHorizontal();
            }

            GUILayout.BeginVertical("Box");
            GUILayout.Space(10);
            GUILayout.Space(10);
            GUILayout.BeginHorizontal();
            GUILayout.Space(10);
            GUIContent[] affixes = new GUIContent[affixTypes.Keys.Count];
            affixTypes.Keys.CopyTo(affixes, 0);
            selectedAffixTypeForCreation = EditorGUILayout.Popup(selectedAffixTypeForCreation, affixes);
            minValueForAffix = EditorGUILayout.FloatField("Min value", minValueForAffix);
            maxValueForAffix = EditorGUILayout.FloatField("Max value", maxValueForAffix);
            GUILayout.Space(10);
            GUILayout.EndHorizontal();
            GUILayout.Space(20);
            if (GUILayout.Button("Add affix"))
            {
                if(createdWearable.affixes == null)
                {
                    createdWearable.affixes = new List<AffixModel>();
                }
                Type affixType = affixTypes[affixes[selectedAffixTypeForCreation]];
                createdAffix = (AffixModel)Activator.CreateInstance(affixType);
                createdAffix.affixName = affixes[selectedAffixTypeForCreation].text;
                createdAffix.minValue = minValueForAffix;
                createdAffix.maxValue = maxValueForAffix;
                subject.affixes.Add(createdAffix);
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
                    createdWearable = (Wearable)new Wearable().GenerateId();
                    EditorUtility.SetDirty(wearableDatabase);
                }
            }
        }


    }
}
