using UnityEngine;
using System.Collections;

public class WeaponController : MonoBehaviour {

	public float fireRate = 0;
	public float Damage = 10;
	public LayerMask whatToHit;
	CharacterControllerScript fiona;

	float timeToFire = 0;
	Transform firePoint;

	// Use this for initialization
	void Awake () {
		firePoint = transform.FindChild ("FirePoint");
		if (firePoint == null) {
			Debug.LogError("No firepint? Dafuq?!");
		}
		fiona = transform.GetComponent<CharacterControllerScript> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (fireRate == 0) {
			if (Input.GetKeyDown (KeyCode.F)) {
				fiona.anim.SetBool ("Shoot",false);
				Shoot ();

			}
		} else {
			if(Input.GetKey (KeyCode.F) && Time.time > timeToFire)
			{
				timeToFire = Time.time + 1/fireRate;
				Shoot();
			}
		}
	}

	void Shoot() {
		float dire = 1f;
		if (fiona.facingRight) {
			dire = 1f;
		} else {
			dire = -1f;
		}
		Vector2 direction = new Vector2 (firePoint.position.x + 100 * dire, firePoint.position.y);
		Vector2 firePointPosition = new Vector2 (firePoint.position.x, firePoint.position.y);
		RaycastHit2D hit = Physics2D.Raycast (firePointPosition, direction - firePointPosition, 100, whatToHit);
		Debug.DrawLine (firePointPosition, (direction - firePointPosition) * 100, Color.cyan );
		if (hit.collider != null) {
			Debug.DrawLine (firePointPosition, hit.point, Color.red);
			Debug.Log ("We hit" + hit.collider.name + " and did " + Damage + " Damage!");
		}
	}
}
