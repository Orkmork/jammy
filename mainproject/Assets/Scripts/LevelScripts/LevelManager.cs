using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {
	private Hashtable sceneArguments;

	void Awake() {
		DontDestroyOnLoad(transform.gameObject);
	}

	public void LoadScene(string sceneName, Hashtable arguments)
	{
		this.sceneArguments = arguments;
		Application.LoadLevel(sceneName);
	}

	public void LoadScene(string sceneName)
	{
		Application.LoadLevel(sceneName);
	}
	
	public Hashtable GetSceneArguments()
	{
		return this.sceneArguments;
	}
}
