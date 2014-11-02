using UnityEngine;
using System.Collections;
using System;

public class PlayerClass {
	public string name;
	public int speed;
	public int level;

	public PlayerClass(string fName, int fSpeed, int fLevel) {
		name = fName;
		speed = fSpeed;
		level = fLevel;
	}
}