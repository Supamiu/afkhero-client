using System.Collections.Generic;
using AFKHero.Behaviour.Monster;
using AFKHero.Model;
using AFKHero.Core.Database;
using UnityEditor;
using UnityEngine;

namespace AFKHero.Editor.Layout
{
    public class WorldDatabaseLayout : AbstractDatabaseLayout
    {
        private int worldSelectedInnerTab = 0;
        private Vector2 worldScrollPosition;
        private List<bool> worldManageItem = new List<bool>();
        private static World createdWorld = new World();

        private string[] innerTabs = { "View database", "Add World" };

        private WorldDatabase worldsDatabase;

        public WorldDatabaseLayout()
        {
            worldsDatabase = Resources.Load<WorldDatabase>("Databases/WorldDatabase");
        }

        public override void DrawDatabase()
        {
            if (worldsDatabase == null)
            {
                worldsDatabase = Resources.Load<WorldDatabase>("Databases/WorldDatabase");
            }
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
                EditorGUILayout.LabelField("Bestiary");
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
            }
        }
    }
}
