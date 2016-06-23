using UnityEngine;
using System.Collections;

namespace AFKHero.UI.CombatText
{
	public class CombatTextType
	{
		public static readonly CombatTextType DAMAGE = new CombatTextType (Color.red);
		public static readonly CombatTextType HEAL = new CombatTextType (Color.green);
		public static readonly CombatTextType MISS = new CombatTextType (Color.grey);

		Color color;

		public CombatTextType (Color color)
		{
			this.color = color;
		}

		public Color GetColor ()
		{
			return this.color;
		}
	}
}
