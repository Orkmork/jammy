using UnityEngine;
using System.Collections;

public class SetSortingLayer : MonoBehaviour {

	public ParticleSystem system;
	public int layerID;
	public int order;

	// Use this for initialization
	void Start () 
	{
		system.renderer.sortingLayerID = layerID;
		system.renderer.sortingOrder = order;
	}

}
