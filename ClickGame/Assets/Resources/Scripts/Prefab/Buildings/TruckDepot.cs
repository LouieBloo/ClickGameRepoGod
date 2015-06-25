using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TruckDepot : MonoBehaviour {

	public GameObject mainGameObjects;
	public int maxTruckAmount;
	[HideInInspector]
	public float truckCarryingCapacity;
	public float truckCarryingCapacityDefault;
	public float truckCarryingCapacityMultiplier;
	
	public Vector2 stationOffset;
	public Vector2 parkingOrigin;
	public float xOffsetDelta;
	public float yOffsetDelta;
	public float refreshRate;
	public float emergencyRefreshRate;
	int truckAmount;
	float emergencyTimer;
	float refreshTimer;
	
	bool trucksOn;
	
	GlobalPrefabs globalPrefabs;
	

	Truck[] stationedTrucks;
	bool[] stationedTrucksOccupied = new bool[10];

	// Use this for initialization
	void Start () {
		stationedTrucks = new Truck[maxTruckAmount];
		globalPrefabs = GameObject.FindGameObjectWithTag("GameLogic").GetComponent<GlobalPrefabs>();
		
	}
	
	// Update is called once per frame
	void Update () {
	
	
		if(trucksOn)
		{
			refreshTimer += Time.deltaTime;
			emergencyTimer += Time.deltaTime;
			
			if(refreshTimer >= refreshRate)
			{
				refreshTimer = 0;
				tryToSendTruck();
			}
			else if(emergencyTimer >= emergencyRefreshRate)
			{
				tryToSendTruckEmergency();
			}
		}
	
	}
	
	void OnMouseDown()
	{
		GameObject.FindGameObjectWithTag("GameLogic").GetComponent<WeedGame>().truckDepotPressed();
	}
	
	void tryToSendTruck()
	{
		int x = 0;
		while(x < stationedTrucks.Length)
		{
			if(stationedTrucksOccupied[x])
			{
				if(stationedTrucks[x].lookForLoad())
				{
					stationedTrucksOccupied[x] = false;
					stationedTrucks[x] = null;
					emergencyTimer = 0;
				}
				
				break;
				
			}
			
			x++;
		}
	}
	
	void tryToSendTruckEmergency()
	{
		int x = 0;
		while(x < stationedTrucks.Length)
		{
			if(stationedTrucksOccupied[x])
			{
				if(stationedTrucks[x].lookForQuickLoad())
				{
					stationedTrucksOccupied[x] = false;
					stationedTrucks[x] = null;
					emergencyTimer = 0;
					refreshTimer = 0;
				}
				
				break;
				
			}
			
			x++;
		}
	}
		
	public Vector2 stationTruck(Truck input)
	{
		Vector2 holder = new Vector2();
		float yOff = 0;
		int x = 0;
		while(x < stationedTrucks.Length)
		{
			if(!stationedTrucksOccupied[x])
			{
				stationedTrucksOccupied[x] = true;
				stationedTrucks[x] = input;
				break;
			}
			yOff -= yOffsetDelta;
			x++;
		}
		
		holder.x = holder.x + parkingOrigin.x + transform.position.x;
		holder.y = yOff + parkingOrigin.y + transform.position.y;
		
		
		
		
		return holder;
		
	}
	
	public void upgradeAllTrucks()
	{
		mainGameObjects.SetActive(true);
		
		
		truckCarryingCapacity *= truckCarryingCapacityMultiplier;
		
		GameObject[] trucks = GameObject.FindGameObjectsWithTag("Truck");
		foreach(GameObject tr in trucks)
		{
			
			tr.GetComponent<Truck>().upgradeCarryingCapacity(truckCarryingCapacity);
		}
		
		
		
		mainGameObjects.SetActive(false);
	}
	
	public bool canTrucksFindLoads()
	{
		return trucksOn;
	}
	
	public void turnOnTrucks()
	{
		trucksOn = true;
	}
	
	public void turnOffTrucks()
	{
		trucksOn = false;
	}
	
	public void createTruck()
	{
		GameObject holder = (GameObject)Instantiate(globalPrefabs.truckPrefab,getStationOffset(),Quaternion.identity);
		holder.transform.parent = mainGameObjects.transform;
		holder.GetComponent<Truck>().upgradeCarryingCapacity(truckCarryingCapacity); 
		
		truckAmount ++;
	}
	
	public int getTruckAmount()
	{
		
		return truckAmount;
	}
	
	public Vector2 getStationOffset()
	{
		return new Vector2(stationOffset.x + transform.position.x,stationOffset.y + transform.position.y);
	}
	
	public void loadData(GodSave inputSave)
	{
		trucksOn = inputSave.trucksOn;
		truckAmount = inputSave.trucks.Count;
		truckCarryingCapacity = inputSave.truckCarryingCapacity;
	}
	
	public void saveData(GodSave inputSave)
	{
		inputSave.trucksOn = trucksOn;
		inputSave.truckCarryingCapacity = truckCarryingCapacity;
	}
	
	public void createDefaultPlayer()
	{
		trucksOn = true;
		truckAmount = 0;
		truckCarryingCapacity = truckCarryingCapacityDefault;
	}
}
