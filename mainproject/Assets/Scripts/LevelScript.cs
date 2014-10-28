using UnityEngine;
using System.Collections;

public class LevelScript : MonoBehaviour {

	CharacterControllerScript fiona;
	
	SoundScript sfx;

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player")
		{
			fiona = GameObject.Find ("Character").GetComponent<CharacterControllerScript>();
			fiona.anim.SetBool ("LevelDone",true);
			fiona.anim.SetFloat("Speed",0f);
			fiona.levelEnd = true;
			sfx.playLvlend();
			StartCoroutine("NextLevel");
		}
	}
	
	IEnumerator NextLevel()
	{
		//... pause briefly
		yield return new WaitForSeconds(4);
		//and than do stuff
		fiona.anim.SetBool ("LevelDone",false);
		fiona.anim.SetFloat("Speed",0f);
		fiona.levelEnd = false;
		Application.LoadLevel(Application.loadedLevel + 1);
	}
}
