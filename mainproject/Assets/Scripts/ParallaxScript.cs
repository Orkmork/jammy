using UnityEngine;
using System.Collections;

public class ParallaxScript : MonoBehaviour {

	public Transform mainCamera;
	public float speed = 0;
	
	// Update is called once per frame
	void Update () {
		Vector2 cam = mainCamera.position;
		
		renderer.material.mainTextureOffset = new Vector2(
			(cam.x * speed) % 1.0f,
			(cam.y * speed) % 1.0f
		);
	}
}
