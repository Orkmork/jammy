using UnityEngine;
using System.Collections;

public class HearthScript : MonoBehaviour {
	GameObject fiona;
	// Use this for initialization
	void Awake () {
		fiona = GameObject.Find("Character");
		Debug.Log("h1x:" + fiona.transform.position.x + " h1y:" + fiona.transform.position.y);
		Debug.Log("h2x:" + transform.position.x + " h2y:" + transform.position.y);
	}

}
