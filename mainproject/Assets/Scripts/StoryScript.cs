﻿using UnityEngine;
using System.Collections;

public class StoryScript : MonoBehaviour {

	public Texture2D storyTex;
	
	public float scale = 0.9f;
	
	public int nextLevel = -1;
	
	Vector2 GetCenter()
	{
		return new Vector2(Screen.width/2, Screen.height/2);
	}
	
	Rect GetCenteredRect(int wid, int hei)
	{
		Vector2 c = GetCenter();
		//return new Rect(c.x-wid/2, c.y-hei/2, c.x+wid/2, c.y+hei/2);
		return new Rect(c.x-wid*scale/2, c.y-hei*scale/2, wid*scale, hei*scale);
	}
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey(KeyCode.Return) || Input.GetAxis("Fire1") > 0)
		{
			Confirm ();
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
	}
	
	void Confirm()
	{
		if (nextLevel < 0) {
			Application.LoadLevel(Application.loadedLevel + 1);
		}
		else
		{
			Application.LoadLevel(nextLevel);
		}
	}
}