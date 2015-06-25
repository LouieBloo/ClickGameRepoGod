using UnityEngine;
using System.Collections;

public class FarmHouse : MonoBehaviour {

	public GameObject GameLogicGameObject;
	public GameObject mainGameObjects;
	
	float smallFarmOffsetX;
	float smallFarmOffsetY;
	
	WeedGame mainGame;
	GlobalPrefabs globalPrefabs;
	
	
	// Use this for initialization
	void Start () {
		mainGame = GameLogicGameObject.GetComponent<WeedGame>();
		globalPrefabs = GameLogicGameObject.GetComponent<GlobalPrefabs>();
	}
	
	void OnMouseDown()
	{
		mainGame.farmHousePressed();
	}
	
	
	
	
	
	
	
	
	
	
	public void createFarmHouse()
	{
		GameObject holder = (GameObject)Instantiate(globalPrefabs.smallFarmPrefab,new Vector3(smallFarmOffsetX + transform.position.x,smallFarmOffsetY + transform.position.y,0),Quaternion.identity);
		holder.transform.parent = mainGameObjects.transform; 
		
		smallFarmOffsetY += 0.69F;
		if(smallFarmOffsetY >= 2.07F)
		{
			smallFarmOffsetY = -1.38F;
			smallFarmOffsetX -= 1.26F;
		}
		
	}
	
	
	public void saveData(GodSave inputSave)
	{
		inputSave.smallFarmOffsetX = smallFarmOffsetX;
		inputSave.smallFarmOffsetY = smallFarmOffsetY;
	}
	
	public void loadData(GodSave inputSave)
	{
		smallFarmOffsetX = inputSave.smallFarmOffsetX;
		smallFarmOffsetY = inputSave.smallFarmOffsetY;
	}
	
	public void createDefaultPlayer()
	{
		smallFarmOffsetX = -1.18F;
		smallFarmOffsetY = -1.38F;
	}
}
