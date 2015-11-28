using UnityEngine;
using System.Collections;
using System;
using FU;

public class GearSelector : Inventory {
	
    [SerializeField] private Animator UIMovementAnimator;

	void Start(){
		CurrentGear = 0;
		sonicHose_UI.ActivateUI();
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown (Controls.ToggleGear)){
			ToggleGear();
		}
	}

	void ToggleGear(){
		if ((int)CurrentGear == Enum.GetNames(typeof(GearEnum)).Length-1){
			CurrentGear = GearEnum.SonicHose;
		}
		else{
			CurrentGear++;
			if (gearInventory[CurrentGear]<=0){
				ToggleGear();
			}
		}

		HighlightActiveGearIcon();
        UIMovementAnimator.SetInteger("AnimState", (int)CurrentGear);
    }

	void HighlightActiveGearIcon(){
		if (CurrentGear == GearEnum.SonicHose) 	sonicHose_UI.ActivateUI();
		else 								  	sonicHose_UI.DeActivateUI();

		if (CurrentGear == GearEnum.K_Bomb) 	k_Bomb_UI.ActivateUI();
		else 									k_Bomb_UI.DeActivateUI();

		if (CurrentGear == GearEnum.BlackDeath) blackDeath_UI.ActivateUI();
		else 									blackDeath_UI.DeActivateUI();
	}
}
