using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {

	public Transform player;
	public float xMargin = 1f;
	public float yMargin = 1f;
	public float xSmooth = 1f;
	public float ySmooth = 1f;
	public Vector2 maxXAndY;
	public Vector2 minXAndY;


	void Awake() {
		player = GameObject.Find ("Character").GetComponent<CharacterControllerScript>().transform;
	}
	
	bool CheckXMargin() {
		return Mathf.Abs (transform.position.x - player.position.x) > xMargin;
	}

	bool CheckYMargin() {
		return Mathf.Abs (transform.position.y - player.position.y) > yMargin;
	}

	void Update(){
		TrackPlayer ();
	}

	void TrackPlayer() {
		float targetX = transform.position.x;
		float targetY = transform.position.y;
		if (CheckXMargin ())
						targetX = Mathf.Lerp (transform.position.x, player.position.x, xSmooth * Time.deltaTime);
		if (CheckYMargin ())
						targetY = Mathf.Lerp (transform.position.y, player.position.y, ySmooth * Time.deltaTime);
		targetX = Mathf.Clamp (targetX, minXAndY.x, maxXAndY.x);
		targetY = Mathf.Clamp (targetY, minXAndY.y, maxXAndY.y);
		transform.position = new Vector3 (targetX, targetY, transform.position.z);
	}
}
