using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FarmHouseUI : MonoBehaviour {

	public Market market;
	WeedGame weedGame;
	
	public Text farmHouseCostText;
	public FarmHouse farmHouse;

	// Use this for initialization
	void Start () {
		weedGame = GameObject.FindGameObjectWithTag("GameLogic").GetComponent<WeedGame>();
	}
	
	
	
	public void updateTexts()
	{	
		farmHouseCostText.text = market.smallFarmHouseCost + " Dollars";
	}
	
	
	public void buySmallFarmHouseButtonPress()
	{
		if(weedGame.canPressMenuButton())
		{
			updateTexts();
			
			market.gameObject.GetComponent<WeedGame>().MenuBackButtonPress();
			
			if(market.buySmallFarmHouse())
			{
				farmHouse.createFarmHouse();
			}
		}
		
		
		
		
	}
}
