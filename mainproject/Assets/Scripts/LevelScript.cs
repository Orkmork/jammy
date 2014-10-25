using UnityEngine;
using System.Collections;

public class LevelScript : MonoBehaviour {

	CharacterControllerScript fiona;

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player")
		{
			fiona = GameObject.Find ("Character").GetComponent<CharacterControllerScript>();
			fiona.anim.SetBool ("LevelDone",true);
			fiona.anim.SetFloat("Speed",0f);
			fiona.levelEnd = true;
		}
	}
}
