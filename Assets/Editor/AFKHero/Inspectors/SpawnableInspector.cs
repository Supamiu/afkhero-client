using AFKHero.Behaviour.Monster;
using AFKHero.Core;
using AFKHero.Core.Database;
using AFKHero.Model;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace AFKHero.EditorExtension.Inspectors
{
    [CustomEditor(typeof(Spawnable))]
    public class SpawnableInspector : Editor
    {
        private WearableDatabase wdb = ResourceLoader.LoadWearableDatabase();

        private Dictionary<GUIContent, Item> items = new Dictionary<GUIContent, Item>();

        private int selectedItemIndex;

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            foreach (Wearable w in wdb.wearables)
            {
                items.Add(new GUIContent(w.itemName), w);
            }
            GUIContent[] itemNames = new GUIContent[items.Keys.Count];
            items.Keys.CopyTo(itemNames, 0);

            Spawnable spawnable = (Spawnable)target;

            GUILayout.BeginVertical();
            GUIStyle label = GUI.skin.GetStyle("Label");
            label.alignment = TextAnchor.UpperCenter;

            EditorGUILayout.LabelField("DropList", label);

            if (spawnable.dropList.Count == 0)
            {
                EditorGUILayout.HelpBox("Empty droplist", MessageType.Info);
            }

            for (int i = 0; i < spawnable.dropList.Count; i++)
            {
                Drop d = spawnable.dropList[i];

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(d.item.itemName + (d.amount > 1 ? " x " + d.amount : "") + " : " + (d.rate * 100f) + "%");
                GUILayout.FlexibleSpace();
                if (GUILayout.Button("X"))
                {
                    spawnable.dropList.RemoveAt(i);
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
                spawnable.dropList.Add(drop);
            }
            EditorGUILayout.EndVertical();
            GUILayout.EndVertical();
        }
    }
}
