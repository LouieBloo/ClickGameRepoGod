using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TruckDepotUI : MonoBehaviour {

	public Market market;
	
	public Text buyTruckCostText;
	public Text buyCarryingCapacityText;
	public TruckDepot truckDepot;
	
	WeedGame weedGame;
	

	// Use this for initialization
	void Start () {
		weedGame = GameObject.FindGameObjectWithTag("GameLogic").GetComponent<WeedGame>();
	}
	
	public void updateTexts()
	{	
		if(truckDepot.getTruckAmount() >= truckDepot.maxTruckAmount)
		{
			buyTruckCostText.text = "Max Trucks!";
		}
		else
		{
			buyTruckCostText.text = market.truckPurchaseCostPerTruck[truckDepot.getTruckAmount()] + " Weed";
		}
		
		buyCarryingCapacityText.text = market.truckCarryingCapacityUpgradeCost + " Dollars";
		
	}
	
	
	public void buyNewTruckButtonPress()
	{
		if(weedGame.canPressMenuButton())
		{
			if(market.purchaseNewTruck(truckDepot.getTruckAmount()))
			{
				truckDepot.createTruck();
				updateTexts();
				market.gameObject.GetComponent<WeedGame>().MenuBackButtonPress();
			}
			
		}
	
	}
	
	
	public void upgradeTruckCarryingCapacityButtonPress()
	{
		if(weedGame.canPressMenuButton())
		{
			if(market.upgradeTruckCarryingCapacity())
			{
				truckDepot.upgradeAllTrucks();
				updateTexts();
			}
			
		}
		
	}
	
	public void turnOnTruckButton()
	{
		if(weedGame.canPressMenuButton())
		{
			truckDepot.turnOnTrucks();
			market.gameObject.GetComponent<WeedGame>().MenuBackButtonPress();
		}
	}
	
	public void turnOffTruckButton()
	{
		if(weedGame.canPressMenuButton())
		{
			truckDepot.turnOffTrucks();
			market.gameObject.GetComponent<WeedGame>().MenuBackButtonPress();
		}
	}
}
