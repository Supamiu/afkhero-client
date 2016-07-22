﻿using UnityEngine;
using UnityEngine.UI;
using AFKHero.Common;

namespace AFKHero.UI.Golds
{
    public class GoldGain : MonoBehaviour
	{
		public Animator animator;

		public Text text;

		// Use this for initialization
		void Start ()
		{
			AnimatorClipInfo[] clipInfo = animator.GetCurrentAnimatorClipInfo (0);
			Destroy (gameObject, clipInfo [0].clip.length);
		}

		/// <summary>
		/// Sets the amount.
		/// </summary>
		/// <param name="amount">Amount.</param>
		public void SetAmount (double amount)
		{
            text.text = "+" + Formatter.Format (amount);
		}
	}
}
