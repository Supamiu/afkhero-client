using System.Collections.Generic;
using AFKHero.Behaviour.Monster;
using AFKHero.Model;
using AFKHero.Core.Database;
using UnityEditor;
using UnityEngine;
using AFKHero.Core;

namespace AFKHero.EditorExtension.Layout
{
    public class WorldDatabaseLayout : AbstractDatabaseLayout
    {
        private int worldSelectedInnerTab = 0;
        private Vector2 worldScrollPosition;
        private List<bool> worldManageItem = new List<bool>();
        private static World createdWorld = new World();

        private string[] innerTabs = { "View database", "Add World" };

        private WorldDatabase worldsDatabase;
        private WearableDatabase wdb;

        //Drops
        private Dictionary<GUIContent, Item> items = new Dictionary<GUIContent, Item>();
        private int selectedItemIndex;
        private GUIContent[] itemNames;

        public WorldDatabaseLayout()
        {
            wdb = ResourceLoader.LoadWearableDatabase();

            worldsDatabase = ResourceLoader.LoadWorldDatabase();
        }

        public override void DrawDatabase()
        {
            if (worldsDatabase == null)
            {
                worldsDatabase = ResourceLoader.LoadWorldDatabase();
            }
            if (wdb == null)
            {
                wdb = ResourceLoader.LoadWearableDatabase();
            }
            foreach (Wearable w in wdb.wearables)
            {
                items.Add(new GUIContent(w.itemName), w);
            }
            itemNames = new GUIContent[items.Keys.Count];
            items.Keys.CopyTo(itemNames, 0);
            GUILayout.BeginHorizontal();
            worldSelectedInnerTab = GUILayout.Toolbar(worldSelectedInnerTab, innerTabs);
            GUILayout.EndHorizontal();

            if (worldSelectedInnerTab == 0)
            {
                //Si on est en mode view
                GUILayout.Space(10);
                if (worldsDatabase.worlds.Count == 0)
                {
                    GUILayout.Label("There is no Item in the Database!");
                }
                else
                {
                    worldScrollPosition = GUILayout.BeginScrollView(worldScrollPosition);
                    for (int i = 0; i < worldsDatabase.worlds.Count; i++)
                    {
                        worldManageItem.Add(false);
                        GUILayout.BeginVertical("Box");
                        GUILayout.BeginHorizontal();
                        worldManageItem[i] = EditorGUILayout.Foldout(worldManageItem[i], worldsDatabase.worlds[i].worldName);
                        GUILayout.FlexibleSpace();
                        if (i > 0)
                        {
                            if (GUILayout.Button("▲"))
                            {//UP
                                worldManageItem.ForEach(e =>
                                {
                                    e = false;
                                });
                                World tmp = worldsDatabase.worlds[i];
                                worldsDatabase.worlds[i] = worldsDatabase.worlds[i - 1];
                                worldsDatabase.worlds[i - 1] = tmp;
                            }
                        }
                        if (i < worldsDatabase.worlds.Count - 1)
                        {
                            if (GUILayout.Button("▼"))
                            {//DOWN
                                worldManageItem.ForEach(e =>
                                {
                                    e = false;
                                });
                                World tmp = worldsDatabase.worlds[i];
                                worldsDatabase.worlds[i] = worldsDatabase.worlds[i + 1];
                                worldsDatabase.worlds[i + 1] = tmp;
                            }
                        }
                        if (GUILayout.Button("X"))
                        {
                            worldsDatabase.worlds.RemoveAt(i);
                        }
                        GUILayout.EndHorizontal();
                        if (worldManageItem[i])
                        { //Si on est entrain de manage cet item.
                            World edit = worldsDatabase.worlds[i];
                            DrawWorldEdit(ref edit, true);
                        }
                        GUILayout.EndVertical();
                    }
                    GUILayout.EndScrollView();
                }
            }
            else
            {//Si on est en mode création
                DrawWorldEdit(ref createdWorld, false);
            }
        }

        public void DrawWorldEdit(ref World w, bool edit)
        {
            if (!edit)
            {
                worldScrollPosition = GUILayout.BeginScrollView(worldScrollPosition);
            }
            GUILayout.BeginVertical("Box");
            GUILayout.Space(20);
            w.worldName = EditorGUILayout.TextField("World name", w.worldName);
            GUILayout.BeginHorizontal();
            w.parallaxFirstPlan = (Sprite)EditorGUILayout.ObjectField("First plan", w.parallaxFirstPlan, typeof(Sprite), true);
            w.parallaxSecondPlan = (Sprite)EditorGUILayout.ObjectField("Second plan", w.parallaxSecondPlan, typeof(Sprite), true);
            w.parallaxThirdPlan = (Sprite)EditorGUILayout.ObjectField("Third plan", w.parallaxThirdPlan, typeof(Sprite), true);
            GUILayout.EndHorizontal();
            GUILayout.BeginVertical("Box");
            EditorGUILayout.LabelField("Stages");
            if (w.stages == null)
            {
                w.stages = new Stage[4] { new Stage(), new Stage(), new Stage(), new Stage() };
            }
            for (int i = 0; i < w.stages.Length; i++)
            {
                GUILayout.BeginVertical("Box");
                GUIStyle label = GUI.skin.GetStyle("Label");
                label.alignment = TextAnchor.UpperCenter;
                EditorGUILayout.LabelField("Stage " + (i + 1), label);
                List<Spawnable> list = w.stages[i].bestiary;
                if (list == null)
                {
                    list = new List<Spawnable>();
                }

                EditorGUILayout.LabelField("Bestiary", EditorStyles.boldLabel);
                int newCount = Mathf.Max(0, EditorGUILayout.IntField("size", list.Count));
                while (newCount < list.Count)
                    list.RemoveAt(list.Count - 1);
                while (newCount > list.Count)
                    list.Add(null);
                for (int j = 0; j < list.Count; j++)
                {
                    list[j] = (Spawnable)EditorGUILayout.ObjectField(list[j], typeof(Spawnable), true);
                }
                w.stages[i].boss = (Spawnable)EditorGUILayout.ObjectField("Final Boss", w.stages[i].boss, typeof(Spawnable), true);

                EditorGUILayout.LabelField("DropList", EditorStyles.boldLabel);
                if(w.stages[i].dropList == null)
                {
                    w.stages[i].dropList = new List<Drop>();
                }
                if (w.stages[i].dropList.Count == 0)
                {
                    EditorGUILayout.HelpBox("Empty droplist", MessageType.Info);
                }

                for (int j = 0; i < w.stages[i].dropList.Count; i++)
                {
                    Drop d = w.stages[j].dropList[i];

                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField(d.item.itemName + (d.amount > 1 ? " x " + d.amount : "") + " : " + (d.rate * 100f) + "%");
                    GUILayout.FlexibleSpace();
                    if (GUILayout.Button("X"))
                    {
                        w.stages[j].dropList.RemoveAt(i);
                    }
                    EditorGUILayout.EndHorizontal();
                }
                GUILayout.Space(5);
                EditorGUILayout.BeginVertical("Box");
                selectedItemIndex = EditorGUILayout.Popup(selectedItemIndex, itemNames);
                Drop drop = new Drop(items[itemNames[selectedItemIndex]]);
                drop.rate = EditorGUILayout.Slider("rate", drop.rate, 0f, 1f);
                drop.amount = EditorGUILayout.IntField("amount", drop.amount);
                if (GUILayout.Button("Add item"))
                {
                    w.stages[i].dropList.Add(drop);
                }
                EditorGUILayout.EndVertical();
                GUILayout.EndVertical();
            }
            GUILayout.EndVertical();
            GUILayout.EndVertical();
            if (!edit)
            {
                if (GUILayout.Button("Create"))
                {
                    EditorUtility.SetDirty(worldsDatabase);
                    worldsDatabase.worlds.Add(w);
                    createdWorld = new World();
                    EditorUtility.SetDirty(worldsDatabase);
                }

                GUILayout.EndScrollView();
            }
        }
    }
}
