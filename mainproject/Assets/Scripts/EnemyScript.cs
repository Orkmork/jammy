using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour {

	public float maxSpeed = 2f;
	
	public Transform groundCheck;
	public Transform groundLookAheadCheck;
	public LayerMask whatIsGround;
	
	public float turnChance = 0f;//0.05f;
	
	public int maxDropTurns = -1;
	
	int randDropTurns = 0;
	int dropTurnCount = 0;	
	
	bool facingRight = true;
	
	bool grounded = false;
	bool groundLookAhead = false;
	bool falling = false;
	
	float groundRadius = 0.2f;
	float lookAheadRadius = 0.1f;
	
	Animator anim;
	
	bool RandomTrue(float chance)
	{	
		if (chance >= 1.0f)
		{
			return true;
		}
		if (chance > 0f)
		{
			return Random.Range(0f, 1f) < chance;
		}
		return false;
	}
	
	void ResetDropTurns()
	{
		dropTurnCount = 0;
		
		if (maxDropTurns < 0)
		{
			return;
		}
		
		randDropTurns = (int)(Random.Range(0, maxDropTurns));
	}
	
	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		
		ResetDropTurns();
		
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
		groundLookAhead = Physics2D.OverlapCircle (groundLookAheadCheck.position, lookAheadRadius, whatIsGround);
		
		if (!groundLookAhead)
		{
			if (maxDropTurns < 0)
			{
				TurnAround();
				return;
			}
			else {
				if (++dropTurnCount <= randDropTurns)
				{
					TurnAround();
				}			
				else if (!grounded)
				{
					ResetDropTurns();				
				}
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
