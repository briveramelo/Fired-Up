using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using FU; //namespace for Controls class of static input strings

//A real "Sonic Fire Extinguisher" can put out fires using sound waves
//This class mimics this behavior, using a trigger collider to detect overlap
//with fire trigger colliders. If this is active and overlapping for long enough,
//it will extinguish the fires.

public class SonicHose : MonoBehaviour {

	[SerializeField]	private EffectSettings beamEffectSettingsScript; //visual distortion
	[SerializeField]	private Animator sonicHoseAnimator;
	[SerializeField]	private Collider sonicBeamCollider;
	[SerializeField]	private AudioSource sonicSound;

	private float batteryPower; public float BatteryPower{get{return batteryPower;}} //UI element reads this
	private GearEnum LastGear;
	private List <Collider> fires; 
	private float timeToExtinguish;
	private float sonicClipLength;
	private float minBatteryPowerToEngage;
	private float fullBatteryCharge;

	void Awake(){
		timeToExtinguish = 2.5f;
		sonicClipLength = sonicSound.clip.length;
		fullBatteryCharge = 1f;
		batteryPower = fullBatteryCharge;
		minBatteryPowerToEngage = 0.33f;
		fires = new List<Collider>();
	}

	void Update(){
		if (Inventory.CurrentGear == GearEnum.SonicHose){
			if (LastGear != GearEnum.SonicHose)
				EquipSonicHose();
			if (Input.GetButtonDown (Controls.FightFire) && batteryPower > minBatteryPowerToEngage)
				StartCoroutine ( EngageSonicHose() );
		}
		else if (LastGear == GearEnum.SonicHose && Inventory.CurrentGear != GearEnum.SonicHose)
			StartCoroutine( PutAwaySonicHose() );

		LastGear = Inventory.CurrentGear;
	}

	void EquipSonicHose(){
		sonicHoseAnimator.SetInteger("AnimState",(int)HoseStates.Equip);
	}

	IEnumerator EngageSonicHose(){
		sonicHoseAnimator.SetInteger("AnimState",(int)HoseStates.Engage);
		beamEffectSettingsScript.IsVisible = true;
		sonicBeamCollider.enabled = true;
		sonicSound.Play();

		//drain battery
		while (Input.GetButton(Controls.FightFire) && Inventory.CurrentGear == GearEnum.SonicHose && batteryPower>0f){
			batteryPower -= Time.deltaTime / sonicClipLength;
			yield return null;
		}

		sonicSound.Stop ();
		sonicHoseAnimator.SetInteger("AnimState",(int)HoseStates.Idle);
		beamEffectSettingsScript.IsVisible = false;
		sonicBeamCollider.enabled = false;
		if (fires.Count>0)
			fires.Clear();

		StartCoroutine (RechargeBattery());
	}

	IEnumerator RechargeBattery(){
		while (batteryPower<fullBatteryCharge && sonicHoseAnimator.GetInteger("AnimState") != (int)HoseStates.Engage){
			batteryPower += Time.deltaTime / sonicClipLength;
			yield return null;
		}
	}


	IEnumerator PutAwaySonicHose(){
		yield return null;
		sonicHoseAnimator.SetInteger("AnimState",(int)HoseStates.PutAway);
	}

	void OnTriggerEnter(Collider col){
		if (LayerMaskExtensions.IsInLayerMask(col.gameObject,Layers.LayerMasks.allFires) && !fires.Contains(col))
			StartCoroutine (TryToExtinguish(col));
	}

	IEnumerator TryToExtinguish(Collider col){
		fires.Add(col);
		float timePassed =0;

		while (fires.Contains(col) && timePassed<timeToExtinguish){
			timePassed+=Time.deltaTime;
			yield return null;
		}

		if (fires.Contains(col)){
			FireSpread fireSpreadScript = col.GetComponent<FireSpread>();
			fireSpreadScript.ExtinguishFire();
			fires.Remove(col);
		}
	}

	void OnTriggerExit(Collider col){
		if (fires.Contains(col))
			fires.Remove(col);
	}
}
