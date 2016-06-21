using UnityEngine;
using System.Collections;
using AFKHero.Behaviour.Monster;

namespace AFKHero.Model
{
	public class World : MonoBehaviour
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

		[Header("Bestiaire du monde")]
		public Spawnable[] bestiary;

		[Header("La distance de début")]
		public float start;

		[Header("La distance de fin")]
		public float end;
	}
}
