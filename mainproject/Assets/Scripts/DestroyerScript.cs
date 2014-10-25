using UnityEngine;
using System.Collections;

public class DestroyerScript : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player")
		{
			GameObject.Find ("Character").GetComponent<CharacterControllerScript>().LoseLife();
			return;
		}
	}
}
