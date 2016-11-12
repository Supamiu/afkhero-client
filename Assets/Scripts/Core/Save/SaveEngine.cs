using UnityEngine;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using AFKHero.Core.Event;
using System.IO;
using AFKHero.Core.Tools;
using System.Linq;
using UnityEngine.SceneManagement;

namespace AFKHero.Core.Save
{
    public class SaveEngine : MonoBehaviour
	{
	    private List<Saveable> saveables;

	    private static SaveData save;

	    private void Awake ()
		{
			var instances = FindObjectsOfType<SaveEngine>();
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
                Persist();
            }));
            EventDispatcher.Instance.Register ("load", new Listener<GameEvent> ((ref GameEvent e) => {
                Load();
			}));
		}

		// Use this for initialization
	    private void Start ()
		{
            SceneManager.sceneLoaded += SceneManager_sceneLoaded;
            saveables = new List<Saveable> ();
			var allGO = FindObjectsOfType<GameObject> ();
			foreach (var go in allGO) {
				saveables.AddRange (go.GetComponents<Saveable> ());
			}
		    if (!File.Exists(Application.persistentDataPath + "/AFKHero.save")) return;
		    var bf = new BinaryFormatter();
		    var file = File.Open(Application.persistentDataPath + "/AFKHero.save", FileMode.Open);
		    save = JsonUtility.FromJson<SaveData>(CryptoService.Xor(bf.Deserialize(file).ToString()));
		    file.Close();
		    Load();
		}

        private void SceneManager_sceneLoaded(Scene arg0, LoadSceneMode arg1)
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
		    if (saveables == null) return;
		    foreach (var s in saveables)
		    {
		        save = s.Save(save);
		    }
		    Persist();
		}

		/// <summary>
		/// Load this instance.
		/// </summary>
		public void Load ()
        {
            foreach (var s in saveables) {
				s.Load (save);
			}
		}

		public void Persist ()
		{
			var bf = new BinaryFormatter ();
			var file = File.Create (Application.persistentDataPath + "/AFKHero.save");
			bf.Serialize (file, CryptoService.Xor (JsonUtility.ToJson (save)));
			file.Close ();
		}
	}
}
