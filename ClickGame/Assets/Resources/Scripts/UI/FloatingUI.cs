using UnityEngine;
using System.Collections;

public class FloatingUI : MonoBehaviour {
	
	float lifeTime;
	public float moveSpeed;
	public float swayAmount;
	public float swaySpeed;
	float lifeTimeTimer;
	
	float xPos;
	float yPos;
	float xPosInitial;
	
	// Update is called once per frame
	void Update () {
		
		lifeTimeTimer += Time.deltaTime;
		
		if(lifeTimeTimer >= lifeTime)
		{
			Destroy (this.gameObject);
		}
		
		transform.position = Camera.main.WorldToViewportPoint(new Vector3(xPos,yPos));
		
		yPos += Time.deltaTime * moveSpeed;
		
		
		xPos += Time.deltaTime * swaySpeed;
		if(Mathf.Abs(xPosInitial-xPos) >= swayAmount)
		{
			swaySpeed *= -1;
		}
		
		
	}
	
	public void setup(string input, float lifeTimeIn, float xPosIn, float yPosIn, Color color)
	{
		GetComponent<GUIText>().text = input;
		lifeTime = lifeTimeIn;
		xPos = xPosIn;
		yPos = yPosIn;
		xPosInitial = xPos;
		GetComponent<GUIText>().color = color;
	}
}
