using UnityEngine;
using System.Collections;

public class Boat : MonoBehaviour {

	public float moveSpeed;
	public float dockWaitTime;
	float dockWaitTimer;
	
	public float carryingCapacity;
	public float carryingCurrently;
	
	bool loaded;
	bool waitingForLoad = true;
	
	Market market;
	Waypoint currentWaypoint;
	
	Animator animator;
	
	// Use this for initialization
	void Start () {
		market = GameObject.FindGameObjectWithTag("GameLogic").GetComponent<Market>();
		
		//animator = GetComponent<Animator>();
		
		
		if(loaded)
		{
			if(carryingCurrently > 0)
			{
				waitingForLoad = false;
				currentWaypoint = GameObject.FindGameObjectWithTag("BoatStart").GetComponent<Waypoint>().nextWaypoint;
			}
		}
		else
		{
		}
	}
	
	// Update is called once per frame
	void Update () {
	
		dockWaitTimer += Time.deltaTime;
		
		if(waitingForLoad)
		{
			if(dockWaitTimer >= dockWaitTime)
			{
				lookForLoad();
			}
			
		}
		else
		{
			if(Vector2.Distance(transform.position,currentWaypoint.transform.position) < 0.1F)
			{
				transform.position = currentWaypoint.transform.position;
				
				if(currentWaypoint.city)
				{
					market.exchangeWeedForGold(carryingCurrently);
					carryingCurrently = 0;
					currentWaypoint = currentWaypoint.nextWaypoint;
					changeAnimation();
				}
				else if(currentWaypoint.farm)
				{
					waitingForLoad = true;
					dockWaitTimer = 0;
				}
				else
				{
					currentWaypoint = currentWaypoint.nextWaypoint;
					changeAnimation();
				}
			}
			else
			{
				//movement
				transform.position = Vector2.MoveTowards(transform.position, currentWaypoint.transform.position, moveSpeed * Time.deltaTime);
			}	
		}
		
	}
	
	
	public void lookForLoad()
	{
		if(market.getWeedAmount() >= carryingCapacity)
		{
			carryingCurrently = carryingCapacity;
			market.modifyWeed(-carryingCapacity);
			
			currentWaypoint = GameObject.FindGameObjectWithTag("BoatStart").GetComponent<Waypoint>().nextWaypoint;
			
			waitingForLoad = false;
		}
	}
	
	
	void changeAnimation()
	{
		
		
	}
	
	
	
	public BoatSave saveYourself()
	{
		BoatSave save = new BoatSave();
		
		save.carryCapacity = carryingCapacity;
		save.moveSpeed = moveSpeed;
		
		save.carryingCurrently = carryingCurrently;
		
	
		return save;
	}
	
	public void loadYourself(BoatSave input)
	{
		loaded = true;
		
		carryingCapacity = input.carryCapacity;
		moveSpeed = input.moveSpeed;
		carryingCurrently = input.carryingCurrently;
	
	}
	
	public void upgradeCarryingCapacity(float input)
	{
		carryingCapacity = input;
	}
}
