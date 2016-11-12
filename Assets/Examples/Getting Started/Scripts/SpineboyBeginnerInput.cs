using UnityEngine;

public class SpineboyBeginnerInput : MonoBehaviour {

	#region Inspector
	public string horizontalAxis = "Horizontal";
	public string attackButton = "Fire1";
	public string jumpButton = "Jump";

	public SpineboyBeginnerModel model;

    private void OnValidate () {
		if (model == null)
			model = GetComponent<SpineboyBeginnerModel>();
	}
	#endregion

    private void Update () {
		if (model == null) return;

		var currentHorizontal = Input.GetAxisRaw(horizontalAxis);
		model.TryMove(currentHorizontal);

		if (Input.GetButton(attackButton))
			model.TryShoot();

		if (Input.GetButtonDown(jumpButton))
			model.TryJump();
	
	}


}
