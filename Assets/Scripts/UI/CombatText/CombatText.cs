using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CombatText : MonoBehaviour {
	public Animator animator;
	private Text damageText;

	void OnEnable()
	{
		Debug.Log ("Start");
		Debug.Log (animator);
		AnimatorClipInfo[] clipInfo = animator.GetCurrentAnimatorClipInfo (0);
		Destroy (gameObject, clipInfo[0].clip.length);
		damageText = animator.GetComponent<Text> ();
	}

	public void SetText(string text)
	{
		Debug.Log ("SetText");
		damageText.text = text;
	}
}
