using UnityEngine;
using System.Collections;
using FU;

public class EventTrigger : MonoBehaviour {

	[SerializeField] private ITriggerable[] interfacesToTrigger;
	[SerializeField] LayerMask layersThatTrigger;
	bool activated = false;

	void OnTriggerEnter(Collider col){
		if (!activated){
			if (LayerMaskExtensions.IsInLayerMask(col.gameObject, layersThatTrigger)){
				foreach (ITriggerable interfaceToTrigger in interfacesToTrigger){
					interfaceToTrigger.ActivateThing();
				}
				activated = true;
			}
		}
	}


}
