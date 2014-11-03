using UnityEngine;
using System.Collections;

public class HandyScript : MonoBehaviour {

	//sceen Scale -----
	static float virtualWidth = 1280.0f;
	static float virtualHeight = 720.0f;
	Matrix4x4 matrix;
	float scSX = 1f;
	float scSY = 1f;

	//Story 
	public Texture2D story00;
	public Texture2D story01;
	public Texture2D story02;
	public Texture2D story03;
	public Texture2D story04;
	public Texture2D story05;
	public Texture2D story06;
	public Texture2D story07;
	public Texture2D story08;
	public Texture2D story09;
	public Texture2D story10;
	public Texture2D story11;
	public Texture2D story12;
	public Texture2D story13;
	public Texture2D credits;
	float sizeTexX = 298f;
	float sizeTexY = 586f;
	public float scaleSize = 1f;

	public int currentSzene = 0;

	Texture2D[] storyElements = new Texture2D[15];
	GameObject persistentGameObject;
	int lives;
	int coins;
	string playerName;

	void Awake() {
		persistentGameObject = GameObject.Find ("LevelManager");
		Hashtable args = persistentGameObject.GetComponent<LevelManager> ().GetSceneArguments ();

		if (args.Count >= 1) {
			if (args.ContainsKey ("lives")) {
				lives = (int)args ["lives"];
			}
			if (args.ContainsKey ("coins")) {
				coins = (int)args ["coins"];
			}
			if (args.ContainsKey ("playerName")) {
				playerName = (string)args ["playerName"];
			}
			if (args.ContainsKey ("currentSzene")) {
				currentSzene = (int)args ["currentSzene"];
			}
		}

	}

	void Start () {
		storyElements[0] = story00;
		storyElements[1] = story01;
		storyElements[2] = story02;
		storyElements[3] = story03;
		storyElements[4] = story04;
		storyElements[5] = story05;
		storyElements[6] = story06;
		storyElements[7] = story07;
		storyElements[8] = story08;
		storyElements[9] = story09;
		storyElements[10] = story10;
		storyElements[11] = story11;
		storyElements[12] = story12;
		storyElements[13] = story13;
		storyElements[14] = credits;

		scSX = Screen.width / virtualWidth;
		scSY = Screen.height / virtualHeight;
		matrix = Matrix4x4.TRS (Vector3.zero, Quaternion.identity, new Vector3(Screen.width/virtualWidth, Screen.height/virtualHeight, 1.0f));
	}

	void Update() {
		if (Input.GetButtonDown ("Fire1")) {
			if(currentSzene == 4) {
				Hashtable args = new Hashtable();
				args.Add ("sourceLevel","Handy");
				args.Add ("targetLevel","Level");
				args.Add ("lives",3);
				args.Add ("coins",0);
				args.Add ("playerName",playerName);
				GameObject.Find("LevelManager").GetComponent<LevelManager>().LoadScene("Zoo",args);
			}
			else if(currentSzene == 7) {
				Hashtable args = new Hashtable();
				args.Add ("sourceLevel","Handy");
				args.Add ("targetLevel","Level");
				args.Add ("lives",lives);
				args.Add ("coins",coins);
				args.Add ("playerName",playerName);
				GameObject.Find("LevelManager").GetComponent<LevelManager>().LoadScene("Friedhof",args);
			}
			else if(currentSzene == 10) {
				Hashtable args = new Hashtable();
				args.Add ("sourceLevel","Handy");
				args.Add ("targetLevel","Level");
				args.Add ("lives",lives);
				args.Add ("coins",coins);
				args.Add ("playerName",playerName);
				GameObject.Find("LevelManager").GetComponent<LevelManager>().LoadScene("Pyramide",args);
			}
			else if(currentSzene == 12) {
				Hashtable args = new Hashtable();
				args.Add ("sourceLevel","Handy");
				args.Add ("targetLevel","Level");
				args.Add ("lives",lives);
				args.Add ("coins",coins);
				args.Add ("playerName",playerName);
				GameObject.Find("LevelManager").GetComponent<LevelManager>().LoadScene("Schloss",args);
			}
			else if(currentSzene == 14) {
				Destroy(GameObject.Find("LevelManager"));
				Application.LoadLevel("Title");
			}
			else {
				currentSzene++;
			}
		}
	}


	void OnGUI() {
		GUI.matrix = matrix;
		GUI.SetNextControlName("Handy");
		if (GUI.Button (new Rect ((Screen.width- sizeTexX) / 2 / scSX, (Screen.height - sizeTexY) / 2 / scSY, sizeTexX / scSX, sizeTexY / scSY ), storyElements[currentSzene])) {
			if(currentSzene == 4) {
				Hashtable args = new Hashtable();
				args.Add ("sourceLevel","Handy");
				args.Add ("targetLevel","Level");
				args.Add ("lives",3);
				args.Add ("coins",0);
				args.Add ("playerName",playerName);
				GameObject.Find("LevelManager").GetComponent<LevelManager>().LoadScene("Zoo",args);
			}
			else if(currentSzene == 7) {
				Hashtable args = new Hashtable();
				args.Add ("sourceLevel","Handy");
				args.Add ("targetLevel","Level");
				args.Add ("lives",lives);
				args.Add ("coins",coins);
				args.Add ("playerName",playerName);
				GameObject.Find("LevelManager").GetComponent<LevelManager>().LoadScene("Friedhof",args);
			}
			else if(currentSzene == 10) {
				Hashtable args = new Hashtable();
				args.Add ("sourceLevel","Handy");
				args.Add ("targetLevel","Level");
				args.Add ("lives",lives);
				args.Add ("coins",coins);
				args.Add ("playerName",playerName);
				GameObject.Find("LevelManager").GetComponent<LevelManager>().LoadScene("Pyramide",args);
			}
			else if(currentSzene == 12) {
				Hashtable args = new Hashtable();
				args.Add ("sourceLevel","Handy");
				args.Add ("targetLevel","Level");
				args.Add ("lives",lives);
				args.Add ("coins",coins);
				args.Add ("playerName",playerName);
				GameObject.Find("LevelManager").GetComponent<LevelManager>().LoadScene("Schloss",args);
			}
			else if(currentSzene == 14) {
				Destroy(GameObject.Find("LevelManager"));
				Application.LoadLevel("Title");
			}
			else {
				currentSzene++;
			}
		}
		GUI.FocusControl ("Handy");
	}
}
