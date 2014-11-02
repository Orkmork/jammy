using UnityEngine;
using System.Collections;

public class GameOverScript : MonoBehaviour {

	// Update is called once per frame
	void Update () {
	 	if (Input.GetKeyUp (KeyCode.Space)) {
			// save state
			Destroy(GameObject.Find("LevelManager"));
			Application.LoadLevel("Title");
		}
	}
}
