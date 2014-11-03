using UnityEngine;
using System.Collections;

public class CharacterControllerScript : MonoBehaviour {

	// player inital config
	public string playerName;
	public int maxlives = 5;
	public int lives = 3;
	public int coins = 0;
	public int shots = 0;
	public float maxSpeed = 2f;
	public int curSpawnSpot = 0;
	string[] attacks = { "KickAttack", "Attack1", "Attack2" };

	//game balancing
	public bool allowJumpCrouch = true;
	public float jumpForce = 250f;
	public float killForce = 2f;

	//collision detect
	public LayerMask whatIsGround;
	public Transform groundCheck;
	private Transform frontCheck;
	float groundRadius = 0.1f;
	public bool grounded = false;
	public bool facingRight = true;
	public float moveDir = 1f;
	public bool shopping = false;

	//sound check
	public bool playableDuck = true;
	public bool playableJump = true;

	//controller stuff
	public Animator anim;
	public GameObject hearthUI;
	SoundScript sfx;

	//animation state mashine
	public bool levelEnd = false;
	public bool alive = true;
	public bool canmove = true;
	public bool waitForRespawn = false;
	public bool speedJump = false;
	public bool hasweapon = false;
	public bool buying = false;

	void Awake()
	{
		frontCheck = transform.Find ("frontCheck").transform;
		//Hashtable arguments = SceneManager.
	}

	// === state ====================================
	
	public void LoseLife()
	{
		if (lives > 0 && alive == true)
		{
			anim.SetBool("LostLife",true);
			alive = false;
			canmove = false;
			waitForRespawn = true;
			anim.SetBool ("CrouchJump", false);
			anim.SetBool ("Crouch", false);
			--lives;
			if(lives == 0) {
				GameObject.Find("LevelManager").GetComponent<LevelManager>().LoadScene("gameover");
			}
			if(facingRight) {
				killForce *= -1f;
			}
			sfx.playDie();
			// highlight playerdeath
			SpriteRenderer renderer = GameObject.Find ("Deathscreen").GetComponent<SpriteRenderer>();
			renderer.color = new Color(0f, 0f, 0f, 0.8f);
			GameObject.Find ("Character").GetComponent<SpriteRenderer>().sortingLayerName = "DeathPlayer";
			GameObject.Find ("Deathtext").GetComponent<GUIText>().text = "Hoppala! Da hat's dich wohl erwischt! <Aktionstaste> für respawn...";

			//kill player
			rigidbody2D.AddForce(new Vector2(0, 100f));
			rigidbody2D.velocity = new Vector2 (killForce, rigidbody2D.velocity.y);


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
		/*
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

				//Debug.Log ("S:"+"SpawnSpot" + curSpawnSpot );
				rigidbody2D.position = GameObject.FindWithTag("SpawnSpot" + curSpawnSpot).transform.position;
				//Debug.Log ("Sx:"+GameObject.FindWithTag("SpawnSpot" + curSpawnSpot).transform.position.x+" Sy:" + GameObject.FindWithTag("SpawnSpot" + curSpawnSpot).transform.position.y);
				alive = true;
				canmove = true;
				waitForRespawn = false;

			}
		}

		if (shots <= 0) {
			anim.SetBool ("HasWeapon", false);	
			hasweapon = false;
		}
		*/
	}

	
	// === animation ==================================

	void unsetShoot() {
		anim.SetBool ("Shoot", false);
		anim.SetBool ("DuckShoot", false);
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
	
	void Flip() {
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
		moveDir *= -1f;
	}
	
	void Update() {
		grounded = Physics2D.OverlapCircle (groundCheck.position, groundRadius, whatIsGround);
		anim.SetBool ("Ground", grounded);

		if (alive && canmove) {
			float move = Input.GetAxis ("Horizontal");
			anim.SetFloat ("vSpeed", rigidbody2D.velocity.y);

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

			if (grounded && Input.GetButtonDown ("Jump") && !anim.GetBool ("WallStick") ) {
					anim.SetBool ("Ground", false);
					rigidbody2D.AddForce (new Vector2 (0, jumpForce));
				sfx.playJump();
			} else if (grounded && Input.GetButtonDown ("Jump") && anim.GetBool ("WallStick") ) {
				anim.SetBool ("Ground", false);
				float dire = 1f;
				if(facingRight)
					dire = -1f;
				else
					dire = 1f;
				rigidbody2D.AddForce (new Vector2 (jumpForce * dire * 2, jumpForce / 4 * 3));
				sfx.playJump();
			}
			//Stick to the wall -------------
			Collider2D[] frontHits = Physics2D.OverlapPointAll(frontCheck.position,whatIsGround);
			if(frontHits.Length >= 1) {
				foreach(Collider2D c in frontHits)
				{
					if(c.tag == "Obstacle")
					{
						anim.SetBool ("WallStick", true);
						sfx.playWall();
					}
				}
			} else {
				anim.SetBool ("WallStick", false);
			}
			//Crouching -------------
			if ((allowJumpCrouch && !grounded) || grounded) {
				if ((Input.GetButtonDown ("Vertical") && Input.GetAxis("Vertical") < 0) || Input.GetButtonDown("Crouch")) {
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
				if ((Input.GetButtonUp ("Vertical") && Input.GetAxis("Vertical") < 0) || Input.GetButtonUp("Crouch")) {
						anim.SetBool ("CrouchJump", false);
						anim.SetBool ("Crouch", false);
					playableDuck = true;
				} 
			}

			//Close combat -----------------

			if (grounded && Mathf.Abs (move) < 0.03f && !hasweapon && !shopping && Input.GetButtonDown ("Fire1") && !anim.GetBool ("KickAttack") && !anim.GetBool ("Attack1") && !anim.GetBool ("Attack2") ) {
				sfx.playAttack();
				anim.SetBool (attacks[Random.Range (0,3)], true);

			} 
			if (Mathf.Abs (move) > 0.03f) {
				anim.SetBool ("KickAttack", false);
				anim.SetBool ("Attack1", false);
				anim.SetBool ("Attack2", false);
			}

			//if(Mathf.Abs (move) > 0.01f && Input.GetKeyDown(KeyCode.T) && anim.GetFloat ("Speed") > 0.01f) {
			//	anim.SetFloat ("Speed", 200f);
			//	rigidbody2D.AddForce(new Vector2(2000f * moveDir, 0f));	
			//}
		}
		else if (waitForRespawn){
			if(Input.GetButtonDown ("Fire1"))
			{
				// highlight playerdeath
				SpriteRenderer renderer = GameObject.Find ("Deathscreen").GetComponent<SpriteRenderer>();
				renderer.color = new Color(0f, 0f, 0f, 0f);
				GameObject.Find ("Character").GetComponent<SpriteRenderer>().sortingLayerName = "Player";
				GameObject.Find ("Deathtext").GetComponent<GUIText>().text = "";
				
				anim.SetBool ("LostLife", false);	
				if(!facingRight) Flip ();
				
				//Debug.Log ("S:"+"SpawnSpot" + curSpawnSpot );
				rigidbody2D.position = GameObject.FindWithTag("SpawnSpot" + curSpawnSpot).transform.position;
				//Debug.Log ("Sx:"+GameObject.FindWithTag("SpawnSpot" + curSpawnSpot).transform.position.x+" Sy:" + GameObject.FindWithTag("SpawnSpot" + curSpawnSpot).transform.position.y);
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
}
