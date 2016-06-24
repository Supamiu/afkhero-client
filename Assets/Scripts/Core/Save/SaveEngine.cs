using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using AFKHero.Core.Event;
using System.IO;


namespace AFKHero.Core.Save
{
	public class SaveEngine : MonoBehaviour
	{
		List<Saveable> saveables;

		Dictionary<string, object[]> save = new Dictionary<string, object[]> ();

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
				this.save [s.GetIdentifier ()] = s.Save ();
			}
		}

		/// <summary>
		/// Load this instance.
		/// </summary>
		public void Load ()
		{
			foreach (Saveable s in this.saveables) {
				object[] data;
				this.save.TryGetValue (s.GetIdentifier (), out data);
				if (data != null) {
					s.Load (data);
				}
			}
		}

		public void Persist ()
		{
			SaveData save = new SaveData ();
			save.data = this.save;
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Create (Application.persistentDataPath + "/AFKHero.gd");
			bf.Serialize (file, save);
			file.Close ();
		}
	}
}
