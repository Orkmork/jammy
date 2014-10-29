using UnityEngine;
using System.Collections;

public class Enemy2Script : MonoBehaviour {
	public float maxSpeed = 2f;
	public int health = 1;

	public LayerMask whatIsGround;
	float lookAheadRadius = 0.1f;	
	bool groundLookAhead = false;
	bool frontLookAhead = false;

		
	public bool facingRight = true;
	public float moveDir = 1f;

	private Transform frontCheck;
	private Transform downCheck;
	
	public void Hurt() {
		health--;
	}

	void Awake()
	{
		frontCheck = transform.Find ("frontCheck").transform;
		downCheck = transform.Find ("downCheck").transform;
	}

	// Update is called once per frame
	void FixedUpdate () {
		frontLookAhead = Physics2D.OverlapCircle (frontCheck.position, 0f, whatIsGround);
		if (frontLookAhead) {
			Flip();
		}
		groundLookAhead = Physics2D.OverlapCircle (downCheck.position, lookAheadRadius, whatIsGround);
		if (!groundLookAhead) {
			Flip();
		}
		if (facingRight) {
			moveDir = 1f;
		} else {
			moveDir = -1f;
		}
		rigidbody2D.velocity = new Vector2 (moveDir * maxSpeed, rigidbody2D.velocity.y);
		if (health <= 0) {
			Death();
		}
	}

	void Death()
	{
		Destroy (gameObject);
	}

	void Flip() {
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}
