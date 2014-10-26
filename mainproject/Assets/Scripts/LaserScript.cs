using UnityEngine;
using System.Collections;

public class LaserScript : MonoBehaviour {
	public GameObject explosion;

	// Use this for initialization
	void Start () {
		Destroy (gameObject, 2);
	}

	void OnExplode()
	{
		Quaternion randomRotation = Quaternion.Euler (0f, 0f, Random.Range (0f,360f));

		GameObject foo = Instantiate (explosion, transform.position, randomRotation) as GameObject;
		Destroy (foo, 0.3f);

	}

	void OnTriggerEnter2D(Collider2D col) {
		if (col.tag == "Enemy") {
			col.GetComponent<Enemy2Script>().Hurt();
			OnExplode ();
			Destroy (gameObject);
		}
		else if (col.tag == "Obstacle") {
			OnExplode ();
			Destroy (gameObject);
		} else if (col.gameObject.tag == "Player") {
			OnExplode ();
			Destroy (gameObject);
		}
	}
}
