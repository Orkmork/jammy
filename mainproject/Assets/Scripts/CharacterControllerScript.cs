using UnityEngine;
using System.Collections;

public class CharacterControllerScript : MonoBehaviour {

	public float maxSpeed = 2f;
	public bool facingRight = true;

	public Animator anim;

	public bool grounded = false;
	public Transform groundCheck;
	public GameObject hearthUI;

	float groundRadius = 0.1f;
	public LayerMask whatIsGround;
	public float jumpForce = 250f;
	public float killForce = 1f;
	public bool levelEnd = false;
	public bool alive = true;
	public bool canmove = true;
	public bool waitForRespawn = false;
	public float moveDir = 1f;
	public bool speedJump = false;
	public bool hasweapon = false;
	public bool buying = false;
	public int curSpawnSpot = 0;


	public bool playableDuck = true;
	public bool playableJump = true;

	public int maxlives = 5;
	public int lives = 3;
	public int coins = 0;
	public int shots = 0;

	private Transform frontCheck;
	public bool allowJumpCrouch = true;

	SoundScript sfx;
	string[] attacks = { "KickAttack", "Attack1", "Attack2" };

	void Awake()
	{
		frontCheck = transform.Find ("frontCheck").transform;
	}

	// === state ====================================
	
	public void LoseLife()
	{
		if (lives > 0)
		{
			--lives;
			if(lives == 0) {
				Application.LoadLevel(1);
			}
			if(facingRight) {
				killForce *= -1f;
			}
			sfx.playDie();
			// highlight playerdeath
			SpriteRenderer renderer = GameObject.Find ("Deathscreen").GetComponent<SpriteRenderer>();
			renderer.color = new Color(0f, 0f, 0f, 0.8f);
			GameObject.Find ("Character").GetComponent<SpriteRenderer>().sortingLayerName = "DeathPlayer";
			GameObject.Find ("Deathtext").GetComponent<GUIText>().text = "Hoppala! Da hat's dich wohl erwischt! <R> für respawn...";

			//kill player
			rigidbody2D.AddForce(new Vector2(0, 100f));
			rigidbody2D.velocity = new Vector2 (2f * killForce, rigidbody2D.velocity.y);

			anim.SetBool("LostLife",true);
			alive = false;
			canmove = false;
			waitForRespawn = true;
		}
		else
		{
			lives = 0;
		}
	}

	public void GainLife()
	{
		if (lives < maxlives)
		{
			Vector3 hearthPos;
			hearthPos = transform.position;
			hearthPos.y += 0.15f;
			Instantiate(hearthUI, hearthPos, Quaternion.identity);
			++lives;
		}
	}
	
	public void ChangeCoins(int amount)
	{
		coins += amount;
	}

	// === control ==================================
	
	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		sfx = GameObject.Find ("Character").GetComponent<SoundScript>();

		// restore state
		if (PlayerPrefs.HasKey ("lives"))
		{
			lives = PlayerPrefs.GetInt("lives");
		}
		if (PlayerPrefs.HasKey ("coins"))
		{
			coins = PlayerPrefs.GetInt("coins");
		}
		shots = 0;
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

		if (alive && canmove) {
			anim.SetFloat ("vSpeed", rigidbody2D.velocity.y);
		}

		float move = Input.GetAxis ("Horizontal");

		if (alive && canmove) {
			if (!levelEnd) {
					anim.SetFloat ("Speed", Mathf.Abs (move));
					rigidbody2D.velocity = new Vector2 (move * maxSpeed, rigidbody2D.velocity.y);
			}
			else if(levelEnd)
			{
				sfx.playLvlend();
			}

			if (move > 0 && !facingRight) {
					Flip ();
			} else if (move < 0 && facingRight) {
					Flip ();
			}

		} else if (waitForRespawn){
			if(Input.GetKeyDown (KeyCode.R))
			{
				// highlight playerdeath
				SpriteRenderer renderer = GameObject.Find ("Deathscreen").GetComponent<SpriteRenderer>();
				renderer.color = new Color(0f, 0f, 0f, 0f);
				GameObject.Find ("Character").GetComponent<SpriteRenderer>().sortingLayerName = "Player";
				GameObject.Find ("Deathtext").GetComponent<GUIText>().text = "";

				anim.SetBool ("LostLife", false);	
				if(!facingRight) Flip ();

				Debug.Log ("S:"+"SpawnSpot" + curSpawnSpot );
				rigidbody2D.position = GameObject.FindWithTag("SpawnSpot" + curSpawnSpot).transform.position;
				Debug.Log ("Sx:"+GameObject.FindWithTag("SpawnSpot" + curSpawnSpot).transform.position.x+" Sy:" + GameObject.FindWithTag("SpawnSpot" + curSpawnSpot).transform.position.y);
				alive = true;
				canmove = true;
				waitForRespawn = false;

			}
		}

		if (shots <= 0) {
			anim.SetBool ("HasWeapon", false);	
			hasweapon = false;
		}



	}

	void unsetShoot() {
		anim.SetBool ("Shoot", false);
		anim.SetBool ("DuckShoot", false);
	}
	
	void Update() {
		if (alive && canmove) {
			float move = Input.GetAxis ("Horizontal");
			if (grounded && Input.GetKeyDown (KeyCode.Space) && !anim.GetBool ("WallStick") ) {
					anim.SetBool ("Ground", false);
					rigidbody2D.AddForce (new Vector2 (0, jumpForce));
				sfx.playJump();
			} else if (grounded && Input.GetKeyDown (KeyCode.Space) && anim.GetBool ("WallStick") ) {
				anim.SetBool ("Ground", false);
				float dire = 1f;
				if(facingRight)
					dire = -1f;
				else
					dire = 1f;
				rigidbody2D.AddForce (new Vector2 (jumpForce * dire * 2, jumpForce / 4 * 3));
				sfx.playJump();
			}
			Collider2D[] frontHits = Physics2D.OverlapPointAll(frontCheck.position,whatIsGround);
			//Debug.Log ("Front:" + frontHits.Length);
			if(frontHits.Length >= 1) {
				foreach(Collider2D c in frontHits)
				{
					//Debug.Log ("Tag:" + c.tag);
					if(c.tag == "Obstacle")
					{
						//Debug.Log ("grounded:" + grounded);
						anim.SetBool ("WallStick", true);
						sfx.playWall();
					}
				}
			} else {
				anim.SetBool ("WallStick", false);
			}
			//Crouching-------------
			if ((allowJumpCrouch && !grounded) || grounded) {
				if (Input.GetKey (KeyCode.DownArrow)) {
					if((allowJumpCrouch && !grounded)) {
						anim.SetBool ("CrouchJump", true);
					}
					else if(grounded){
						anim.SetBool ("CrouchJump", false);
						anim.SetBool ("Crouch", true);
					}
					if(playableDuck)
					{
						if(playableDuck)
								sfx.playDuck();
						playableDuck = false;
					}	
				} 
				if (Input.GetKeyUp (KeyCode.DownArrow)) {
					//if((allowJumpCrouch && !grounded)) {
					//	anim.SetBool ("CrouchJump", false);
					//	anim.SetBool ("Crouch", false);
					//}
					//else if(grounded){
						anim.SetBool ("CrouchJump", false);
						anim.SetBool ("Crouch", false);
					//}
					playableDuck = true;
				} 
			}

			//KickAttack-----------------

			if (grounded && Mathf.Abs (move) < 0.01f && !hasweapon && Input.GetKeyDown (KeyCode.LeftControl) && !anim.GetBool ("KickAttack") && !anim.GetBool ("Attack1") && !anim.GetBool ("Attack2") ) {
				sfx.playAttack();
				anim.SetBool (attacks[Random.Range (0,3)], true);

			} 
			if (Mathf.Abs (move) > 0.01f) {
				anim.SetBool ("KickAttack", false);
				anim.SetBool ("Attack1", false);
				anim.SetBool ("Attack2", false);
			}



			//if(Mathf.Abs (move) > 0.01f && Input.GetKeyDown(KeyCode.T) && anim.GetFloat ("Speed") > 0.01f) {
			//	anim.SetFloat ("Speed", 200f);
			//	rigidbody2D.AddForce(new Vector2(2000f * moveDir, 0f));	
			//}
		}

	}

	void resetAttack1() {
		anim.SetBool ("Attack1", false);
	}
	void resetAttack2() {
		anim.SetBool ("Attack2", false);
	}
	void resetKickAttack() {
		anim.SetBool ("KickAttack", false);
	}

	/*
	void OnCollisionStay2D(Collision2D other) { 
		if (Input.GetKeyDown (KeyCode.C)) {
			Debug.Log ("Col:" + other.gameObject.tag);
		}
		if (other.gameObject.tag == "Bar") {
			if(Input.GetKeyDown (KeyCode.B)) {
				ChangeCoins(1);
			}
		}
	}*/

	void Flip() {
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
		moveDir *= -1f;
	}

	IEnumerator waitForMe()
	{
		yield return new WaitForSeconds(2);
	}
}
