﻿using UnityEngine;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using AFKHero.Core.Event;
using System.IO;
using AFKHero.Core.Tools;


namespace AFKHero.Core.Save
{
    public class SaveEngine : MonoBehaviour
	{
		List<Saveable> saveables;

		static SaveData save;

		void Awake ()
		{
			DontDestroyOnLoad (this);
			EventDispatcher.Instance.Register ("save", new Listener<GameEvent> ((ref GameEvent e) => {
                Save();
			}));
			EventDispatcher.Instance.Register ("load", new Listener<GameEvent> ((ref GameEvent e) => {
                Load();
			}));
		}

		// Use this for initialization
		void Start ()
		{
            saveables = new List<Saveable> ();
			GameObject[] allGO = FindObjectsOfType<GameObject> ();
			foreach (GameObject go in allGO) {
				saveables.AddRange (go.GetComponents<Saveable> ());
			}
			if (File.Exists (Application.persistentDataPath + "/AFKHero.gd")) {
				//TODO load data.
			}
		}

		void OnLevelWasLoaded (int level)
		{
            Start();
            Load();
		}

		/// <summary>
		/// Save this instance.
		/// </summary>
		public void Save ()
		{
			SaveEngine.save = new SaveData ();
			foreach (Saveable s in saveables) {
				SaveEngine.save = s.Save (SaveEngine.save);
			}
		}

		/// <summary>
		/// Load this instance.
		/// </summary>
		public void Load ()
		{
			foreach (Saveable s in saveables) {
				s.Load (SaveEngine.save);
			}
		}

		public void Persist ()
		{
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Create (Application.persistentDataPath + "/AFKHero.save");
			bf.Serialize (file, CryptoService.Instance.Xor (JsonUtility.ToJson (SaveEngine.save)));
			file.Close ();
		}
	}
}
