using UnityEngine;
using System.Collections;

public class DotzScript : MonoBehaviour {

	public GameObject DustExplo;
		
	void OnExplode()
	{
		Quaternion randomRotation = Quaternion.Euler (0f, 0f, Random.Range (0f,360f));
		GameObject foo = Instantiate (DustExplo, transform.position, randomRotation) as GameObject;
		Destroy (foo, 0.3f);
	}
	
	void OnTriggerEnter2D(Collider2D col) {
		if (col.tag == "Obstacle") {
			OnExplode ();
		}
	}
}
