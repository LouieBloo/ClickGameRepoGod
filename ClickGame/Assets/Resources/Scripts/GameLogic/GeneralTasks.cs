using UnityEngine;
using System.Collections;

public class GeneralTasks : MonoBehaviour {

	public GameObject toastPrefab;
	public GameObject floatingTextPrefab;
	
	public void createToast(string input)
	{
		GameObject holder = (GameObject)Instantiate(toastPrefab,Vector3.zero,Quaternion.identity);
		
		holder.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform,false);
		holder.GetComponent<RectTransform>().anchoredPosition = new Vector3(0,120,0);
		
		holder.GetComponent<Toast>().setText(input);
		holder.GetComponent<Toast>().duration = 3.0F;
	}
	
	
	public void createFloatingText(string input, float lifeTime, float xPos, float yPos, Color color)
	{
		GameObject holder = (GameObject)Instantiate(floatingTextPrefab,Vector3.zero,Quaternion.identity);
		holder.GetComponent<FloatingUI>().setup(input,lifeTime,xPos,yPos,color);
		
	}
}
