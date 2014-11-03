using UnityEngine;
using System.Collections;

public class MenuScript : MonoBehaviour {
	//default settings --------------
	public string playerName = "";
	private string _defaultName = "Charactername";
	private int playernameLength = 15;

	//sceen Scale -----
	static float virtualWidth = 1920.0f;
	static float virtualHeight = 1080.0f;
	Matrix4x4 matrix;
	float scSX = 1f;
	float scSY = 1f;

	//background 
	public Texture2D background;

	//main menu settings-----------
	string[] menuElements = new string[4] {"player_name", "button_play", "button_options", "button_credits"};
	int selected = 0;	
	float scaleX = .25f;
	float scaleY = .125f;
	//LevelManager lm;

	//playername field----------
	public GUIStyle playerNameStyle;
	public GUIStyle questionStyle;
	float posNameX = 0.165f;
	float posNameY = 0.25f;
	float sizeNameX = 0.235f;
	float sizeNameY = 0.07f;
	private bool playnameQuestion = false;
	float questionPosY = 0.79f;

	
	//button play-----------
	public GUIStyle buttonPlay;
	float posPlayX = 0.16f;
	float posPlayY = 0.35f;
	
	//button options--------
	public GUIStyle buttonOptions;	
	float posOptionsX = 0.16f;
	float posOptionsY = 0.5f;
	
	//button credits--------
	public GUIStyle buttonCredits;	
	float posCreditsX = 0.16f;
	float posCreditsY = 0.65f;



	void Start() {
		scSX = Screen.width / virtualWidth;
		scSY = Screen.height / virtualHeight;
		matrix = Matrix4x4.TRS (Vector3.zero, Quaternion.identity, new Vector3(Screen.width/virtualWidth, Screen.height/virtualHeight, 1.0f));

		selected = 1;
		// restore state
		if (PlayerPrefs.HasKey ("playerName"))
		{
			playerName = PlayerPrefs.GetString("playerName");
		}
	}
	
	int menuSelection (string[] menuArray, int selectedItem, string direction) {		
		if (direction == "up") {			
			if (selectedItem == 0) {				
				selectedItem = menuArray.Length - 1;				
			} else {				
				selectedItem -= 1;				
			}			
		}
		
		if (direction == "down") {			
			if (selectedItem == menuArray.Length - 1) {				
				selectedItem = 0;				
			} else {				
				selectedItem += 1;				
			}			
		}		
		return selectedItem;		
	}
	
	void Update() {
		if(Input.GetButtonDown ("Vertical") && Input.GetAxis("Vertical") > 0){			
			selected = menuSelection(menuElements, selected, "up");			
		}
		
		if(Input.GetButtonDown ("Vertical") && Input.GetAxis("Vertical") < 0){			
			selected = menuSelection(menuElements, selected, "down");			
		}

		if (Input.GetButtonDown ("Fire1")) {
			if(selected == 1) {
				if(playerName == _defaultName || playerName.Trim () == "")
				{
					GUI.FocusControl ("player_name");
					selected = 0;
					playnameQuestion = true;
				}
				else 
				{
					Debug.Log ("pn:" + playerName);
					playnameQuestion = false;
					GUI.FocusControl ("button_play");
					selected = 1;
					PlayerPrefs.SetString ("playerName", playerName);
					Hashtable args = new Hashtable();
					args.Add ("sourceLevel","MainMenu");
					args.Add ("targetLevel","Handy");
					args.Add ("playerName",playerName);
					GameObject.Find("LevelManager").GetComponent<LevelManager>().LoadScene("Handy",args);
				}
			}else if (selected == 3) {
				GUI.FocusControl ("button_credits");
				selected = 3;
				//load credits----------
				
				Hashtable args = new Hashtable();
				args.Add ("sourceLevel","MainMenu");
				args.Add ("targetLevel","Credits");
				GameObject.Find("LevelManager").GetComponent<LevelManager>().LoadScene("Handy",args);
			}

		}
	}

	void OnGUI() {
		GUI.matrix = matrix;
		//run ---
		Event curEvent = UnityEngine.Event.current;

		//background --------------------
		GUI.DrawTexture(new Rect(0,0,Screen.width / scSX, Screen.height / scSY),background);
		if (playnameQuestion) {

			GUI.Label (new Rect (Screen.width * posNameX / scSX, Screen.height * posNameY * questionPosY / scSY, Screen.width * sizeNameX / scSX, Screen.height * sizeNameY / scSY),  new GUIContent("Gib bitte einen Spielernamen ein!"),questionStyle);
			GUI.FocusControl ("player_name");
			selected = 0;
		}

		//player name field -------------
		GUI.SetNextControlName(menuElements[0]);
		playerName = GUI.TextField(new Rect(Screen.width * posNameX /scSX, Screen.height * posNameY/scSY, Screen.width * sizeNameX/scSX, Screen.height * sizeNameY/scSY), playerName, playernameLength, playerNameStyle);

		//play button ---------------------
		GUI.SetNextControlName(menuElements[1]);
		if(GUI.Button (new Rect(Screen.width * posPlayX/scSX, Screen.height * posPlayY/scSY, Screen.width * scaleX/scSX, Screen.height * scaleY/scSY),"Play",buttonPlay)) {
			if(playerName == _defaultName || playerName.Trim () == "")
			{
				GUI.FocusControl ("player_name");
				selected = 0;
				playnameQuestion = true;
			}
			else 
			{
				playnameQuestion = false;
				GUI.FocusControl ("button_play");
				selected = 1;
				PlayerPrefs.SetString ("playerName", playerName);
				Hashtable args = new Hashtable();
				args.Add ("sourceLevel","MainMenu");
				args.Add ("targetLevel","Handy");
				args.Add ("playerName",playerName);
				GameObject.Find("LevelManager").GetComponent<LevelManager>().LoadScene("Handy",args);
			}

		}

		//options button ------------------
		GUI.SetNextControlName(menuElements[2]);		
		if(GUI.Button (new Rect(Screen.width * posOptionsX/scSX, Screen.height * posOptionsY/scSY, Screen.width * scaleX/scSX, Screen.height * scaleY/scSY),"Options",buttonOptions)) {
			GUI.FocusControl ("button_options");
			selected = 2;
			Debug.Log ("options");
		}		

		//credits button -------------------
		GUI.SetNextControlName(menuElements[3]);		
		if(GUI.Button (new Rect(Screen.width * posCreditsX/scSX, Screen.height * posCreditsY/scSY, Screen.width * scaleX/scSX, Screen.height * scaleY/scSY),"Credits",buttonCredits)) {
			GUI.FocusControl ("button_credits");
			selected = 3;
			//load credits----------

			Hashtable args = new Hashtable();
			args.Add ("sourceLevel","MainMenu");
			args.Add ("targetLevel","Credits");
			GameObject.Find("LevelManager").GetComponent<LevelManager>().LoadScene("Handy",args);
		}
		/*
		//Debug foo
		if (curEvent.type != EventType.Layout && curEvent.type != EventType.Repaint) {
			Debug.Log ("type:" + curEvent.type + " pname:"+playerName.Trim()+" defName:"+_defaultName + " Ret:" + KeyCode.Return + " Ent:" + KeyCode.KeypadEnter + " cur:" + curEvent.keyCode + " curM:" + Input.GetMouseButtonDown(0) + " f:" + GUI.GetNameOfFocusedControl () + " pQ:" + playnameQuestion + " sel:" + selected);
		}
		*/

		//checking playername -----------
		if (Input.GetMouseButtonDown(0) && GUI.GetNameOfFocusedControl () == "player_name") {
			GUI.FocusControl ("player_name");
			selected = 0;
		}
		if (GUI.GetNameOfFocusedControl () == "player_name" && playerName != _defaultName && curEvent.type == EventType.used && (curEvent.keyCode == KeyCode.Return || curEvent.keyCode == KeyCode.KeypadEnter)) {      
			GUI.FocusControl ("button_play");
			selected = 1;
			playnameQuestion = false;
		} else {
			GUI.FocusControl(menuElements[selected]);	
		}

		//entered anything --------------
		if (curEvent.type == EventType.Repaint)
		{
			if (GUI.GetNameOfFocusedControl () == "player_name")
			{
				if (playerName == _defaultName) {
					playerName = "";
				}
			}
			else
			{
				if (playerName == "") {
					playerName = _defaultName;
				}
			}
		}

	}
}
	