using UnityEngine;
using System.Collections;

public class SonicHose_UI : UI_Animations {

	[SerializeField] private EffectSettings sonicEffectSettings;
	[SerializeField] private UVTextureAnimator[] textureAnimatorScripts;
	[SerializeField] private SonicHose sonicHoseScript;
	[SerializeField] private Transform sonicBeamTransform;

	void Update(){
		sonicBeamTransform.localScale = new Vector3(1f,1f,sonicHoseScript.BatteryPower);
	}

	public override void ActivateUI(){
		base.ActivateUI();
		//sonicEffectSettings.IsVisible = true;
		ToggleTextures(true);
		StartCoroutine(DelayedDeActivation());
	}
	
	public override void DeActivateUI(){
		base.DeActivateUI();
		//sonicEffectSettings.IsVisible = false;
		ToggleTextures(false);
	}

	IEnumerator DelayedDeActivation(){
		yield return new WaitForSeconds(1f);
		ToggleTextures(false);
	}

	void ToggleTextures(bool isOn){
		foreach (UVTextureAnimator texAnim in textureAnimatorScripts){
			texAnim.enabled = isOn;
		}
	}
}
