﻿using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using AFKHero.Core.Event;
using System.IO;
using AFKHero.Core.Tools;


namespace AFKHero.Core.Save
{
	public class SaveEngine : MonoBehaviour
	{
		List<Saveable> saveables;

		SaveData save = new SaveData ();

		void Awake ()
		{
			DontDestroyOnLoad (this);
			EventDispatcher.Instance.Register ("save", new Listener<GameEvent> ((ref GameEvent e) => {
				this.Save ();
			}));
			EventDispatcher.Instance.Register ("load", new Listener<GameEvent> ((ref GameEvent e) => {
				this.Load ();
			}));
		}

		// Use this for initialization
		void Start ()
		{
			this.saveables = new List<Saveable> ();
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
			this.Start ();
			this.Load ();
		}

		/// <summary>
		/// Save this instance.
		/// </summary>
		public void Save ()
		{
			foreach (Saveable s in this.saveables) {
				this.save = s.Save (this.save);
			}
		}

		/// <summary>
		/// Load this instance.
		/// </summary>
		public void Load ()
		{
			foreach (Saveable s in this.saveables) {
				s.Load (this.save);
			}
		}

		public void Persist ()
		{
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Create (Application.persistentDataPath + "/AFKHero.save");
			bf.Serialize (file, CryptoService.Instance.Xor (JsonUtility.ToJson (this.save)));
			file.Close ();
		}
	}
}
