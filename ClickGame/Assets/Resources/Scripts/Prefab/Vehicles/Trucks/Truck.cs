using UnityEngine;
using System.Collections;

public class Truck : MonoBehaviour {


	public float moveSpeed;
	public float refreshSpeed;
	
	float carryingCapacity;
	float carryingCurrently;

	bool movingToCity;
	bool movingToFarm;
	bool loaded;
	public bool stationed;
	
	string animationString;

	Market market;
	Waypoint currentWaypoint;
	TruckDepot truckDepot;
	
	Animator animator;

	// Use this for initialization
	void Start () {
		market = GameObject.FindGameObjectWithTag("GameLogic").GetComponent<Market>();
		truckDepot = GameObject.FindGameObjectWithTag("TruckDepot").GetComponent<TruckDepot>();
		
		animator = GetComponent<Animator>();
		
		
		if(loaded)
		{
			if(movingToCity)
			{
				currentWaypoint = GameObject.FindGameObjectWithTag("WaypointStart").GetComponent<Waypoint>();
				transform.position = truckDepot.getStationOffset();
				changeAnimation();
			}
			else
			{
				transform.position = truckDepot.stationTruck(this);
				stationed = true;
			}
			
			gameObject.transform.parent = GameObject.FindGameObjectWithTag("MainGameObjects").transform;
		}
		else
		{
		}
	}
	
	// Update is called once per frame
	void Update () {
	
		if(movingToCity || movingToFarm)
		{
		
			//movement
			transform.position = Vector2.MoveTowards(transform.position, currentWaypoint.transform.position, moveSpeed * Time.deltaTime);
			
			if(Vector2.Distance(transform.position,currentWaypoint.transform.position) < 0.1F)
			{
				if(movingToCity && currentWaypoint.city)
				{
					
					//reached final waypoint, exchance with market
					market.exchangeWeedForGold(carryingCurrently);
					carryingCurrently = 0;
					
					currentWaypoint = currentWaypoint.nextWaypoint;
					animationString = "";
					changeAnimation();
					
					movingToCity = false;
					movingToFarm = true;
				}
				else if(movingToCity)
				{
					
					currentWaypoint = currentWaypoint.nextWaypoint;
					changeAnimation();
				}
				else if(movingToFarm && currentWaypoint.farm)
				{
					
					currentWaypoint = null;
					//changeAnimation();
					movingToCity = false;
					movingToFarm = false;
				}
				else if(movingToFarm)
				{
					
					currentWaypoint = currentWaypoint.nextWaypoint;
					changeAnimation();
				}
				
			}
		
		
		
		
		}
		else if(stationed)
		{
		}
		else
		{
			//move to station
			//movement
			
			if(truckDepot.canTrucksFindLoads() && lookForLoad())
			{
			}
			else
			{
				
				transform.position = Vector2.MoveTowards(transform.position, truckDepot.getStationOffset(), moveSpeed * Time.deltaTime);
				
				if(Vector2.Distance(transform.position,truckDepot.getStationOffset()) < 0.1F)
				{
					transform.position = truckDepot.stationTruck(this);
					stationed = true;
				}
			}
			
		
		}	
	
	
	}
	
	
	public bool lookForLoad()
	{
		if(market.getWeedAmount() >= carryingCapacity)
		{
			market.modifyWeed(-carryingCapacity);
			carryingCurrently = carryingCapacity;
			movingToCity = true;
			stationed = false;
			
			currentWaypoint = GameObject.FindGameObjectWithTag("WaypointStart").GetComponent<Waypoint>();
			animationString = "Weed";
			changeAnimation();
			
			
			
			return true;
	
		}
		
		return false;
	}
	
	public bool lookForQuickLoad()
	{
		if(market.getWeedAmount() < carryingCapacity && market.getWeedAmount() > 0)
		{
			carryingCurrently = market.getWeedAmount();
			market.modifyWeed(-market.getWeedAmount());
			movingToCity = true;
			stationed = false;
			
			currentWaypoint = GameObject.FindGameObjectWithTag("WaypointStart").GetComponent<Waypoint>();
			animationString = "Weed";
			changeAnimation();
			
			
			return true;
			
		}
		
		return false;
	}
	
	void changeAnimation()
	{
		
	
		if(currentWaypoint != null)
		{
			
			float targetX = Mathf.Round (currentWaypoint.transform.position.x * 100f) / 100f;
			float targetY = Mathf.Round (currentWaypoint.transform.position.y * 100f) / 100f;
			float myX = Mathf.Round (transform.position.x * 100f) / 100f;
			float myY = Mathf.Round (transform.position.y * 100f) / 100f;
			
			float xDistance = Mathf.Abs(targetX - myX);
			float yDistance = Mathf.Abs(targetY - myY);
			
		
			
			
			if(myX > targetX && xDistance > 0.1F)
			{
				animator.Play("truckLeft" + animationString);
			}
			else if(myX < targetX && xDistance > 0.1F)
			{
				animator.Play("truckRight" + animationString);
			}
			else if(myY > targetY && yDistance > 0.1F)
			{
				animator.Play("truckDown" + animationString);
			}
			else if(myY < targetY && yDistance > 0.1F)
			{
				animator.Play("truckUp" + animationString);
			}
		
		
		
		}
		else
		{
			animator.Play("truckLeft");
		}
		
	}
	
	
	
	public TruckSave saveYourself()
	{
		TruckSave save = new TruckSave();
		
		save.carryCapacity = carryingCapacity;
		save.moveSpeed = moveSpeed;
		save.movingToCity = movingToCity;
		save.carryingCurrently = carryingCurrently;
		
		save.xPos = transform.position.x;
		save.yPos = transform.position.y;
		
		return save;
	}
	
	public void loadYourself(TruckSave input)
	{
		loaded = true;
	
		movingToCity = input.movingToCity;
		carryingCapacity = input.carryCapacity;
		moveSpeed = input.moveSpeed;
		carryingCurrently = input.carryingCurrently;
		
		
		
	}
	
	public void upgradeCarryingCapacity(float input)
	{
		carryingCapacity = input;
	}
	
	
	
}













			
			
			
		

