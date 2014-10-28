using UnityEngine;
using System.Collections;

public class GunScript : MonoBehaviour {

	public GameObject laser;
	public float speed = 20f;
	Rigidbody2D thisRig;

	private CharacterControllerScript fiona;
	private Animator anim;

	void Start () {
		fiona = transform.parent.GetComponent<CharacterControllerScript> ();
		anim = transform.parent.GetComponent<Animator>();
		thisRig = laser.GetComponent<Rigidbody2D> ();
	}

	void Update () {
		//Shoot----------------------
		if (fiona.grounded && fiona.hasweapon && !anim.GetBool ("Crouch") && Mathf.Abs (anim.GetFloat ("Speed")) == 0 && Input.GetKeyDown (KeyCode.LeftControl)) {
			anim.SetBool ("Shoot", true);

			if (fiona.facingRight) {
				Rigidbody2D bulletInstance = Instantiate (thisRig, transform.position, Quaternion.Euler (new Vector3 (0, 0, 0))) as Rigidbody2D;
				bulletInstance.velocity = new Vector2 (speed, 0);
			} else {
				Rigidbody2D bulletInstance = Instantiate (thisRig, transform.position, Quaternion.Euler (new Vector3 (0, 0, 180f))) as Rigidbody2D;
				bulletInstance.velocity = new Vector2 (-speed, 0);
			}
			fiona.shots--;
		} 
		if (fiona.grounded && fiona.hasweapon && anim.GetBool ("Crouch") && Mathf.Abs (anim.GetFloat ("Speed")) == 0 && Input.GetKeyDown (KeyCode.LeftControl)) {
			anim.SetBool ("DuckShoot", true);
			
			if (fiona.facingRight) {
				Rigidbody2D bulletInstance = Instantiate (thisRig, transform.position, Quaternion.Euler (new Vector3 (0, 0, 0))) as Rigidbody2D;
				bulletInstance.velocity = new Vector2 (speed, 0);
			} else {
				Rigidbody2D bulletInstance = Instantiate (thisRig, transform.position, Quaternion.Euler (new Vector3 (0, 0, 180f))) as Rigidbody2D;
				bulletInstance.velocity = new Vector2 (-speed, 0);
			}
			fiona.shots--;
		} 
		if (fiona.hasweapon && Input.GetKeyUp (KeyCode.LeftControl)) {
			anim.SetBool ("Shoot", false);
			anim.SetBool ("DuckShoot", false);
		}

	}
}
