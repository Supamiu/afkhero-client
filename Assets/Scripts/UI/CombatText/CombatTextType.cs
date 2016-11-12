using UnityEngine;

namespace AFKHero.UI.CombatText
{
    public class CombatTextType
	{
		public static readonly CombatTextType DAMAGE = new CombatTextType (Color.white);
		public static readonly CombatTextType HEAL = new CombatTextType (Color.green);
		public static readonly CombatTextType MISS = new CombatTextType (Color.grey);

	    private readonly Color color;

		public CombatTextType (Color color)
		{
			this.color = color;
		}

		public Color GetColor ()
		{
			return color;
		}
	}
}
