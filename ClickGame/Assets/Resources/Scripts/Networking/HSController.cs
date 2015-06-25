using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HSController : MonoBehaviour
{
	
	string addScoreURL = "http://vaptrucking.com/clickGame/addscore.php?"; //be sure to add a ? to your url
	string highscoreURL = "http://vaptrucking.com/clickGame/display.php";
	string addPlayerURL = "http://vaptrucking.com/clickGame/addPlayer.php?";
	
	void Start()
	{
	
	}
	
	public void getScoreButtonPress()
	{
		StartCoroutine(GetScores());
	}
	
	public void getScoreButtonPress2()
	{
		Debug.Log ("Posting");
		//StartCoroutine(PostScores(1,33));
		StartCoroutine(addPlayer("asdf"));
	}
	
	
	
	
	IEnumerator addPlayer(string name)
	{

		string post_url = addPlayerURL + "&name=" + WWW.EscapeURL(name);
		
		// Post the URL to the site and create a download object to get the result.
		WWW hs_post = new WWW(post_url);
		yield return hs_post; // Wait until the download is done
		
		if (hs_post.error != null)
		{
			Debug.Log("There was an error posting the high score: " + hs_post.error);
		}
		else
		{
			Debug.Log (hs_post.text);
		}
	}
	
	
	
	
	
	
	// remember to use StartCoroutine when calling this function!
	IEnumerator PostScores(int playerId, int score)
	{
		//This connects to a server side php script that will add the name and score to a MySQL DB.
		// Supply it with a string representing the players name and the players score.
		//string hash = MD5Test.Md5Sum(playerId + score + secretKey);
		
		
		//Debug.Log ("sent id = " + playerId);
		//Debug.Log ("sent score = " + score);
		
		
		string post_url = addScoreURL + "&playerId=" + playerId + "&score=" + score;
		//string post_url = addScoreURL + "&playerId=" + WWW.EscapeURL(playerId) + "&score=" + score + "&hash=" + hash;
		
		// Post the URL to the site and create a download object to get the result.
		WWW hs_post = new WWW(post_url);
		yield return hs_post; // Wait until the download is done
		
		if (hs_post.error != null)
		{
			Debug.Log("There was an error posting the high score: " + hs_post.error);
		}
	}
	
	// Get the scores from the MySQL DB to display in a GUIText.
	// remember to use StartCoroutine when calling this function!
	IEnumerator GetScores()
	{
		gameObject.GetComponent<Text>().text = "Loading Scores";
		WWW hs_get = new WWW(highscoreURL);
		yield return hs_get;
		
		if (hs_get.error != null)
		{
			Debug.Log("There was an error getting the high score: " + hs_get.error);
		}
		else
		{
			gameObject.GetComponent<Text>().text = hs_get.text; // this is a GUIText that will display the scores in game.
		}
	}
	
}
