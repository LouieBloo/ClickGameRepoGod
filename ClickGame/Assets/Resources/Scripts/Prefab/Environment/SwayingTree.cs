using UnityEngine;
using System.Collections;

public class SwayingTree : MonoBehaviour {


	public float shineTime;
	float shineTimer;
	Animator anim;
	bool idling = false;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
		shineTime = Random.Range(shineTime - 0.5F,shineTime + 0.5F);
	}
	
	// Update is called once per frame
	void Update () {
	
		shineTimer += Time.deltaTime;
		
		if(shineTimer >= shineTime)
		{
			anim.Play("treeSway");
			shineTimer = 0;
			idling = false;
		}
		else
		{
			if(idling)
			{
				
			}
			else if(shineTimer >= 0.25F)
			{
				idling = true;
				shineTimer = 0;
				anim.Play ("idle");
			}
		}
	
	}
	
	

}
