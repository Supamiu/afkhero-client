using UnityEngine;
using System.Collections;
using AFKHero.Behaviour.Monster;

namespace AFKHero.Model
{
    [System.Serializable]
	public class World
	{
		[Header("Nom du monde")]
		public string worldName;

		[Header("Sprite pour le sol")]
		public Sprite groundSprite;

		[Header("1er plan du parallax")]
		public Sprite parallaxFirstPlan;

		[Header("2nd plan du parallax")]
		public Sprite parallaxSecondPlan;

		[Header("3e plan du parallax")]
		public Sprite parallaxThirdPlan;
        
        [Header("Les paliers du monde")]
		public Stage[] stages;
	}
}
