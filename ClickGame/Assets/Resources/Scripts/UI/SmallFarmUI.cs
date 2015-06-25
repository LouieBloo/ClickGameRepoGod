using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SmallFarmUI : MonoBehaviour {

	public Text weedPerSecondText;
	public Text levelText;
	public Text weedPerSecondUpgradeText;
	public Text totalWeedText;
	
	public Market market;
	WeedGame weedGame;
	
	SmallFarm currentFarm;

	// Use this for initialization
	void Start () {
		weedGame = GameObject.FindGameObjectWithTag("GameLogic").GetComponent<WeedGame>();
	
	}
	
	
	public void upgradeFarmButtonPressed()
	{
		if(weedGame.canPressMenuButton())
		{
			if(market.upgradeSmallFarmHouse(currentFarm.level))
			{
				currentFarm.upgrade();
				updateTexts();
			}
		}
	}
	
	public void setup(SmallFarm currentFarmIn)
	{
		currentFarm = currentFarmIn;
		updateTexts();
	}
	
	void updateTexts()
	{
		weedPerSecondText.text = "Weed Per Second: " + currentFarm.weedPerUpdate;
		levelText.text = "Level: " + currentFarm.level;
		weedPerSecondUpgradeText.text = market.smallFarmHouseUpgradeCostCalculation(currentFarm.level) + " Dollars";
		totalWeedText.text = "Total weed farmed: " + currentFarm.totalWeedFarmed;
	}
}
