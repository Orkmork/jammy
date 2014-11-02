using UnityEngine;
using System.Collections;

public class LevelScript : MonoBehaviour {

	CharacterControllerScript fiona;
	SoundScript sfx;
	GameScript gameSC;
	HudScript hud;
	public int targetHandyScene;
	public int currentLevel;

	void Awake()
	{		
		fiona = GameObject.Find ("Character").GetComponent<CharacterControllerScript>();
		sfx = GameObject.Find ("Character").GetComponent<SoundScript>();
		gameSC = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<GameScript> ();
		hud = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<HudScript> ();
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player" && !fiona.levelEnd)
		{
			fiona.levelEnd = true;
			fiona.anim.SetBool ("LevelDone",true);
			fiona.anim.SetFloat("Speed",0f);
			hud.timeRunning = false;
			sfx.playLvlend();
			StartCoroutine("NextLevel");
		}
	}
	
	IEnumerator NextLevel()
	{
		//... pause briefly
		yield return new WaitForSeconds(3);
		//and than do stuff

		fiona.anim.SetBool ("LevelDone",false);
		fiona.anim.SetFloat("Speed",0f);
		fiona.levelEnd = false;

		// send highscore ----
		gameSC.level = currentLevel;
		gameSC.leveltime = hud.GetLevelTime();
		gameSC.playerName = fiona.playerName;
		gameSC.sendHighscore();

		//load next level -----
		Hashtable args = new Hashtable();
		args.Add ("sourceLevel","Level");
		args.Add ("targetLevel","Handy");
		args.Add ("lives",fiona.lives);
		args.Add ("coins",fiona.coins);
		args.Add ("playerName",fiona.playerName);
		args.Add ("currentSzene", targetHandyScene);
		GameObject.Find("LevelManager").GetComponent<LevelManager>().LoadScene("Handy",args);
	}
}
