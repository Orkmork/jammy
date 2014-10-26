using UnityEngine;
using System.Collections;

public class Enemy2Script : MonoBehaviour {
	public float maxSpeed = 2f;
	public int health = 1;

	public LayerMask whatIsGround;
	float lookAheadRadius = 0.1f;	
	bool groundLookAhead = false;

		
	public bool facingRight = true;
	public float moveDir = 1f;

		
	Animator anim;

	private Transform frontCheck;
	private Transform downCheck;
	
	public void Hurt() {
		health--;
	}
	// Use this for initialization
	void Start () {
		//anim = GetComponent<Animator> ();
		//Run (1f);
	}

	void Awake()
	{
		frontCheck = transform.Find ("frontCheck").transform;
		downCheck = transform.Find ("downCheck").transform;
	}

	// Update is called once per frame
	void FixedUpdate () {
		Collider2D[] frontHits = Physics2D.OverlapPointAll(frontCheck.position,whatIsGround);
		foreach(Collider2D c in frontHits)
		{
			Debug.Log ("Hit " + c.tag);
			if(c.tag == "Obstacle");
			{
				Flip();
				break;
			}
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


	void Run(float move)
	{
		anim.SetFloat ("Speed", Mathf.Abs (move));
		
		rigidbody2D.velocity = new Vector2 (move * maxSpeed, rigidbody2D.velocity.y);
		
		if(move > 0 && !facingRight)
		{
			Flip();
		}
		else if(move < 0 && facingRight)
		{
			Flip();
		}
	}
	
	void Flip() {
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}
