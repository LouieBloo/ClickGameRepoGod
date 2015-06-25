using UnityEngine;
using System.Collections;

public class MainLeaf : MonoBehaviour {

	public GameObject GameLogicGameObject;
	
	
	Animator animator;
	
	//WeedGame mainGame;
	Market market;

	// Use this for initialization
	void Start () {
	
		animator = GetComponent<Animator>();
	
		//mainGame = GameLogicGameObject.GetComponent<WeedGame>();
		market = GameLogicGameObject.GetComponent<Market>();
	}
	
	void OnMouseDown()
	{
		market.mainLeafClicked();
		animator.SetTrigger("leafMove");
	}
}
