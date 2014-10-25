using UnityEngine;
using System.Collections;

public class CharacterControllerScript : MonoBehaviour {

	public float maxSpeed = 2f;
	bool facingRight = true;

	public Animator anim;

	bool grounded = false;
	public Transform groundCheck;
	float groundRadius = 0.2f;
	public LayerMask whatIsGround;
	public float jumpForce = 250f;
	public float killForce = 1f;
	public bool levelEnd = false;
	public bool alive = true;
	public bool waitForRespawn = false;



	public int lives = 3;
	public int coins = 0;
	
	public bool allowJumpCrouch = true;

	// === state ====================================
	
	public void LoseLife()
	{
		if (lives > 0)
		{
			--lives;
			if(facingRight) {
				killForce *= -1f;
			}


//			rigidbody2D.velocity = new Vector2 (2f * killForce, rigidbody2D.velocity.y);
			rigidbody2D.AddForce(new Vector2(0, 100f));
			rigidbody2D.velocity = new Vector2 (2f * killForce, rigidbody2D.velocity.y);

			anim.SetBool("LostLife",true);
			alive = false;
			waitForRespawn = true;
		}
		else
		{
			Debug.Break();
			return;
		}
	}

	public void GainLife()
	{
		if (lives < 10)
		{
			++lives;
		}
	}
	
	public void IncreaseCoins(int amount)
	{
		coins += amount;
	}

	// === control ==================================
	
	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();

		// restore state
		if (PlayerPrefs.HasKey ("lives"))
		{
			lives = PlayerPrefs.GetInt("lives");
		}
		if (PlayerPrefs.HasKey ("coins"))
		{
			coins = PlayerPrefs.GetInt("coins");
		}
	}

	void OnDisable()
	{
		// save state
		PlayerPrefs.SetInt ("lives", lives);
		PlayerPrefs.SetInt ("coins", coins);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		grounded = Physics2D.OverlapCircle (groundCheck.position, groundRadius, whatIsGround);
		anim.SetBool ("Ground", grounded);

		if (alive) {
						anim.SetFloat ("vSpeed", rigidbody2D.velocity.y);
				}

		float move = Input.GetAxis ("Horizontal");

		if (alive) {
			if (!levelEnd) {
					anim.SetFloat ("Speed", Mathf.Abs (move));
					rigidbody2D.velocity = new Vector2 (move * maxSpeed, rigidbody2D.velocity.y);
			}

			if (move > 0 && !facingRight) {
					Flip ();
			} else if (move < 0 && facingRight) {
					Flip ();
			}
		} else if (waitForRespawn){
			if(Input.GetKeyDown (KeyCode.R))
			{

				anim.SetBool ("LostLife", false);	
				if(!facingRight) Flip ();
				rigidbody2D.position = GameObject.Find ("SpawnPoint").transform.position;
				alive = true;
				waitForRespawn = false;

			}
		}


		if(Input.GetKeyDown (KeyCode.K)){
			LoseLife();
		}
		if(Input.GetKeyDown (KeyCode.L)){
			GainLife();
		}


	}
	
	void Update() {
		if (alive) {
			float move = Input.GetAxis ("Horizontal");
			if (grounded && Input.GetKeyDown (KeyCode.Space)) {
					anim.SetBool ("Ground", false);
					rigidbody2D.AddForce (new Vector2 (0, jumpForce));
			}

			//Crouching-------------
			if (allowJumpCrouch || grounded) {
					if (Input.GetKey (KeyCode.DownArrow)) {
							anim.SetBool ("Crouch", true);
					} 
					if (Input.GetKeyUp (KeyCode.DownArrow)) {
							anim.SetBool ("Crouch", false);
					} 
			}

			//KickAttack-----------------

			if (grounded && move < 0.01f && Input.GetKey (KeyCode.LeftControl)) {
					anim.SetBool ("KickAttack", true);
			} 
			if (Input.GetKeyUp (KeyCode.LeftControl)) {
					anim.SetBool ("KickAttack", false);
			} else if (Mathf.Abs (move) > 0.01f) {
					anim.SetBool ("KickAttack", false);
			}
		}

	}


	void Flip() {
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	IEnumerator waitForMe()
	{
		yield return new WaitForSeconds(2);
	}
}
