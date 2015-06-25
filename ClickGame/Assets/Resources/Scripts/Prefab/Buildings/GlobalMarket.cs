using UnityEngine;
using System.Collections;

public class GlobalMarket : MonoBehaviour {

	public GameObject GameLogicGameObject;
	public float globalMarketExchangeRateDefault;
	public float globalMarketExchangeRateSubtractor;
	[HideInInspector]
	public float globalMarketExchangeRate;
	
	bool active = false;
	
	
	WeedGame mainGame;
	Market market;
	GlobalPrefabs globalPrefabs;
	
	
	// Use this for initialization
	void Start () {
		mainGame = GameLogicGameObject.GetComponent<WeedGame>();
		globalPrefabs = GameLogicGameObject.GetComponent<GlobalPrefabs>();
		market = GameLogicGameObject.GetComponent<Market>();
		
		
		
	}
	
	void OnMouseDown()
	{
		if(active)
		{
		}
	}
	
	public void activate()
	{
		active = true;
		GetComponent<SpriteRenderer>().color = new Color(1,1,1,1);
	}
	
	public void saveData(GodSave inputSave)
	{
		inputSave.globalMarketExchangeRate = globalMarketExchangeRate;
		inputSave.globalMarketBuilt = active;
	}
	
	public void loadData(GodSave inputSave)
	{
		globalMarketExchangeRate = inputSave.globalMarketExchangeRate;
		
		if(inputSave.globalMarketBuilt)
		{
			active = true;
			GetComponent<SpriteRenderer>().color = new Color(1,1,1,1);
		}
		
		market.globalMarketExchangeRate = globalMarketExchangeRate;
	}
	
	public void createDefaultPlayer()
	{
		globalMarketExchangeRate = globalMarketExchangeRateDefault;
		market.globalMarketExchangeRate = globalMarketExchangeRate;
		
		active = false;
		GetComponent<SpriteRenderer>().color = new Color(0,0,0,0);
	}
}
