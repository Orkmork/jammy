using UnityEngine;
using System.Collections;

public class LevelScript : MonoBehaviour {

	CharacterControllerScript fiona;
	SoundScript sfx;

	void Awake()
	{		
		fiona = GameObject.Find ("Character").GetComponent<CharacterControllerScript>();
		sfx = GameObject.Find ("Character").GetComponent<SoundScript>();
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player" && !fiona.levelEnd)
		{
			fiona.levelEnd = true;
			fiona.anim.SetBool ("LevelDone",true);
			fiona.anim.SetFloat("Speed",0f);
			sfx.playLvlend();
			StartCoroutine("NextLevel");
		}
	}
	
	IEnumerator NextLevel()
	{
		//... pause briefly
		yield return new WaitForSeconds(3);
		//and than do stuff
		fiona.anim.SetBool ("LevelDone",false);
		fiona.anim.SetFloat("Speed",0f);
		fiona.levelEnd = false;
		Application.LoadLevel(Application.loadedLevel + 1);
	}
}
