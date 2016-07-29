using UnityEngine;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using AFKHero.Core.Event;
using System.IO;
using AFKHero.Core.Tools;
using System.Linq;


namespace AFKHero.Core.Save
{
    public class SaveEngine : MonoBehaviour
	{
		List<Saveable> saveables;

		static SaveData save;

		void Awake ()
		{
			SaveEngine[] instances = FindObjectsOfType<SaveEngine>();
			if (instances.Length > 1)
			{
				Destroy(instances[instances.Length - 1].gameObject);
				instances = instances.Reverse().Skip(1).Reverse().ToArray();
			}
			DontDestroyOnLoad (this);
			EventDispatcher.Instance.Register ("save", new Listener<GameEvent> ((ref GameEvent e) => {
                Save();
			}));
            EventDispatcher.Instance.Register("restart", new Listener<GenericGameEvent<float>>((ref GenericGameEvent<float> e) => {
                save.distance = e.Data;
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
			if (File.Exists (Application.persistentDataPath + "/AFKHero.save")) {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(Application.persistentDataPath + "/AFKHero.save", FileMode.Open);
                save = JsonUtility.FromJson<SaveData>(CryptoService.Xor(bf.Deserialize(file).ToString()));
                file.Close();
                Load();
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
            save = new SaveData ();
            if (saveables != null)
            {
                foreach (Saveable s in saveables)
                {
                    save = s.Save(save);
                }
                Persist();
            }
		}

		/// <summary>
		/// Load this instance.
		/// </summary>
		public void Load ()
		{
			foreach (Saveable s in saveables) {
				s.Load (save);
			}
		}

		public void Persist ()
		{
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Create (Application.persistentDataPath + "/AFKHero.save");
			bf.Serialize (file, CryptoService.Xor (JsonUtility.ToJson (save)));
			file.Close ();
		}
	}
}
