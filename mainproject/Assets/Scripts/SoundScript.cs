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
	public AudioClip attack;//

	CharacterControllerScript fiona;

	// Use this for initialization
	void Start () {
		fiona = GameObject.Find ("Character").GetComponent<CharacterControllerScript>();
	}
	
	// Update is called once per frame
	void Update () {
		if (fiona.hasweapon && Mathf.Abs (fiona.anim.GetFloat ("Speed")) == 0 && Input.GetKey (KeyCode.LeftControl)) {
			if(!audio.isPlaying) {
				audio.clip = shoot;
				audio.Play ();
			}
		}
	}

	public void playAttack()
	{
		audio.clip = attack;
		audio.Play ();
	}

	public void playKick()
	{
		audio.clip = kick;
		audio.Play ();
	}

	public void playLvlend()
	{
		if (!audio.isPlaying) {
			audio.clip = lvlend;
			audio.Play ();
		}
	}

	public void playDie()
	{
		audio.clip = die;
		audio.Play ();
	}

	public void playCoins()
	{
		audio.clip = coins;
		audio.Play ();
	}

	public void playBox()
	{
		audio.clip = collectbox;
		audio.Play ();
	}

	public void playJump()
	{
		audio.clip = jump;
		audio.Play ();
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
	
	public void playHealth()
	{
		audio.clip = health;
		audio.Play ();
	}

	void SoundEnd() {
	}
}
