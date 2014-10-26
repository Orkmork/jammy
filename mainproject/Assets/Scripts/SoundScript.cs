using UnityEngine;
using System.Collections;

public class SoundScript : MonoBehaviour {

	public AudioClip kick;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.LeftControl)) {
			if(!audio.isPlaying) {
				audio.clip = kick;
				audio.Play ();
			}
		}
	}

	void SoundEnd() {
	}
}
