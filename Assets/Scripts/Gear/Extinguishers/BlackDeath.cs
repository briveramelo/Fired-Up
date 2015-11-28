using UnityEngine;
using System.Collections;
using FU;
public class BlackDeath : MonoBehaviour {
	
	[SerializeField]	private GameObject grenade;
	[SerializeField]	private GameObject blackHoleEffect;
	[SerializeField]	private GameObject soundEffect;
	[SerializeField]	private EffectSettings effectSettings;
	[SerializeField]	private ParticleSystem rockParticles;
	[SerializeField]	private LayerMask layersToPull;
	private float holeRadius;
	private float pullForce;
	private bool singularizing;
	private float timeToBeginPulling;
	private float timeToAppear;
	private float timeToSpendPulling;
	
	// Use this for initialization
	void Awake () {
		holeRadius = 10f;
		pullForce = 20f;
		timeToAppear = 3f;
		timeToBeginPulling = 3.5f;
		timeToSpendPulling = 6f;
		StartCoroutine (RipThroughSpaceTime());
	}
	
	IEnumerator RipThroughSpaceTime(){
		yield return new WaitForSeconds(timeToAppear);
		blackHoleEffect.transform.position = grenade.transform.position;
		blackHoleEffect.transform.rotation = Quaternion.identity;
		blackHoleEffect.SetActive(true);
		Destroy(grenade);
		Destroy(GetComponent<Rigidbody>());

		yield return new WaitForSeconds(timeToBeginPulling-timeToAppear);
		soundEffect.SetActive(true);
		singularizing = true;
		StartCoroutine (PullInObjects());

		yield return new WaitForSeconds (timeToSpendPulling);
		effectSettings.IsVisible = false;
		singularizing = false;
	}

	IEnumerator PullInObjects(){
		while (singularizing){
			foreach (Collider col in Physics.OverlapSphere(transform.position,holeRadius,layersToPull)){
				Vector3 pullDir = (transform.position - col.transform.position).normalized;
				float pullFactor = Vector3.Distance(transform.position,col.transform.position)/holeRadius;
				col.attachedRigidbody.AddForce(pullDir * pullForce * pullFactor);
			}
			yield return null;
		}
		yield return null;
	}
	
	void OnTriggerEnter(Collider col){
		if (LayerMaskExtensions.IsInLayerMask(col.gameObject, layersToPull)){
			Destroy(col.gameObject);
		}
	}
}