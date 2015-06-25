using UnityEngine;
using System.Collections;
using System.Collections.Generic; 
using System.Runtime.Serialization.Formatters.Binary; 
using System.IO;

[System.Serializable] 
public class GodSave {


	public  List<SmallFarmSave> smallFarms = new List<SmallFarmSave>();
	public List<TruckSave> trucks = new List<TruckSave>();
	
	//Market//
	public float totalGold;
	public float totalWeed;
	
	//Upgrade House
	public float weedPerClick;
	public float weedPerClickUpgradeCost;
	
	//FarmHouse
	public float smallFarmHouseCost;
	public float smallFarmOffsetX;
	public float smallFarmOffsetY;
	
	//Truck Depot
	public bool trucksOn;
	public float truckCarryingCapacityUpgradeCost;
	public float truckCarryingCapacity;
	
	//GlobalMarket
	public bool globalMarketBuilt;
	public float globalMarketExchangeRate;
	
}
