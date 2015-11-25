using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using FU;
using System.Linq;

public class FireSpread : MonoBehaviour {
    
	public bool isOnFire;
	private FireSpread[] nearbyFireSpreadScripts;
	[SerializeField] private EffectSettings fireEffectSettings;
	[SerializeField] private GameObject fireParts;
	private float fireRadius;
	private Collider thisCollider;

	void Start () {
		thisCollider = GetComponent<Collider>();
		fireRadius = 1f;
		Collider[] nearbyFireColliders = Physics.OverlapSphere(transform.position,fireRadius,Layers.LayerMasks.allFires.value);
		nearbyFireColliders = nearbyFireColliders.Where(col => col!=thisCollider).ToArray();
		nearbyFireSpreadScripts = new FireSpread[nearbyFireColliders.Length];
		for (int i=0; i<nearbyFireColliders.Length; i++){
			nearbyFireSpreadScripts[i] = nearbyFireColliders[i].GetComponent<FireSpread>();
		}

		if (isOnFire) CatchFire();
    }

	public void CatchFire(){
		fireParts.SetActive(true);
		fireEffectSettings.IsVisible = true;
		isOnFire = true;
		StartCoroutine(SetFires());
	}

    IEnumerator SetFires(){
        yield return new WaitForSeconds(5f);
		if (nearbyFireSpreadScripts.Length > 0 && isOnFire){
			for (int i = 0; i < nearbyFireSpreadScripts.Length; i++){
				if (!nearbyFireSpreadScripts[i].isOnFire){
					nearbyFireSpreadScripts[i].CatchFire();
                }
            }
        }
        StartCoroutine(SetFires());
    }

    public void ExtinguishFire(){
		fireEffectSettings.IsVisible = false;
        isOnFire = false;
    }

	void OnDrawGizmosSelected(){
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position + Vector3.up*.5f,fireRadius);
	}




	void printLocalFires(){
		if(nearbyFireSpreadScripts.Length > 0){
			for(int i = 0; i < nearbyFireSpreadScripts.Length-1; i++)   //foreach (FireSpread fire in fireList){
			Debug.Log(nearbyFireSpreadScripts[i].name);
		}
	}
}
