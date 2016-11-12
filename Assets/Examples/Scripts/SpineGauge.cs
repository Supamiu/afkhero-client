﻿using UnityEngine;
using Spine.Unity;

[ExecuteInEditMode]
[RequireComponent(typeof(SkeletonRenderer))]
public class SpineGauge : MonoBehaviour {

	#region Inspector
	[Range(0,1)]
	public float fillPercent = 0;

	[SpineAnimation]
	public string fillAnimationName;
	#endregion

    private SkeletonRenderer skeletonRenderer;
    private Spine.Animation fillAnimation;

    private void Awake () {
		skeletonRenderer = GetComponent<SkeletonRenderer>();

	}

    private void Update () {
		SetGaugePercent(fillPercent);
	}

	public void SetGaugePercent (float x) {
		if (skeletonRenderer == null) return;
		var skeleton = skeletonRenderer.skeleton; if (skeleton == null) return;

		// Make super-sure that fillAnimation isn't null. Early exit if it is.
		if (fillAnimation == null) {
			fillAnimation = skeleton.Data.FindAnimation(fillAnimationName);
			if (fillAnimation == null) return;
		}
			
		fillAnimation.Apply(skeleton, 0, x, false, null);

		skeleton.Update(Time.deltaTime);
		skeleton.UpdateWorldTransform();
	}
}
