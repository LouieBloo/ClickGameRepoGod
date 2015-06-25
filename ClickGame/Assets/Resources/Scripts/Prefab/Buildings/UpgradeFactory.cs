using UnityEngine;
using System.Collections;

public class UpgradeFactory : MonoBehaviour {

	public GlobalPrefabs globalPrefabs;
	
	
	
	
	
	
	
	// Use this for initialization
	void Start () {
	}
	
	void OnMouseDown()
	{
		globalPrefabs.globalWeedGame.upgradeFactoryPressed();
	}
	
	public void purchaseGlobalMarketBuilding()
	{
		globalPrefabs.globalGlobalMarket.activate();
	}
	
}
