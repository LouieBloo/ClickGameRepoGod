using UnityEngine;
using System.Collections;
using System.Collections.Generic; 

public class SmallFarm : MonoBehaviour {


	Market market;
	WeedGame mainGame;
	
	float updateTime = 1;
	float timer;
	[HideInInspector]
	public float weedPerUpdate;
	public float weedPerUpdateMultiplier;
	public float weedPerUpdateDefault;
	[HideInInspector]
	public int level = 1;
	[HideInInspector]
	public float totalWeedFarmed;
	
	bool createdFromLoad =false;
	
	
	
	

	// Use this for initialization
	void Start () {
		market = GameObject.FindGameObjectWithTag("GameLogic").GetComponent<Market>();
		mainGame = GameObject.FindGameObjectWithTag("GameLogic").GetComponent<WeedGame>();
		
		
		if(createdFromLoad)
		{
		}
		else
		{
			weedPerUpdate = weedPerUpdateDefault;
			totalWeedFarmed = 0;
		}
		
		
		market.modifyResiduleIncome(weedPerUpdate);
	}
	
	// Update is called once per frame
	void Update () {
	
		timer += Time.deltaTime;
		
		if(timer >= updateTime)
		{
			market.modifyWeed(weedPerUpdate);
			totalWeedFarmed += weedPerUpdate;
			timer = 0;
		}
	
	}
	
	void OnMouseDown()
	{
		mainGame.smallFarmHousePressed();
		mainGame.smallFarmHouseUI.GetComponent<SmallFarmUI>().setup(this);
	}
	
	
	public void upgrade()
	{
		
		level++;
			
		market.modifyResiduleIncome(-weedPerUpdate);
		weedPerUpdate *= weedPerUpdateMultiplier;
		market.modifyResiduleIncome(weedPerUpdate);
			
	}
	
	
	
	public SmallFarmSave saveYourself()
	{
		SmallFarmSave test = new SmallFarmSave();
		test.farmPosition = new List<float>();
		
		test.farmPosition.Add(transform.position.x);
		test.farmPosition.Add(transform.position.y);
		test.farmPosition.Add(transform.position.z);
		
		test.level = level;
		
		test.weedPerUpdate = weedPerUpdate;
		
		test.totalWeedFarmed = totalWeedFarmed;
		
		return test;
	}
	
	public void loadYourself(SmallFarmSave input)
	{
		createdFromLoad = true;
	
		level = input.level;
		
		weedPerUpdate = input.weedPerUpdate;
		
		totalWeedFarmed = input.totalWeedFarmed;
		
		gameObject.transform.position = new Vector3(input.farmPosition[0],input.farmPosition[1],input.farmPosition[2]);
		gameObject.transform.parent = GameObject.FindGameObjectWithTag("MainGameObjects").transform;
		
	}
}
