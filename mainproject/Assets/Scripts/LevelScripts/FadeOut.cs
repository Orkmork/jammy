using UnityEngine;
using System.Collections;

public class FadeOut : MonoBehaviour {

	Color newColor;

	void Start () {
		//fade in----------
		StartCoroutine (Fade (0,1,1));
		//fade out----------
		StartCoroutine (Fade (2,1,0));
	}

	public IEnumerator Fade(float wait, float duration, int targetAlpha)
	{
		
		float newAlpha = (1 - targetAlpha);
		targetAlpha = ((2 * targetAlpha) - 1);
		yield return new WaitForSeconds (wait);

		while (newAlpha >= 0 && newAlpha <= 1) 
		{
			newColor = gameObject.guiText.color;
			newColor.a = newAlpha; 
			gameObject.guiText.color = newColor;
			
			newAlpha = newAlpha + (Time.deltaTime/duration) * targetAlpha;    
			yield return true; 
		}
		if (gameObject.guiText.color.a != targetAlpha) {
			newColor = gameObject.guiText.color;
			newColor.a = targetAlpha; 
			gameObject.guiText.color = newColor;
			yield return true; 
		}
	}
}
