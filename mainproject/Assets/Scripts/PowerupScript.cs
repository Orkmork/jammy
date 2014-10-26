using UnityEngine;
using System.Collections;

public class PowerupScript : MonoBehaviour {

	CharacterControllerScript fiona;

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player")
		{
			fiona = GameObject.Find ("Character").GetComponent<CharacterControllerScript>();
			fiona.hasweapon = true;
			fiona.anim.SetBool ("HasWeapon",true);
			fiona.shots += 10;
			Destroy (this.gameObject);
		}
	}
}
