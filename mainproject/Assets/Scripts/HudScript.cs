using UnityEngine;
using System.Collections;

public class HudScript : MonoBehaviour {

	CharacterControllerScript playerScript;

	public Texture2D lifeTex;
	public Texture2D coinTex;
	public Texture2D chestTex;
	public GUIStyle guiStyle;

	// Use this for initialization
	void Start () {
		playerScript = GameObject.Find ("Character").GetComponent<CharacterControllerScript> ();
	}
	
	// Update is called once per frame
	void Update () {
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

	void OnGUI() 
	{
		int lives = playerScript.lives;
		//GUI.Label (new Rect (10, 10, 100, 50), "Lives: " + lives);

		Vector2 o = new Vector2 (5, 5);

		int w = lifeTex.width;
		Vector2 p = o;
		for (int i=0; i<lives; i++)
		{
			GUI.Label(new Rect(p.x, p.y, p.x+w, p.y+w), lifeTex);
			p.x += w + 5;
		}

		GUI.Label(new Rect(o.x, o.y+w, o.x+100, o.y+2*w), new GUIContent(playerScript.coins.ToString(), coinTex),guiStyle);
		GUI.Label(new Rect(o.x, o.y+2*w, o.x+100, o.y+3*w), new GUIContent(playerScript.shots.ToString(), chestTex),guiStyle);
	}
}
