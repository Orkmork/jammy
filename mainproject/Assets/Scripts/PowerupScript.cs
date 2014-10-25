using UnityEngine;
using System.Collections;

public class PowerupScript : MonoBehaviour {

	CharacterControllerScript fiona;

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player")
		{
			fiona = GameObject.Find ("Character").GetComponent<CharacterControllerScript>();
			fiona.anim.SetBool ("HasWeapon",true);
			Destroy (this.gameObject);
		}
	}
}
