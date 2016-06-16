using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using AFKHero.Behaviour.Monster;
using AFKHero.Core.Event;
using AFKHero.Tools;

namespace AFKHero.Core
{
	public class SpawnEngine : MonoBehaviour
	{
		public WorldManager worldManager;

		public float spawnInterval = 2f;

		public float spawnChances = 0.5f;

		[Header("Juste pour récupérer l'offset")]
		public Transform hero;

		private Vector3 spawnPosition;

		private float moved = 0f;

		/// <summary>
		/// L'offset de spawn (la distance entre le héro et le spawnEngine pour avoir des monstres bien scale en damage.
		/// </summary>
		private float offset = 0f;

		// Use this for initialization
		void Start ()
		{
			this.spawnPosition = this.transform.position;
			this.offset = Vector2.Distance (hero.position, this.transform.position);
			Debug.Log ("spawn offset : " + this.offset.ToString ());
			EventDispatcher.Instance.Register ("movement.moved", new Listener<GenericGameEvent<float>> ((ref GenericGameEvent<float> e) => {
				this.moved += e.Data;
				if (this.moved >= this.spawnInterval && PercentageUtils.Instance.GetResult (this.spawnChances)) {
					this.Spawn (PercentageUtils.Instance.GetItemFromPonderables<Spawnable> (this.worldManager.GetCurrentWorld ().bestiary));
					this.moved = 0f;
				} else if (this.moved >= this.spawnInterval) {
					this.moved = 0f;
				}
			}));
		}

		void Spawn (Spawnable s)
		{
			GameObject spawned = GameObject.Instantiate (s.gameObject, this.spawnPosition, Quaternion.identity) as GameObject;
			spawned.GetComponent<Spawnable> ().Init (AFKHero.distance + this.offset);
		}
	}
}
