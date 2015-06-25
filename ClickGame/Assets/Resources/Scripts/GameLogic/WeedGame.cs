using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;	
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic; 
using System.Runtime.Serialization.Formatters.Binary; 
using System.IO;

public class WeedGame : MonoBehaviour {

	public GeneralTasks generalTasks;
	public GlobalPrefabs globalPrefabs;
	
	public GameObject mainGameObjects;
	public GameObject mainGameUI;
	public GameObject upgradeUI;
	public GameObject farmHouseUI;
	public GameObject smallFarmHouseUI;
	public GameObject truckDepotUI;
	
	
	
	float uiTimer;
	
	GodSave godSave;
	
	string savedPlayerIdKey = "PlayerID";
	

	// Use this for initialization
	void Start () {
		setupGame ();
	}
	
	
	void Update()
	{
		uiTimer += Time.deltaTime;
	}
	
	
	public bool canPressMenuButton()
	{
		if(uiTimer >= 0.4F)
		{
			return true;
		}
		return false;
	}
	
	
	
	
	
	//Building and button presses
	
	public void upgradeFactoryPressed()
	{
		//Input.ResetInputAxes();
		uiTimer = 0;
	
		mainGameUI.SetActive(false);
		mainGameObjects.SetActive(false);
		upgradeUI.SetActive(true);
		upgradeUI.GetComponent<UpgradeUI>().updateTexts();
	}
	
	public void farmHousePressed()
	{
		//Input.ResetInputAxes();
		uiTimer = 0;
		
		mainGameUI.SetActive(false);
		mainGameObjects.SetActive(false);
		farmHouseUI.SetActive(true);
		farmHouseUI.GetComponent<FarmHouseUI>().updateTexts();
	}
	
	public void MenuBackButtonPress()
	{
		if(canPressMenuButton())
		{
		
	
			mainGameUI.SetActive(true);
			mainGameObjects.SetActive(true);
			
			farmHouseUI.SetActive(false);
			upgradeUI.SetActive(false);
			smallFarmHouseUI.SetActive(false);
			truckDepotUI.SetActive(false);
		}
	}
	
	public void smallFarmHousePressed()
	{
		//Input.ResetInputAxes();
		uiTimer = 0;
		
		smallFarmHouseUI.SetActive(true);
	}
	
	public void truckDepotPressed()
	{
		//Input.ResetInputAxes();
		uiTimer = 0;
		
		mainGameUI.SetActive(false);
		mainGameObjects.SetActive(false);
		
		truckDepotUI.SetActive(true);
		truckDepotUI.GetComponent<TruckDepotUI>().updateTexts();
		
	}
	
	
	
	
	//LOADING AND SAVING, CREATING NEW CHAR
	
	
	
	public  void SaveMe() {
		
		Debug.Log ("Saving godSave");
		
		BinaryFormatter bf = new BinaryFormatter();
		//Application.persistentDataPath is a string, so if you wanted you can put that into debug.log if you want to know where save games are located
		FileStream file = File.Create (Application.persistentDataPath + "/weedGame.thc"); //you can call it anything you want
		bf.Serialize(file, godSave);
		file.Close();
	}   
	
	public  void LoadMe() {
		if(File.Exists(Application.persistentDataPath + "/weedGame.thc")) {
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(Application.persistentDataPath + "/weedGame.thc", FileMode.Open);
			godSave = (GodSave)bf.Deserialize(file);
			file.Close();
		}
		else
		{
			Debug.Log ("Load error");
		}
	}
	
	

	void setupGame()
	{
		LoadMe();
		getSavedData();
		loadAllObjects();
		//InvokeRepeating("saveData", 30, 30);
	}
	
	void getSavedData()
	{
		if(PlayerPrefs.HasKey(savedPlayerIdKey))
		{
			Debug.Log ("Found Player. Getting market data...");
			
			globalPrefabs.globalMarket.getSavedData(godSave);
			globalPrefabs.globalFarmHouse.loadData(godSave);
			globalPrefabs.globalTruckDepot.loadData(godSave);
			globalPrefabs.globalGlobalMarket.loadData(godSave);
		}
		else
		{
			createNewPlayer();
			
			globalPrefabs.globalMarket.createDefaultPlayer();
			globalPrefabs.globalFarmHouse.createDefaultPlayer();
			globalPrefabs.globalTruckDepot.createDefaultPlayer();
			globalPrefabs.globalGlobalMarket.createDefaultPlayer();
		}
	}
	
	public void saveData()
	{
	
		globalPrefabs.globalMarket.saveData(godSave);
		globalPrefabs.globalFarmHouse.saveData(godSave);
		globalPrefabs.globalTruckDepot.saveData(godSave);
		globalPrefabs.globalGlobalMarket.saveData(godSave);
		saveAllObjects();
		
		SaveMe();
		
		generalTasks.createToast("Game Saved");
		
	}
	
	void createNewPlayer()
	{
		Debug.Log ("Creating New Player");
		
		PlayerPrefs.SetInt(savedPlayerIdKey,5);
		
		godSave = new GodSave();
		//SaveMe();
	}
	
	
	
	public void deletePlayer()
	{
		PlayerPrefs.DeleteAll();
		Application.LoadLevel(0);
	}
	
	
	void loadAllObjects()
	{
	
	
		if(godSave != null)
		{
			foreach(SmallFarmSave sm in godSave.smallFarms)
			{
				GameObject holder = (GameObject)Instantiate(globalPrefabs.smallFarmPrefab,new Vector3(50,0,0),Quaternion.identity);
				holder.GetComponent<SmallFarm>().loadYourself(sm);
			}
			
			foreach(TruckSave ts in godSave.trucks)
			{
				GameObject holder2 = (GameObject)Instantiate(globalPrefabs.truckPrefab,new Vector3(50,0,0),Quaternion.identity);
				holder2.GetComponent<Truck>().loadYourself(ts);
			}
			
		}
		else
		{
			Debug.Log ("Found no godSave");
		}
		
	}
	
	void saveAllObjects()
	{
		if(godSave != null)
		{
			godSave.smallFarms.Clear();
			godSave.trucks.Clear();
			
			GameObject[] holder = GameObject.FindGameObjectsWithTag("SmallFarm");
			foreach(GameObject hol in holder)
			{
				godSave.smallFarms.Add(hol.GetComponent<SmallFarm>().saveYourself());
			}
			
			holder = GameObject.FindGameObjectsWithTag("Truck");	
			foreach(GameObject hol in holder)
			{
				godSave.trucks.Add(hol.GetComponent<Truck>().saveYourself());
			}
			
		}
		else
		{
			Debug.Log ("Found no godSave");
		}
	}
	
}
