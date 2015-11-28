using UnityEngine;
using System.Collections;
using FU;

public class GrenadeTosser : Inventory {
	
	[SerializeField]	private GameObject k_Bomb;
	[SerializeField]	private GameObject blackDeath;

	private float throwForce;
	private float timeToWaitToThrow;
	private bool canThrowGrenade;

	// Use this for initialization
	void Awake () {
		throwForce = 500f;
		timeToWaitToThrow = 2f;
		canThrowGrenade = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown (Controls.FightFire) && gearInventory[CurrentGear]>0 &&
		    (CurrentGear == GearEnum.K_Bomb || CurrentGear == GearEnum.BlackDeath) &&
		    canThrowGrenade){

			StartCoroutine (ThrowThing());
		}
	}

	IEnumerator ThrowThing(){
		GameObject objectToSpawn = CurrentGear == GearEnum.K_Bomb ? k_Bomb : blackDeath;
		gearInventory[CurrentGear]--;
		UI_Animations active_UI;
		if (CurrentGear == GearEnum.K_Bomb)	active_UI = k_Bomb_UI;
		else								active_UI = blackDeath_UI;
		active_UI.UpdateInventory(gearInventory[CurrentGear]);

		canThrowGrenade = false;
		GameObject grenade = Instantiate(objectToSpawn, transform.position, Quaternion.identity) as GameObject;
		grenade.GetComponentInChildren<Rigidbody>().AddForce(Camera.main.transform.forward * throwForce);
		yield return new WaitForSeconds(timeToWaitToThrow);
		canThrowGrenade = true;
	}

	void OnDrawGizmos(){
		Gizmos.color = Color.red;
		Gizmos.DrawLine (transform.position, transform.position + Camera.main.transform.forward * 3f);
	}
}
