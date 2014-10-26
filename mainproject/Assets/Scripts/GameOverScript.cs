using UnityEngine;
using System.Collections;

public class GameOverScript : MonoBehaviour {

	// Update is called once per frame
	void Update () {
	 	if (Input.GetKey (KeyCode.Space)) {
			// save state
			PlayerPrefs.SetInt ("lives", 3);
			PlayerPrefs.SetInt ("coins", 0);
			Application.LoadLevel(0);
		}
		
		LevelControl();
	}
	
	void LevelControl()
	{
		if (Input.GetKeyDown(KeyCode.PageUp))
		{
			Application.LoadLevel(Application.loadedLevel + 1);
		}
		else if (Input.GetKeyDown(KeyCode.PageDown))
		{
			Application.LoadLevel(Application.loadedLevel - 1);
		}
		else if (Input.GetKeyDown(KeyCode.Home))
		{
			Application.LoadLevel(0);
		}
	}
}
