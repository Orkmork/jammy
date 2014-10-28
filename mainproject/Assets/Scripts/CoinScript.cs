using UnityEngine;
using System.Collections;

public class CoinScript : MonoBehaviour {

	CharacterControllerScript fiona;
	SoundScript sfx;
	
	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player")
		{
			fiona = GameObject.Find ("Character").GetComponent<CharacterControllerScript>();
			sfx = GameObject.Find ("Character").GetComponent<SoundScript>();
			fiona.coins += 1;
			sfx.playCoins();
			Destroy (this.gameObject);
		}
	}
}
