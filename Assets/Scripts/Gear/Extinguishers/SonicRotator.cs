using UnityEngine;
using System.Collections;

public class SonicRotator : MonoBehaviour {

	private float rotationSpeed;
	private Vector3 rotationVector;
	private Quaternion targetRotation;

	void Awake(){
		rotationSpeed = 5f;
		rotationVector = Vector3.forward * rotationSpeed;
	}

	void Update () {
		targetRotation = Quaternion.Euler(transform.rotation.eulerAngles + rotationVector);
		transform.rotation = Quaternion.Slerp(transform.rotation,targetRotation,0.1f);
	}
}
