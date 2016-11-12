/*****************************************************************************
 * SkeletonGhostRenderer created by Mitch Thompson
 * Full irrevocable rights and permissions granted to Esoteric Software
*****************************************************************************/

using UnityEngine;
using System.Collections;

namespace Spine.Unity.Modules {
	public class SkeletonGhostRenderer : MonoBehaviour {

		public float fadeSpeed = 10;

	    private Color32[] colors;
	    private Color32 black = new Color32(0, 0, 0, 0);
	    private MeshFilter meshFilter;
	    private MeshRenderer meshRenderer;

	    private void Awake () {
			meshRenderer = gameObject.AddComponent<MeshRenderer>();
			meshFilter = gameObject.AddComponent<MeshFilter>();
		}

		public void Initialize (Mesh mesh, Material[] materials, Color32 color, bool additive, float speed, int sortingLayerID, int sortingOrder) {
			StopAllCoroutines();

			gameObject.SetActive(true);
			meshRenderer.sharedMaterials = materials;
			meshRenderer.sortingLayerID = sortingLayerID;
			meshRenderer.sortingOrder = sortingOrder;
			meshFilter.sharedMesh = Instantiate(mesh);
			colors = meshFilter.sharedMesh.colors32;

			if ((color.a + color.r + color.g + color.b) > 0) {
				for (var i = 0; i < colors.Length; i++)
					colors[i] = color;
			}

			fadeSpeed = speed;

			if (additive)
				StartCoroutine(FadeAdditive());
			else
				StartCoroutine(Fade());
		}

	    private IEnumerator Fade () {
			Color32 c;
			for (var t = 0; t < 500; t++) {
				var breakout = true;
				for (var i = 0; i < colors.Length; i++) {
					c = colors[i];
					if (c.a > 0)
						breakout = false;

					colors[i] = Color32.Lerp(c, black, Time.deltaTime * fadeSpeed);
				}
				meshFilter.sharedMesh.colors32 = colors;

				if (breakout)
					break;
				
				yield return null;
			}

			Destroy(meshFilter.sharedMesh);
			gameObject.SetActive(false);
		}

	    private IEnumerator FadeAdditive () {
			Color32 c;
			var black = this.black;

			for (var t = 0; t < 500; t++) {

				var breakout = true;
				for (var i = 0; i < colors.Length; i++) {
					c = colors[i];
					black.a = c.a;
					if (c.r > 0 || c.g > 0 || c.b > 0)
						breakout = false;

					colors[i] = Color32.Lerp(c, black, Time.deltaTime * fadeSpeed);
				}

				meshFilter.sharedMesh.colors32 = colors;

				if (breakout)
					break;
				yield return null;
			}

			Destroy(meshFilter.sharedMesh);

			gameObject.SetActive(false);
		}

		public void Cleanup () {
			if (meshFilter != null && meshFilter.sharedMesh != null)
				Destroy(meshFilter.sharedMesh);

			Destroy(gameObject);
		}
	}

}
