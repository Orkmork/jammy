using UnityEngine;
using System.Collections;

public class StoryScript : MonoBehaviour {

	public Texture2D storyTex;

	public GUIStyle butPlay;
	public GUIStyle butOptions;
	public GUIStyle butCredits;

	float scaleX = .25f;
	float scaleY = .125f;

	float posB1X = 0.16f;
	float posB1Y = 0.25f;
	float posB2X = 0.16f;
	float posB2Y = 0.4f;
	float posB3X = 0.16f;
	float posB3Y = 0.55f;

	
	public float scale = 1.0f;
	
	public int nextLevel = -1;
	
	public bool scaleTex = true;
	
	Vector2 GetCenter()
	{
		return new Vector2(Screen.width/2, Screen.height/2);
	}
	
	Rect GetCenteredRect(int wid, int hei)
	{
		if (scaleTex)
		{
			return new Rect(0, 0, Screen.width, Screen.height);
		}

		Vector2 c = GetCenter();		
		return new Rect(c.x-wid*scale/2, c.y-hei*scale/2, wid*scale, hei*scale);
	}
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Application.loadedLevel != 0) {
			if ( Input.GetKey (KeyCode.Space)
						|| Input.GetKey (KeyCode.Return)
						|| Input.GetAxis ("Fire1") > 0) {
				Confirm ();
			}
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
	
	void OnGUI () {
		GUI.DrawTexture(GetCenteredRect(storyTex.width, storyTex.height), storyTex);
		if (Application.loadedLevel == 0) {
			PlayerPrefs.SetInt ("lives", 3);
			PlayerPrefs.SetInt ("coins", 0);
			PlayerPrefs.SetInt ("shots", 0);
			if(GUI.Button (new Rect(Screen.width * posB1X, Screen.height * posB1Y, Screen.width * scaleX, Screen.height * scaleY),"",butPlay)) {
				Application.LoadLevel(2);
			}
			if(GUI.Button (new Rect(Screen.width * posB2X, Screen.height * posB2Y, Screen.width * scaleX, Screen.height * scaleY),"",butOptions)){
			}
			if(GUI.Button (new Rect(Screen.width * posB3X, Screen.height * posB3Y, Screen.width * scaleX, Screen.height * scaleY),"",butCredits)){
				Application.LoadLevel(19);
			}
		}
	}
	
	void Confirm()
	{
		if (Application.loadedLevel == 19) {
			Application.LoadLevel(0);
		}
		else if (nextLevel < 0) {
			Application.LoadLevel(Application.loadedLevel + 1);
		}
		else
		{
			Application.LoadLevel(nextLevel);
		}
	}
}
