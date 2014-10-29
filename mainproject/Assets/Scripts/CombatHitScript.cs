using UnityEngine;
using System.Collections;

public class CombatHitScript : MonoBehaviour {
	public GameObject DustExplo;
	SoundScript sfx;

	void Awake() {
		sfx = GameObject.Find ("Character").GetComponent<SoundScript>();
	}


	void OnExplode()
	{
		Quaternion randomRotation = Quaternion.Euler (0f, 0f, Random.Range (0f,360f));
		
		GameObject foo = Instantiate (DustExplo, transform.position, randomRotation) as GameObject;
		Destroy (foo, 0.3f);
	}

	void OnTriggerEnter2D(Collider2D col) {
		if (col.tag == "Enemy") {
			sfx.playKick();
			col.GetComponent<Enemy2Script> ().Hurt ();
			OnExplode ();
		} else if (col.tag == "Obstacle") {
			OnExplode ();
		}
	}
}
