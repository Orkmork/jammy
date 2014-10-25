using UnityEngine;
using System.Collections;

public class CharacterRespawn : MonoBehaviour {

	public GameObject player;
	public Transform spawnPoint;

	void OnTriggerEnter(Collider other) {
		Destroy (other.gameObject);
	}
}
