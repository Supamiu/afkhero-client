using UnityEngine;
using System.Collections;

public class RaggedySpineboy : MonoBehaviour {

	public LayerMask groundMask;
	public float restoreDuration = 0.5f;
	public Vector2 launchVelocity = new Vector2(50,100);

    private Spine.Unity.Modules.SkeletonRagdoll2D ragdoll;
    private Collider2D naturalCollider;

    private void Start () {
		
		ragdoll = GetComponent<Spine.Unity.Modules.SkeletonRagdoll2D>();
		naturalCollider = GetComponent<Collider2D>();
	}

    private void AddRigidbody () {
		var rb = gameObject.AddComponent<Rigidbody2D>();
		#if UNITY_5_1 || UNITY_5_2 || UNITY_5_3 || UNITY_5_4 || UNITY_5_5
        rb.freezeRotation = true;
		#else
		rb.fixedAngle = true;
		#endif
		naturalCollider.enabled = true;
	}

    private void RemoveRigidbody () {
		Destroy(GetComponent<Rigidbody2D>());
		naturalCollider.enabled = false;
	}

    private void Update () {
		
	}

    private void OnMouseUp () {
		if (naturalCollider.enabled) {
			Launch();
		}
	}

    private void Launch () {
		RemoveRigidbody();
		ragdoll.Apply();
		ragdoll.RootRigidbody.velocity = new Vector2(Random.Range(-launchVelocity.x, launchVelocity.x), launchVelocity.y);
		StartCoroutine(WaitUntilStopped());
	}

    private IEnumerator Restore () {
		var estimatedPos = ragdoll.EstimatedSkeletonPosition;
		Vector3 rbPosition = ragdoll.RootRigidbody.position;

		var skeletonPoint = estimatedPos;
		var hit = Physics2D.Raycast((Vector2)rbPosition, (Vector2)(estimatedPos - rbPosition), Vector3.Distance(estimatedPos, rbPosition), groundMask);
		if (hit.collider != null)
			skeletonPoint = hit.point;
		

		ragdoll.RootRigidbody.isKinematic = true;
		ragdoll.SetSkeletonPosition(skeletonPoint);

		yield return ragdoll.SmoothMix(0, restoreDuration);
		ragdoll.Remove();

		AddRigidbody();
	}

    private IEnumerator WaitUntilStopped () {
		yield return new WaitForSeconds(0.5f);

		float t = 0;
		while (t < 0.5f) {
			if (ragdoll.RootRigidbody.velocity.magnitude > 0.09f)
				t = 0;
			else
				t += Time.deltaTime;

			yield return null;
		}

		StartCoroutine(Restore());
	}
}
