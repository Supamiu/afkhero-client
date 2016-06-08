using UnityEngine;
using System.Collections;
using Spine.Unity;
using Spine.Unity.Modules;
using Spine;

namespace AFKHero.Behaviour
{
	[RequireComponent(typeof(SkeletonRenderer))]
	public class WeaponChanger : MonoBehaviour
	{

		public Sprite[] weapons;

		private Skeleton skeleton;

		private int actualWeapon = 0;

		[SpineSlot]
		public string slot;

		// Use this for initialization
		void Start ()
		{
			this.skeleton = GetComponent<SkeletonRenderer> ().skeleton;
			this.Equip (weapons[actualWeapon]);
		}

		void Equip(Sprite weapon){
			this.skeleton.AttachUnitySprite (this.slot, weapon);
		}

		public void SwitchWeapon(){
			actualWeapon++;
			if (actualWeapon > weapons.Length - 1) {
				actualWeapon = 0;
			}
			this.Equip (weapons[actualWeapon]);
		}
	}
}
