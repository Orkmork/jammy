using UnityEngine;
using System.Collections;

public class SoundScript : MonoBehaviour {

	public AudioClip kick;//
	public AudioClip lvlend;//
	public AudioClip colision;
	public AudioClip run;
	public AudioClip die;//
	public AudioClip mummy;
	public AudioClip health;
	public AudioClip ghost;
	public AudioClip wolfdie;
	public AudioClip zombies;
	public AudioClip coins;//
	public AudioClip shoot;//
	public AudioClip collectbox;//#
	public AudioClip jump;//
	public AudioClip duck;//

	CharacterControllerScript fiona;

	// Use this for initialization
	void Start () {
		fiona = GameObject.Find ("Character").GetComponent<CharacterControllerScript>();
	}
	
	// Update is called once per frame
	void Update () {
		if (!fiona.hasweapon && Input.GetKey (KeyCode.LeftControl)) {
			if(!audio.isPlaying) {
				audio.clip = kick;
				audio.Play ();
			}
		}
		if (fiona.hasweapon && Input.GetKey (KeyCode.LeftControl)) {
			if(!audio.isPlaying) {
				audio.clip = shoot;
				audio.Play ();
			}
		}
	}

	public void playLvlend()
	{
		if(!audio.isPlaying) {
			audio.clip = lvlend;
			audio.Play ();
		}
	}

	public void playDie()
	{
		if(!audio.isPlaying) {
			audio.clip = die;
			audio.Play ();
		}
	}

	public void playCoins()
	{
		if(!audio.isPlaying) {
			audio.clip = coins;
			audio.Play ();
		}
	}

	public void playBox()
	{
		if(!audio.isPlaying) {
			audio.clip = coins;
			audio.Play ();
		}
	}

	public void playJump()
	{
		if(!audio.isPlaying) {
			audio.clip = jump;
			audio.Play ();
		}
	}

	public void playDuck()
	{
		if(!audio.isPlaying) {
			audio.clip = duck;
			audio.Play ();
		}
	}

	public void playWall()
	{
		if(!audio.isPlaying) {
			audio.clip = colision;
			audio.Play ();
		}
	}

	void SoundEnd() {
	}
}
