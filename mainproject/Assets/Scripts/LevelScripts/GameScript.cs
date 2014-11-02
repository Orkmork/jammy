using UnityEngine;
using System.Net;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class GameScript : MonoBehaviour {
	public bool sent = false; 
	public string internet; 

	//highscore settings-----------
	public string playerName = "Simon";
	public int leveltime;
	public int level = 1;

	//url information------------
	private string playerJson;
	private string backendUrl = "http://perkampus.com/jammy/";
	private string addHighscoreUrl = "http://perkampus.com/jammy/addHighscore";
	private string getHighscoreUrl = "http://perkampus.com/jammy/Highscore/";

	public bool checkInternet()
	{
		internet = GetHtmlFromUri (backendUrl);
		if(internet == "success")
		{
			return true;
		}
		return false;
	}


	public void sendHighscore() {
		if(checkInternet()) {
			List<PlayerClass> p = new List<PlayerClass>();
			p.Add (new PlayerClass(playerName, leveltime,level));
			playerJson = JsonConvert.SerializeObject (p);
			
			WWWForm form = new WWWForm ();
			form.AddField ("data", playerJson);
			WWW www = new WWW (addHighscoreUrl, form);
			StartCoroutine(WaitForRequest(www));
		}
	}

	public void getHighscore(int level) {
		if (checkInternet ()) {
			WWW www = new WWW (getHighscoreUrl + level.ToString());
			StartCoroutine(WaitForRequest(www));
		}
	}

	
	IEnumerator WaitForRequest(WWW www)
	{
		yield return www;
			
		// check for errors
		if (www.error == null)
		{
			sent = true;
			Debug.Log("WWW Ok!: " + www.text);
		} else {
			sent = false;
			Debug.Log("WWW Error: "+ www.error);
		}    
	} 

	public string GetHtmlFromUri(string resource)
	{
		string html = string.Empty;
		HttpWebRequest req = (HttpWebRequest)WebRequest.Create(resource);
		try
		{
			using (HttpWebResponse resp = (HttpWebResponse)req.GetResponse())
			{
				bool isSuccess = (int)resp.StatusCode < 299 && (int)resp.StatusCode >= 200;
				if (isSuccess)
				{
					using (StreamReader reader = new StreamReader(resp.GetResponseStream()))
					{
						char[] cs = new char[7];
						reader.Read(cs, 0, cs.Length);
						foreach(char ch in cs)
						{
							html +=ch;
						}
					}
				}
			}
		}
		catch
		{
			return "";
		}
		return html;
	}

	string FormatTime (float time)
		
	{
		
		int intTime = (int)time;
		int minutes = intTime / 60;
		int seconds = intTime % 60;
		int fraction = (int)time * 10;
		fraction = fraction % 10;
		
		//Build string with format
		// 17[minutes]:21[seconds]:05[fraction]
		
		//  timeText = minutes.ToString () ;
		//timeText = timeText + seconds.ToString ();
		string foo = string.Format ("{0:00}:{1:00}:{2:000}", minutes, seconds,fraction);
		
		// timeText +=  fraction.ToString ();
		return foo;
	}
}
