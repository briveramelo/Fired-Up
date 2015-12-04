using UnityEngine;
using System.Collections;

public class RoomOccluder : MonoBehaviour, ITriggerable {

	[SerializeField] private OcclusionPortal occluder;
	public void ActivateThing(){
		occluder.open = false;
	}

}
