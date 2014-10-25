using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour {

	public float maxSpeed = 2f;
	
	public Transform groundCheck;
	public Transform groundLookAheadCheck;
	public LayerMask whatIsGround;
	
	public float dropChance = 0.2f;
	public float turnChance = 0f;//0.05f;
	
	bool facingRight = true;
	
	bool grounded = false;
	bool groundLookAhead = false;
	
	float groundRadius = 0.2f;
	
//	public float jumpForce = 250f;
	
	Animator anim;
	
	bool RandomTrue(float chance)
	{	
		if (chance > 0f)
		{
			return Random.Range(0f, 1f) < chance;
		}
		else
		{
			return false;
		}
	}
	
	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		
		Run (1f);
	}
	
	// Update is called once per frame
	void Update () {			
	}
	
	void OnTriggerEnter2D(Collider2D other)
	{
		TurnAround();
	}
	
	void TurnAround()
	{
		if (facingRight)
		{
			Run (-1f);
		}
		else
		{
			Run (1f);
		}
	}
	
	void FixedUpdate () {
		grounded = Physics2D.OverlapCircle (groundCheck.position, groundRadius, whatIsGround);
		groundLookAhead = Physics2D.OverlapCircle (groundLookAheadCheck.position, groundRadius, whatIsGround);
		
		if (!groundLookAhead)
		{
			if (RandomTrue(dropChance))
			{
			    TurnAround();
			    return;
		    }
		}
/*
		if (RandomTrue(turnChance)) 
		{
			TurnAround();
			return;
		}
*/		
		if (facingRight)
		{
			Run (1f);
		}
		else
		{
			Run (-1f);
		}
	}
	
	void Run(float move)
	{
//		anim.SetFloat ("Speed", Mathf.Abs (move));
		
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
