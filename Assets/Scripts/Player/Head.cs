using UnityEngine;
using System.Collections;
using FU;
public class Head : MonoBehaviour {

	[SerializeField]	private Transform playerTransform;
	private float rotationSpeed;
	private float minAxisInput;
	private float lowBound;
	private float highBound;

	// Use this for initialization
	void Awake () {
		rotationSpeed = 3f;
		minAxisInput = 0.15f;
		lowBound = 85;
		highBound = 275;

		Controls.SetControls();
	}
	
	// Update is called once per frame
	void Update () {
		Rotate();
	}

	void Rotate(){
		float forwardAxis = Input.GetAxis(Controls.LookUp);
		float sidewaysAxis = Input.GetAxis(Controls.LookSideways);
		Vector3 eulerRotation = transform.localRotation.eulerAngles;
		bool shouldWeKeepTiltingTheHead = (eulerRotation.x>=lowBound && eulerRotation.x<(lowBound+5f) && forwardAxis<0) ||
									 (eulerRotation.x<=highBound && eulerRotation.x>(highBound-5f) && forwardAxis>0) ||
									 (!(eulerRotation.x>=lowBound && eulerRotation.x<=highBound));

		forwardAxis = shouldWeKeepTiltingTheHead ? forwardAxis : 0f;

		if (Mathf.Abs (forwardAxis)>minAxisInput || Mathf.Abs (sidewaysAxis)>minAxisInput){
			transform.localRotation = Quaternion.Euler(eulerRotation.x + (rotationSpeed * forwardAxis),0f,0f);
			playerTransform.rotation = Quaternion.Euler(0f,playerTransform.rotation.eulerAngles.y + (rotationSpeed * sidewaysAxis),0f);
		}

		if (eulerRotation.x==90) transform.localRotation = Quaternion.Euler(85f,0f,0f);
		else if (eulerRotation.x==270) transform.localRotation = Quaternion.Euler(275f,0f,0f);
	}
}
