﻿using UnityEngine;
using System.Collections;
using AFKHero.Common;

namespace AFKHero.Stat{
	public class Vitality : Stat {

		public bool isMortal;

		public double currentHp;

		void Start(){
			this.currentHp = Value;
		}

		public void Init(){
			this.Start ();
		}
	}
}
