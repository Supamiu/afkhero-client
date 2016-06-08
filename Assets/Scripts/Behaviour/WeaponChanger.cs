using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Spine.Unity;
using Spine.Unity.Modules;
using Spine;
using AFKHero.Model;

namespace AFKHero.Behaviour
{
	[RequireComponent (typeof(SkeletonRenderer))]
	public class WeaponChanger : MonoBehaviour
	{

		public Weapon[] weaponPrefabs;

		private List<Weapon> weapons = new List<Weapon> ();

		private Skeleton skeleton;

		private int actualWeapon = 0;

		private Weapon equiped;

		[SpineSlot]
		public string slot;

		// Use this for initialization
		void Start ()
		{
			foreach (Weapon w in this.weaponPrefabs) {
				this.weapons.Add (Instantiate (w));
			}
			this.skeleton = GetComponent<SkeletonRenderer> ().skeleton;
			this.Equip (weapons [actualWeapon]);
		}

		void Equip (Weapon weapon)
		{
			if (!weapon.isInit) {
				weapon.Init ();
			}
			if (this.equiped != null) {
				this.equiped.Detach ();
			}
			this.skeleton.AttachUnitySprite (this.slot, weapon.sprite);
			this.equiped = weapon;
			weapon.Attach (gameObject);
		}

		public void SwitchWeapon ()
		{
			actualWeapon++;
			if (actualWeapon > weapons.Count - 1) {
				actualWeapon = 0;
			}
			this.Equip (weapons [actualWeapon]);
		}
	}
}
