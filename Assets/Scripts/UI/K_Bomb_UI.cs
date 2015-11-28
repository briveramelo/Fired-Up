using UnityEngine;
using System.Collections;

public class K_Bomb_UI : UI_Animations {


	[SerializeField]	private ParticleSystem iceFog;
	[SerializeField]	private ParticleSystem iceEruption;

	void Awake(){
		StartCoroutine (PlayIce());
	}

	IEnumerator PlayIce(){
		iceFog.Stop();
		iceEruption.Stop();
		
		iceFog.Play();
		iceEruption.Play();
		yield return new WaitForSeconds(0.3f);
		StartCoroutine (PauseIce(0f));
	}

	IEnumerator PauseIce(float timeToPause){
		yield return new WaitForSeconds(timeToPause);
		iceFog.Pause();
		iceEruption.Pause();
	}


	public override void ActivateUI(){
		base.ActivateUI();
		StartCoroutine (PlayIce());
	}

	public override void DeActivateUI(){
		base.DeActivateUI();
		StartCoroutine(PauseIce(0f));
	}



}
