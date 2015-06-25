using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Toast : MonoBehaviour {

	public float duration;
	float durationTimer;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		durationTimer += Time.deltaTime;
		
		if(durationTimer >= duration)
		{
			Destroy(this.gameObject);
		}
	
	}
	
	public void setText(string input)
	{
		GetComponentInChildren<Text>().text = input;
		GetComponent<RectTransform>().sizeDelta = new Vector2(input.Length*25.0F,60.0f);
	}
}
