using UnityEngine;
using System.Collections;

public class GameOverScript : MonoBehaviour {

	//sceen Scale -----
	static float virtualWidth = 1920.0f;
	static float virtualHeight = 1080.0f;
	Matrix4x4 matrix;
	float scSX = 1f;
	float scSY = 1f;

	//background 
	public Texture2D background;
	public GUIStyle textStyle;

	void Start() {
		scSX = Screen.width / virtualWidth;
		scSY = Screen.height / virtualHeight;
		matrix = Matrix4x4.TRS (Vector3.zero, Quaternion.identity, new Vector3(Screen.width/virtualWidth, Screen.height/virtualHeight, 1.0f));
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown ("Fire1")) {
			// save state
			Destroy(GameObject.Find("LevelManager"));
			Application.LoadLevel("Title");
		}
	}

	void OnGUI() {
		GUI.matrix = matrix;		
		//background --------------------
		GUI.DrawTexture (new Rect (0, 0, Screen.width / scSX, Screen.height / scSY), background);
		GUI.Label (new Rect (Screen.width / 2 / scSX, (Screen.height / 2 - 30 ) / scSY, 150/ scSX, 50/ scSY),  new GUIContent("Das war ja mal ein glatter Reinfall!"),textStyle);
		GUI.Label (new Rect (Screen.width / 2 / scSX, (Screen.height / 2 + 30 ) / scSY, 150/ scSX, 50/ scSY),  new GUIContent("Drücke <Aktionstaste> um es noch einmal zu versuchen."),textStyle);
	}
}
