using UnityEngine;
using System.Collections;
using AFKHero.Common;

namespace AFKHero.Stat{
	public class Vitality : AbstractStat {

		public double currentHp;

		void Start(){
			this.currentHp = Value;
		}

		public void Init(){
			this.Start ();
		}

		public override string GetName() {
			return "vitality";
		}
	}
}
