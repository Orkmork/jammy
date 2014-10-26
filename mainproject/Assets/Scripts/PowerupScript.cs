using UnityEngine;
using System.Collections;

public class PowerupScript : MonoBehaviour {

	CharacterControllerScript fiona;
	SoundScript sfx;

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player")
		{
			fiona = GameObject.Find ("Character").GetComponent<CharacterControllerScript>();
			sfx = GameObject.Find ("Character").GetComponent<SoundScript>();
			fiona.hasweapon = true;
			fiona.anim.SetBool ("HasWeapon",true);
			fiona.shots += 10;
			sfx.playBox();
			Destroy (this.gameObject);
		}
	}
}
