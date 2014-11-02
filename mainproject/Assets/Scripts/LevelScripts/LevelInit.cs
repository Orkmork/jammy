using UnityEngine;
using System.Collections;

public class LevelInit : MonoBehaviour {

	GameObject persistentGameObject;
	
	// Use this for initialization
	void Awake () {
		persistentGameObject = GameObject.Find ("LevelManager");
		Hashtable args = persistentGameObject.GetComponent<LevelManager> ().GetSceneArguments ();
		if (args.Count >= 1) {
			if ((string)args ["sourceLevel"] == "MainMenu") {
				if ((string)args ["targetLevel"] == "Credits") {
					GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<HandyScript> ().currentSzene = 14;
				}
			}
			if ((string)args ["sourceLevel"] == "Handy") {
				if ((string)args ["targetLevel"] == "Level") {
					CharacterControllerScript fiona = GameObject.Find ("Character").GetComponent<CharacterControllerScript> ();
					fiona.lives = (int)args ["lives"];
					fiona.coins = (int)args ["coins"];
					fiona.playerName = (string)args["playerName"];
				}
			}
		}
	}
}
