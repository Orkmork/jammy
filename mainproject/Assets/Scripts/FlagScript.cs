using UnityEngine;
using System.Collections;

public class FlagScript : MonoBehaviour {

	public bool isStartSpawn = true;
	public int spawnSpot = 0;
	public Animator anim;
	CharacterControllerScript fiona;
	SoundScript sfx;

	FlagScript() {

	}

	void Awake() {
		//transform.FindChild ("flag_grey").gameObject.SetActive (false);
		anim = GetComponent<Animator> ();
		if (isStartSpawn) {
			anim.SetBool ("Startspawn", true);
		}
	}
	// Update is called once per frame
	void FixedUpdate () {
		/*if (isActive) {
			anim.SetBool ("Active", true);
		} else {
			anim.SetBool ("Active", false);
		}*/
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player" && !isStartSpawn)
		{
			fiona = GameObject.Find ("Character").GetComponent<CharacterControllerScript>();
			sfx = fiona.GetComponent<SoundScript>();
			sfx.playBox();
			anim.SetBool ("Active", true);
			fiona.curSpawnSpot = spawnSpot;
			BoxCollider2D[] myColliders = gameObject.GetComponents<BoxCollider2D>();
			foreach(BoxCollider2D bc in myColliders) bc.enabled = false;

		}
	}
}
