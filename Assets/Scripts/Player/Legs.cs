using UnityEngine;
using System.Collections;
using FU;

public class Legs : MonoBehaviour {

	[SerializeField]	private	Rigidbody playerBody;
	[SerializeField]	private Transform feetTran;
	private float minAxisInput;
	private float speed;
	private float maxSpeed;
	private float jumpForce;
	private float moveForce;

	// Use this for initialization
	void Awake () {
		minAxisInput = 0.15f;
		maxSpeed = 5f;
		moveForce = .5f;
		jumpForce = 300f;
	}
	
	// Update is called once per frame
	void Update () {
		Move ();
		Jump();
	}

	void Move(){
		speed = playerBody.velocity.magnitude;
		float forwardAxis = Input.GetAxis(Controls.Forward);
		float sidewaysAxis = Input.GetAxis(Controls.Sideways);

		if (Mathf.Abs (forwardAxis)>=minAxisInput){
			playerBody.AddForce( moveForce * transform.forward * -Mathf.Sign( forwardAxis ), ForceMode.VelocityChange );
		}
		if (Mathf.Abs (sidewaysAxis)>=minAxisInput){
			playerBody.AddForce( moveForce * transform.right * Mathf.Sign( sidewaysAxis ), ForceMode.VelocityChange );
		}
		if (speed >= maxSpeed){
			playerBody.velocity = playerBody.velocity.normalized * maxSpeed;
		}

	}

	void Jump(){
		if (Input.GetButtonDown (Controls.Jump)){
			if (Physics.CheckSphere(feetTran.position,.5f,Layers.LayerMasks.ground.value)){
				playerBody.velocity = new Vector3 (playerBody.velocity.x,0f,playerBody.velocity.z);
				playerBody.AddForce (jumpForce * Vector3.up);
			}
		}
	}


}
