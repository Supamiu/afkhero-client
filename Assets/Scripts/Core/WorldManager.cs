using UnityEngine;
using System.Collections;
using AFKHero.Model;
using AFKHero.Common;

public class WorldManager : MonoBehaviour {

	[Header("1er plan du parallax")]
	public ScrollingScript parallaxFirstPlan;

	[Header("2nd plan du parallax")]
	public ScrollingScript parallaxSecondPlan;

	[Header("3e plan du parallax")]
	public ScrollingScript parallaxThirdPlan;

	[Header("Mondes existants")]
	public World[] worlds;

	[Header("Monde en cours")]
	public int currentWorld = 0;

	void Start(){
		if (this.worlds.Length == 0) {
			Debug.LogError ("WorldManager has 0 worlds, this should never happen !");
			return;
		}
		this.SetWorld (this.GetCurrentWorld());
	}

	public World GetCurrentWorld(){
		return this.worlds [currentWorld];
	}

	private void SetWorld(World world){
		SpriteRenderer[] firstPlanSprites = this.parallaxFirstPlan.GetComponentsInChildren<SpriteRenderer> ();
		SpriteRenderer[] secondPlanSprites = this.parallaxSecondPlan.GetComponentsInChildren<SpriteRenderer> ();
		SpriteRenderer[] thirdPlanSprites = this.parallaxThirdPlan.GetComponentsInChildren<SpriteRenderer> ();
		foreach(SpriteRenderer s in firstPlanSprites) {
			s.sprite = world.parallaxFirstPlan;
		}
		foreach(SpriteRenderer s in secondPlanSprites) {
			s.sprite = world.parallaxSecondPlan;
		}
		foreach(SpriteRenderer s in thirdPlanSprites) {
			s.sprite = world.parallaxThirdPlan;
		}
	}

	public WorldManager NextWorld(){
		//TODO gérer la fin du jeu.
		this.currentWorld++;
		return this;
	}
}
