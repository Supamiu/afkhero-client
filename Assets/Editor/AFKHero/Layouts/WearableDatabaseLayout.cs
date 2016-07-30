using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using AFKHero.Model;
using AFKHero.Core.Database;
using AFKHero.Core;
using AFKHero.Core.Gear;
using AFKHero.Model.Affix;
using AFKHero.Tools;
using AFKHero.Core.Affix;

namespace AFKHero.EditorExtension.Layout
{
    public class WearableDatabaseLayout : AbstractDatabaseLayout
    {
        private bool filterEnabled = false;
        private GearType typeFilter;
        private string nameFilter = "";
        private List<Wearable> matchingItems;


        private int selectedInnerTab = 0;
        private Vector2 scrollPosition;
        private List<bool> managedItem = new List<bool>();
        private static Wearable createdWearable = (Wearable)new Wearable().GenerateId();
        private bool customRatio = false;
        private bool customAffixPool = false;

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
                GUILayout.Space(10);
                filterEnabled = EditorGUILayout.BeginToggleGroup("Filter items", filterEnabled);
                EditorGUILayout.BeginHorizontal();
                typeFilter = (GearType)EditorGUILayout.EnumPopup("type", typeFilter);
                nameFilter = EditorGUILayout.TextField("nom", nameFilter);
                EditorGUILayout.EndHorizontal();
                EditorGUILayout.EndToggleGroup();
                if (filterEnabled)
                {
                    matchingItems = new List<Wearable>();
                    foreach (Wearable w in wearableDatabase.wearables)
                    {
                        if (w.type == typeFilter && w.itemName.Contains(nameFilter))
                        {
                            matchingItems.Add(w);
                        }
                    }
                }
                else
                {
                    matchingItems = wearableDatabase.wearables;
                }

                //Si on est en mode view
                GUILayout.Space(10);
                if (wearableDatabase.wearables.Count == 0)
                {
                    EditorGUILayout.HelpBox("There is no Item in the Database!", MessageType.Info);
                }
                else
                {
                    scrollPosition = GUILayout.BeginScrollView(scrollPosition);
                    for (int i = 0; i < matchingItems.Count; i++)
                    {
                        managedItem.Add(false);
                        GUILayout.BeginVertical("Box");
                        GUILayout.BeginHorizontal();
                        managedItem[i] = EditorGUILayout.Foldout(managedItem[i], matchingItems[i].itemName);
                        GUILayout.FlexibleSpace();

                        if (GUILayout.Button("X"))
                        {
                            if (filterEnabled)
                            {
                                wearableDatabase.wearables.RemoveAt(wearableDatabase.wearables.IndexOf(matchingItems[i]));
                            }
                            else
                            {
                                wearableDatabase.wearables.RemoveAt(i);
                            }
                        }

                        GUILayout.EndHorizontal();
                        if (managedItem[i])
                        { //Si on est entrain de manage cet item.
                            Wearable edit = matchingItems[i];
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

            if (!customAffixPool)
            {
                subject.affixPool = DefaultAffixPool.ForType(subject.type);
            }

            customAffixPool = EditorGUILayout.BeginToggleGroup("Costum affix pool", customAffixPool);
            if (subject.affixPool == null)
            {
                subject.affixPool = DefaultAffixPool.ForType(subject.type);
            }
            for (int j = 0; j < subject.affixPool.Count; j++)
            {
                GUILayout.BeginHorizontal();
                subject.affixPool[j].type = (AffixType)EditorGUILayout.EnumPopup("", subject.affixPool[j].type);
                subject.affixPool[j].minValue = EditorGUILayout.FloatField("min value", subject.affixPool[j].minValue);
                subject.affixPool[j].maxValue = EditorGUILayout.FloatField("max value", subject.affixPool[j].maxValue);
                GUILayout.FlexibleSpace();
                if (GUILayout.Button("X"))
                {
                    subject.affixPool.RemoveAt(j);
                }
                GUILayout.EndHorizontal();
            }
            EditorGUILayout.EndToggleGroup();

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
                    GUILayout.Label(subject.legendaryAffix.type.ToString() + "(" + subject.legendaryAffix.minValue + " - " + subject.legendaryAffix.maxValue + ")" + " : " + subject.legendaryAffix.description);
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
