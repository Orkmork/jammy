using UnityEngine;
using System.Collections;

public class HudScript : MonoBehaviour {

	CharacterControllerScript playerScript;

	//sceen Scale -----
	static float virtualWidth = 1920.0f;
	static float virtualHeight = 1080.0f;
	Matrix4x4 matrix;
	float scSX = 1f;
	float scSY = 1f;

	//hearts
	public Texture2D heartTex;
	public Texture2D noheartTex;
	float posHeartTexX = 0.003f;
	float posHeartTexY = 0.003f;

	//coins
	public Texture2D coinTex;
	public GUIStyle coinStyle;
	float posCoinTexX = 0.004f;
	float posCoinTexY = 0.06f;

	//ammo
	public Texture2D ammoTex;
	public GUIStyle ammoStyle;
	float posAmmoTexX = 0.004f;
	float posAmmoTexY = 0.1f;

	//level time	
	public Texture2D timeTex;
	public GUIStyle timeStyle;
	float posTimeTexX = 0.915f;
	float posTimeTexY = 0.003f;

	//leveltimer
	public bool timeRunning = true;
	float timer = 0f;
	int minutes = 0;
	int seconds = 0;

	void Start () {
		playerScript = GameObject.Find ("Character").GetComponent<CharacterControllerScript> ();
		scSX = Screen.width / virtualWidth;
		scSY = Screen.height / virtualHeight;
		matrix = Matrix4x4.TRS (Vector3.zero, Quaternion.identity, new Vector3(Screen.width/virtualWidth, Screen.height/virtualHeight, 1.0f));
	}
	
	// Update is called once per frame
	void Update () {
		if (timeRunning) {
				LevelTimer ();
		}
	}

	void OnGUI() 
	{
		GUI.matrix = matrix;

		int lives = playerScript.lives;
		int maxlives = playerScript.maxlives;

		//heart display -----------
		if (!heartTex) {
			Debug.LogError ("Texture for hearts is missing.");
		}
		if (!noheartTex) {
			Debug.LogError ("Texture for nohearts time is missing.");
		}
		for (int i=0; i<lives; i++) {
			GUI.DrawTexture (new Rect (Screen.width * posHeartTexX/scSX + 32/scSX * i, Screen.height * posHeartTexY/scSY, 32/scSX, 32/scSY), heartTex, ScaleMode.ScaleToFit, true, 0f);
		}
		for (int i=lives; i<maxlives; i++) {
			GUI.DrawTexture (new Rect (Screen.width * posHeartTexX/scSX + 32/scSX * i, Screen.height * posHeartTexY/scSY, 32/scSX, 32/scSY), noheartTex, ScaleMode.ScaleToFit, true, 0f);
		}

		//coins display -----------
		if (!coinTex) {
			Debug.LogError ("Texture for coins is missing.");
		}
		GUI.DrawTexture (new Rect (Screen.width * posCoinTexX/scSX, Screen.height * posCoinTexY/scSY, 24/scSX, 24/scSY), coinTex, ScaleMode.ScaleToFit, true, 0f);
		GUI.Label (new Rect(Screen.width * posCoinTexX /scSX + 28/scSX, ((Screen.height-12) * posCoinTexY )/scSY ,100/scSX,32/scSY),new GUIContent(playerScript.coins.ToString()),coinStyle);

		//ammo display -----------
		if (!ammoTex) {
			Debug.LogError ("Texture for ammo is missing.");
		}
		GUI.DrawTexture (new Rect (Screen.width * posAmmoTexX/scSX, Screen.height * posAmmoTexY/scSY, 24/scSX, 21/scSY), ammoTex, ScaleMode.ScaleToFit, true, 0f);
		GUI.Label (new Rect(Screen.width * posAmmoTexX /scSX + 28/scSX, ((Screen.height-10) * posAmmoTexY )/scSY,100/scSX,32/scSY),new GUIContent(playerScript.shots.ToString()),ammoStyle);


		//time display -------------
		if (!timeTex) {
			Debug.LogError ("Texture for level time is missing.");
		}
		GUI.DrawTexture (new Rect (Screen.width * posTimeTexX/scSX, Screen.height * posTimeTexY/scSY, 32/scSX, 32/scSY), timeTex, ScaleMode.ScaleToFit, true, 0f);
		GUI.Label (new Rect(Screen.width * posTimeTexX /scSX + 36/scSX, Screen.height  * posTimeTexY / scSY + 2/scSY,100/scSX,32/scSY),new GUIContent(minutes.ToString ("D2") + ":" + seconds.ToString("D2")),timeStyle);
	}

	// rewrite
	void LevelTimer() {
		timer += Time.deltaTime;
		seconds = (int)(timer % 60);
		minutes = (int)(timer / 60);
	}
	
	public int GetLevelTime() {
		return (int)timer;
	}
}
