using UnityEngine;
using System.Collections;

public class MessageScript : MonoBehaviour {

	public string message = "nothing to see here";
	
	public int width  = 100;
	public int height = 50;	
	
	Vector2 GetCenter()
	{
		return new Vector2(Screen.width/2, Screen.height/2);
	}
	
	Rect GetCenteredRect(int wid, int hei)
	{
		Vector2 c = GetCenter();
		//return new Rect(c.x-wid/2, c.y-hei/2, c.x+wid/2, c.y+hei/2);
		return new Rect(c.x-wid/2, c.y-hei/2, wid, hei);
	}

	// Use this for initialization
	void Start () {
	
	}
	
	void Update()
	{
		if (Input.GetKey(KeyCode.Return) || Input.GetAxis("Fire1") > 0)
		{
			Confirm ();
		}
	}
	
	// Update is called once per frame
	void OnGUI () {
		if (
			GUI.Button (
				GetCenteredRect(width, height),
				message
			)
		)
		{
			Confirm ();
		}
	}
	
	void Confirm()
	{
		Application.LoadLevel(Application.loadedLevel + 1);
	}
}
