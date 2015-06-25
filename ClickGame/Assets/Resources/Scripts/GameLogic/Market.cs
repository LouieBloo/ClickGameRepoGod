using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Market : MonoBehaviour {

	public WeedGame mainGame;
	public GeneralTasks generalTasks;

	public Text residuleIncomeText;
	public Text weedScoreText;
	public Text goldScoreText;
	
	private float residuleIncome;
	
	[HideInInspector]
	public float globalMarketExchangeRate; // given by the globalMarketBuilding
	
	private float totalWeed;
	private float totalGold;
	
	//UPGRADES
	
	public float weedPerClickDefault;
	public float weedPerClickUpgradeCostDefault;
	private float weedPerClick;
	public float weedPerClickGrowth;
	[HideInInspector]
	public float weedPerClickUpgradeCost;
	public float weedPerClickUpgradeCostMultiplier;
	//
	public float globalMarketBuildingCost;
	
	//farmhouse Purchase
	public float smallFarmHouseCostDefault;
	[HideInInspector]
	public float smallFarmHouseCost;
	public float smallFarmHouseCostMultiplier;
	
	//farmhouse Upgrades
	public float smallFarmUpgradeCostDefault;
	public float smallFarmUpgradeCostMultiplier;
	
	//Truck Depot upgrades and purchases
	public float[] truckPurchaseCostPerTruck;
	public float truckCarryingCapacityUpgradeCostDefault;
	[HideInInspector]
	public float truckCarryingCapacityUpgradeCost;
	public float truckCarryingCapacityUpgradeCostMultiplier;
	
	
	void Start()
	{
	}

	public void mainLeafClicked()
	{
		modifyWeed(weedPerClick);
		generalTasks.createFloatingText("+"+weedPerClick,2,Random.Range (-1.0F,1.0F),0,new Color(0.5F,1F,0));
	}


	//UPGRADE FACTORY UPGRADES
	public bool buyWeedPerClick()
	{
		if(getGoldAmount() >= weedPerClickUpgradeCost)
		{
			weedPerClick *= weedPerClickGrowth;
			modifyGold(-weedPerClickUpgradeCost);
			weedPerClickUpgradeCost *= weedPerClickUpgradeCostMultiplier;
			weedPerClickUpgradeCost = Mathf.Round(weedPerClickUpgradeCost);
			
			return true;
		}
		else
		{
			generalTasks.createToast("Not enough money!");
		}
		
		return false;
	}
	
	public bool buyGlobalMarketBuilding()
	{
		if(getGoldAmount() >= globalMarketBuildingCost)
		{
			modifyGold(-globalMarketBuildingCost);
			return true;
		}
		else
		{
			generalTasks.createToast("Not enough money!");
		}
	
		return false;
	}
	
	
	//FARM HOUSE PURCHASES
	public bool buySmallFarmHouse()
	{
		if(getGoldAmount() >= smallFarmHouseCost)
		{
			modifyGold(-smallFarmHouseCost);
			smallFarmHouseCost *= smallFarmHouseCostMultiplier;
			smallFarmHouseCost = Mathf.Round(smallFarmHouseCost);
			
			return true;
		}
		else
		{
			generalTasks.createToast("Not enough money!");
		}
		
		return false;
	}
	
	//SMALL FARM HOUSE UPGRADES
	///
	public bool upgradeSmallFarmHouse(int levelInput)
	{
		if(getGoldAmount() >= smallFarmHouseUpgradeCostCalculation(levelInput))
		{
			modifyGold(-smallFarmHouseUpgradeCostCalculation(levelInput));
			return true;
		}
		else
		{
			generalTasks.createToast("Not enough money!");
		}
		return false;
	}
	
	public float smallFarmHouseUpgradeCostCalculation(float input)
	{
		float initial = smallFarmUpgradeCostDefault;
		
		int x = 1;
		while(x < input)
		{
			initial *= smallFarmUpgradeCostMultiplier;
			x++;
		}
		
		return initial;
	}
	
	
	//Truck Depot
	public bool purchaseNewTruck(int truckAmount)
	{
		if(truckAmount < truckPurchaseCostPerTruck.Length)
		{
			if(getWeedAmount() >= truckPurchaseCostPerTruck[truckAmount])
			{
				modifyWeed(-truckPurchaseCostPerTruck[truckAmount]);
				return true;
			}
			else
			{
				generalTasks.createToast("Not enough weed!");
			}
		}
		return false;
	}
	
	public bool upgradeTruckCarryingCapacity()
	{
		if(getGoldAmount() >= truckCarryingCapacityUpgradeCost)
		{
			modifyGold(-truckCarryingCapacityUpgradeCost);
			truckCarryingCapacityUpgradeCost *= truckCarryingCapacityUpgradeCostMultiplier;
			return true;
		}
		else
		{
			generalTasks.createToast("Not enough money!");
		}
		return false;
	}
	
	
	
	
	public void exchangeWeedForGold(float input)
	{
		modifyGold(input * globalMarketExchangeRate);
	}
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	//GETTERS AND SETTERS AND SAVE LOAD
	
	
	
	public void modifyWeed(float input)
	{
		totalWeed += input;
		updateWeedScore();
	}
	
	public void modifyGold(float input)
	{
		totalGold += input;
		updateGoldScore();
	}
	
	
	public float getWeedAmount()
	{
		return totalWeed;
	}
	
	public float getGoldAmount()
	{
		return totalGold;
	}
	
	void updateWeedScore()
	{
		weedScoreText.text = "" + Mathf.Floor (totalWeed);
	}
	
	void updateGoldScore()
	{
		goldScoreText.text = "" + Mathf.Floor (totalGold);
	}
	
	
	public void modifyResiduleIncome(float input)
	{
		residuleIncome += input;
		
		residuleIncomeText.text = "+ " + residuleIncome;
	}
	
	
	public void getSavedData(GodSave inputSave)
	{
		modifyGold(inputSave.totalGold);
		modifyWeed(inputSave.totalWeed);
		
		weedPerClick = inputSave.weedPerClick;
		weedPerClickUpgradeCost = inputSave.weedPerClickUpgradeCost;
		
		smallFarmHouseCost = inputSave.smallFarmHouseCost;
		
		truckCarryingCapacityUpgradeCost = inputSave.truckCarryingCapacityUpgradeCost;
	}
	
	public void saveData(GodSave inputSave)
	{
		inputSave.totalGold = getGoldAmount();
		inputSave.totalWeed = getWeedAmount();
		
		inputSave.weedPerClick = weedPerClick;
		inputSave.weedPerClickUpgradeCost = weedPerClickUpgradeCost;
		
		inputSave.smallFarmHouseCost = smallFarmHouseCost;
		
		inputSave.truckCarryingCapacityUpgradeCost = truckCarryingCapacityUpgradeCost;
	}
	
	public void createDefaultPlayer()
	{
		weedPerClick = weedPerClickDefault;
		weedPerClickUpgradeCost = weedPerClickUpgradeCostDefault;
		modifyGold(0);
		modifyWeed(0);
		smallFarmHouseCost = smallFarmHouseCostDefault;
		truckCarryingCapacityUpgradeCost = truckCarryingCapacityUpgradeCostDefault;
	}
	
	
	public void debugAdd100()
	{
		modifyWeed(100);
	}
	
	public void debugAdd1000()
	{
		modifyWeed(1000);
	}
	
}
