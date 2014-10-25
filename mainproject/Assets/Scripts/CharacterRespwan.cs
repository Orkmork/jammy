using UnityEngine;
using System.Collections;

public class CharacterRespwan : MonoBehaviour {

	public GameObject player;
	public Transform spawnPoint;

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player")
		{
			other.transform.position = spawnPoint.position;			 
		}
	}
	
}
