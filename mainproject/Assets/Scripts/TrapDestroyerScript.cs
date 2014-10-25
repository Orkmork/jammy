using UnityEngine;
using System.Collections;

public class TrapDestroyerScript : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player")
		{
			//Debug.Break();
			GameObject.Find("Character").GetComponent<CharacterControllerScript>().LoseLife();
			
			return;
		}
	}
}
