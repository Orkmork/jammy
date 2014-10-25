using UnityEngine;
using System.Collections;

public class CharacterControllerScript : MonoBehaviour {

	public float maxSpeed = 2f;
	bool facingRight = true;

	Animator anim;

	bool grounded = false;
	public Transform groundCheck;
	float groundRadius = 0.2f;
	public LayerMask whatIsGround;
	public float jumpForce = 250f;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		grounded = Physics2D.OverlapCircle (groundCheck.position, groundRadius, whatIsGround);
		anim.SetBool ("Ground", grounded);

		anim.SetFloat ("vSpeed", rigidbody2D.velocity.y);

		float move = Input.GetAxis ("Horizontal");

		anim.SetFloat ("Speed", Mathf.Abs (move));

		rigidbody2D.velocity = new Vector2 (move * maxSpeed, rigidbody2D.velocity.y);

		if(move > 0 && !facingRight)
		{
			Flip();
		}
		else if(move <0 && facingRight)
		{
			Flip();
		}


	}
	
	void Update() {
		float move = Input.GetAxis ("Horizontal");
		if (grounded && Input.GetKeyDown (KeyCode.Space)) {
			anim.SetBool ("Ground", false);
			rigidbody2D.AddForce(new Vector2(0, jumpForce));
		}

		//Crouching-------------
		if (grounded && Input.GetKey(KeyCode.DownArrow )) {
			anim.SetBool ("Crouch", true);
		} 
		if (grounded && Input.GetKeyUp(KeyCode.DownArrow )) {
			anim.SetBool ("Crouch", false);
		} 

		//KickAttack-----------------

		if (grounded && move < 0.01f && Input.GetKey(KeyCode.LeftControl )) {
			anim.SetBool ("KickAttack", true);
		} 
		if (Input.GetKeyUp(KeyCode.LeftControl )) {
			anim.SetBool ("KickAttack", false);
		}else if(Mathf.Abs (move) > 0.01f) {
			anim.SetBool ("KickAttack", false);
		}


	}


	void Flip() {
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}
