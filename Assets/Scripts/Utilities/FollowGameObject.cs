using UnityEngine;
using System.Collections;

public class FollowGameObject : MonoBehaviour {

	[SerializeField]	private Transform objectToFollow;
	
	// Update is called once per frame
	void Update () {
		if (objectToFollow){
			transform.position = objectToFollow.position;
		}
	}
}
