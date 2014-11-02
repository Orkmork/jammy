using UnityEngine;
using System.Collections;

public class ParallaxScript : MonoBehaviour {

	public Transform level;
	public Transform mainCamera;
	public Vector2 offset = new Vector2(0, 0);
	public Vector2 speed = new Vector2(0, 0);
	public bool doWrapX = true;
	public bool doWrapY = false;
	
	// Update is called once per frame
	void Update () {
		Vector2 cam = new Vector2(
			mainCamera.position.x,
			mainCamera.position.y
		);
		
		if (level != null) {
			cam.x -= level.position.x;
			cam.y -= level.position.y;
		}
		
		Vector2 ofs = new Vector2 (
			wrapOrClip(cam.x * speed.x + offset.x, doWrapX),
			wrapOrClip(cam.y * speed.y + offset.y, doWrapY)
		);

		renderer.material.mainTextureOffset = ofs;
	}
	
	float wrapOrClip(float x, bool doWrap) {
		if (doWrap) {
			x %= 1.0f;
		}
		else if (x < 0) {
			x = 0;
		}
		else if (x > 0.9999f) {
			x = 1.0f;
		}
		return x;
	}
}
