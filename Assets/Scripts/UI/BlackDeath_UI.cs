using UnityEngine;
using System.Collections;

public class BlackDeath_UI : UI_Animations {
	
	[SerializeField] private EffectSettings blackDeathEffectSettings;
	[SerializeField] private RotateAround[] rotateAroundScripts;
	[SerializeField] private UVTextureAnimator texAnimScript;
	[SerializeField] private ParticleSystem rockParticleSystem;
	[SerializeField] private ParticleSystem dustParticleSystem;

	void Awake(){
		StartCoroutine (ToggleActivation(true));
		StartCoroutine (DelayedDeActivation());
	}

	public override void ActivateUI(){
		base.ActivateUI();
		StartCoroutine (ToggleActivation(true));
		StartCoroutine (DelayedDeActivation());
	}
	
	public override void DeActivateUI(){
		base.DeActivateUI();
		StartCoroutine (ToggleActivation(false));
	}

	IEnumerator ToggleActivation(bool isOn){
		texAnimScript.enabled = isOn;
		if (isOn){
			rockParticleSystem.Play();
			dustParticleSystem.Play();
		} 
		else{
			rockParticleSystem.Pause();
			dustParticleSystem.Pause();
		}
		foreach (RotateAround rot in rotateAroundScripts){
			rot.enabled = isOn;
		}
		yield return null;
	}

	IEnumerator DelayedDeActivation(){
		yield return new WaitForSeconds(1f);
		StartCoroutine (ToggleActivation(false));
	}
}
