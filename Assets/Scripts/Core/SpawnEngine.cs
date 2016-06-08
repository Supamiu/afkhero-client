﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using AFKHero.Behaviour;
using AFKHero.Core.Event;
using AFKHero.Tools;

namespace AFKHero.Core
{
	public class SpawnEngine : MonoBehaviour
	{
		public Spawnable[] monsters;

		public float spawnInterval = 0.5f;

		public float spawnChances = 0.5f;

		private Vector3 spawnPosition;

		private float moved = 0f;

		// Use this for initialization
		void Start ()
		{
			this.spawnPosition = this.transform.position;
			EventDispatcher.Instance.register ("movement.moved", new Listener<GenericGameEvent<float>>((ref GenericGameEvent<float> e) => {
				this.moved += e.Data;
				if(this.moved >= this.spawnInterval && PercentageUtils.Instance.GetResult(this.spawnChances)){
					this.Spawn(this.monsters[0]);
					this.moved = 0f;
				}else if(this.moved >= this.spawnInterval){
					this.moved = 0f;
				}
			}));
		}

		void Spawn(Spawnable s){
			Instantiate (s, this.spawnPosition, Quaternion.identity);
		}
	}
}
