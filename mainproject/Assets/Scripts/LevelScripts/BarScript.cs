using UnityEngine;
using System.Collections;

public class BarScript : MonoBehaviour {

	CharacterControllerScript fiona;	
	SoundScript sfx;
	public int costs = 10;

	void Awake()
	{
		fiona = GameObject.Find ("Character").GetComponent<CharacterControllerScript>();
		sfx = GameObject.Find ("Character").GetComponent<SoundScript>();
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player")
		{
			fiona.shopping = true;
			GameObject.Find ("Deathtext").GetComponent<GUIText>().text = "Mit <Aktionstaste> für " + costs + " Münzen erfrischen...";
			//StartCoroutine("ReloadGame");
		}
	}

	void OnTriggerExit2D(Collider2D other) {
		if (other.tag == "Player") {
			fiona.shopping = false;
			GameObject.Find ("Deathtext").GetComponent<GUIText> ().text = "";
		}
	}

	void OnTriggerStay2D(Collider2D other) { 
		//if (Input.GetKeyDown (KeyCode.C)) {
				//		Debug.Log ("Col:" + other.gameObject.tag);
				//}
		if (other.gameObject.tag == "Player") {
			if(Input.GetButtonDown ("Fire1") && fiona.shopping && fiona.buying == false && fiona.coins >= costs && fiona.lives < fiona.maxlives) {
				fiona.buying = true;
				fiona.canmove = false;
				fiona.anim.SetBool ("Buying", true);
				sfx.playHealth();
				fiona.GainLife();
				fiona.ChangeCoins(costs * (-1));
				StartCoroutine(Waitforme ());
			}
		}
	}

	IEnumerator ReloadGame()
	{
		//... pause briefly
		yield return new WaitForSeconds(4);
		//and than do stuff
		GameObject.Find ("Deathtext").GetComponent<GUIText>().text = "";
	}

	IEnumerator Waitforme()
	{
		//... pause briefly
		yield return new WaitForSeconds(1);
		fiona.canmove = true;
		fiona.anim.SetBool ("Buying", false);
		fiona.buying = false;
		//and than do stuff
		//GameObject.Find ("Deathtext").GetComponent<GUIText>().text = "";
	}
}
