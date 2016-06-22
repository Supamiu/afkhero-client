using UnityEngine;
using System.Collections.Generic;
using AFKHero.Core.Event;


namespace AFKHero.Core.Save
{
	public class SaveEngine : MonoBehaviour
	{
		List<Saveable> saveables = new List<Saveable> ();

		Dictionary<string, object[]> save = new Dictionary<string, object[]> ();

		void Awake ()
		{
			DontDestroyOnLoad (this.gameObject);
			EventDispatcher.Instance.Register ("save", new Listener<GameEvent>((ref GameEvent e) => {
				this.Save();
			}));
			EventDispatcher.Instance.Register ("load", new Listener<GameEvent> ((ref GameEvent e) => {
				this.Load();
			}));
		}

		// Use this for initialization
		void Start ()
		{
			GameObject[] allGO = FindObjectsOfType<GameObject> ();
			foreach (GameObject go in allGO) {
				saveables.AddRange (go.GetComponents<Saveable> ());
			}
		}

		void OnLevelWasLoaded(int level){
			this.Start ();
			this.Load ();
		}

		/// <summary>
		/// Save this instance.
		/// </summary>
		public void Save ()
		{
			foreach (Saveable s in this.saveables) {
				if (this.save.ContainsKey (s.GetIdentifier ())) {
					this.save [s.GetIdentifier ()] = s.Save ();
				} else {
					this.save.Add (s.GetIdentifier (), s.Save ());
				}
			}
		}

		/// <summary>
		/// Load this instance.
		/// </summary>
		public void Load()
		{
			foreach (Saveable s in this.saveables) {
				object[] data;
				this.save.TryGetValue (s.GetIdentifier(), out data);
				if (data != null) {
					s.Load (data);
				}
			}
		}
	}
}
