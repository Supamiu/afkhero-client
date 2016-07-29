using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using AFKHero.Model;
using AFKHero.Core.Database;
using AFKHero.Core;
using AFKHero.Core.Gear;
using AFKHero.Model.Affix;
using AFKHEro.Model.Affix;
using AFKHero.Tools;

namespace AFKHero.EditorExtension.Layout
{
    public class WearableDatabaseLayout : AbstractDatabaseLayout
    {
        private int selectedInnerTab = 0;
        private Vector2 scrollPosition;
        private List<bool> managedItem = new List<bool>();
        private static Wearable createdWearable = (Wearable)new Wearable().GenerateId();
        private bool customRatio = false;

        //Création d'affixe
        private AffixModel createdAffix = new AffixModel();
        private LegendaryAffixModel createdLegendaryAffix = new LegendaryAffixModel();

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
            if (!customRatio)
            {
                subject.mainStatRatio = RatioEngine.Editor.GetDefaultMainStatRatio(subject.type);
            }
            customRatio = EditorGUILayout.BeginToggleGroup("Custom ratio", customRatio);
            subject.mainStatRatio = EditorGUILayout.FloatField(mainStatName + " ratio", subject.mainStatRatio);
            EditorGUILayout.EndToggleGroup();
            GUILayout.BeginVertical("Box");
            GUILayout.Label("Affixes possibles", EditorStyles.boldLabel);
            if (subject.affixPool == null)
            {
                subject.affixPool = new List<AffixModel>();
            }
            for (int j = 0; j < subject.affixPool.Count; j++)
            {
                GUILayout.BeginHorizontal();
                GUILayout.Label(subject.affixPool[j].type.ToString() + "(" + subject.affixPool[j].minValue + " - " + subject.affixPool[j].maxValue + ")");
                GUILayout.FlexibleSpace();
                if (GUILayout.Button("X"))
                {
                    subject.affixPool.RemoveAt(j);
                }
                GUILayout.EndHorizontal();
            }

            GUILayout.BeginVertical("Box");
            GUILayout.Space(10);
            GUILayout.Space(10);
            GUILayout.BeginHorizontal();
            GUILayout.Space(10);

            createdAffix.type = (AffixType)EditorGUILayout.EnumPopup("type", createdAffix.type);
            createdAffix.minValue = EditorGUILayout.FloatField("Min value", createdAffix.minValue);
            createdAffix.maxValue = EditorGUILayout.FloatField("Max value", createdAffix.maxValue);

            GUILayout.Space(10);
            GUILayout.EndHorizontal();
            GUILayout.Space(20);
            if (GUILayout.Button("Add affix"))
            {
                if (subject.affixPool == null)
                {
                    subject.affixPool = new List<AffixModel>();
                }
                subject.affixPool.Add(createdAffix);
                createdAffix = new AffixModel();
            }
            GUILayout.EndVertical();
            GUILayout.Space(10);

            if (subject.rarity == Rarity.LEGENDARY)
            {
                GUILayout.Label("Affixe Légendaire", EditorStyles.boldLabel);
                if (subject.legendaryAffix != null)
                {
                    GUILayout.Label(subject.legendaryAffix.type.ToString() + "(" + subject.legendaryAffix.minValue + " - " + subject.legendaryAffix.maxValue + ")" + " : " + subject.legendaryAffix.description );
                }
                else
                {
                    GUILayout.Label("Pas encore définie");
                }
                GUILayout.BeginVertical("Box");
                createdLegendaryAffix.type = (AffixType)EditorGUILayout.EnumPopup("type", createdLegendaryAffix.type);
                createdLegendaryAffix.minValue = EditorGUILayout.FloatField("Min value", createdLegendaryAffix.minValue);
                createdLegendaryAffix.maxValue = EditorGUILayout.FloatField("Max value", createdLegendaryAffix.maxValue);
                createdLegendaryAffix.description = EditorGUILayout.TextField("description", createdLegendaryAffix.description);
                if (GUILayout.Button("Appliquer"))
                {
                    subject.legendaryAffix = createdLegendaryAffix;
                    createdLegendaryAffix = new LegendaryAffixModel();
                }
                GUILayout.EndVertical();
            }
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
