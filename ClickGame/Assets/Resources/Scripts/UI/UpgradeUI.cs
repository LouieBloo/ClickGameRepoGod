using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UpgradeUI : MonoBehaviour {

	public Market market;
	public UpgradeFactory upgradeFactory;

	public Text weedPerClickCostText;
	public Text purchaseGlobalMarketText;
	
	WeedGame weedGame;

	// Use this for initialization
	void Start () {
		weedGame = GameObject.FindGameObjectWithTag("GameLogic").GetComponent<WeedGame>();
	}
	
	public void updateTexts()
	{
		weedPerClickCostText.text = market.weedPerClickUpgradeCost + " Dollars";
		purchaseGlobalMarketText.text = market.globalMarketBuildingCost + " Dollars";
	}
	
	public void buyUpgradePerClickButtonPress()
	{
		if(weedGame.canPressMenuButton())
		{
		
			if(market.buyWeedPerClick())
			{
				//farmHouse.b
			}
			
			updateTexts();
			
		}
		
	}
	
	public void buyGlobalMarketBuilding()
	{
		if(weedGame.canPressMenuButton())
		{
			
			if(market.buyGlobalMarketBuilding())
			{
				upgradeFactory.purchaseGlobalMarketBuilding();
				weedGame.MenuBackButtonPress();
			}
			
			updateTexts();
			
		}
	}
}
