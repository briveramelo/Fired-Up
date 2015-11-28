using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using FU;

public class Inventory : MonoBehaviour {

	protected static Dictionary <GearEnum,int> gearInventory;
	public static GearEnum CurrentGear;

	protected static SonicHose_UI sonicHose_UI;
	protected static K_Bomb_UI k_Bomb_UI;
	protected static BlackDeath_UI blackDeath_UI;

	// Use this for initialization
	void Awake(){
		gearInventory = new Dictionary<GearEnum, int>();
		gearInventory.Add(GearEnum.SonicHose,int.MaxValue);
		gearInventory.Add(GearEnum.K_Bomb,2);
		gearInventory.Add(GearEnum.BlackDeath,3);

		sonicHose_UI = FindObjectOfType<SonicHose_UI>();
		k_Bomb_UI = FindObjectOfType<K_Bomb_UI>();;
		blackDeath_UI = FindObjectOfType<BlackDeath_UI>();;

		k_Bomb_UI.UpdateInventory(gearInventory[GearEnum.K_Bomb]);
		blackDeath_UI.UpdateInventory(gearInventory[GearEnum.BlackDeath]);
	}

}
