﻿using UnityEngine;
using System.Collections;

public class LookAtScript : MonoBehaviour {
	
	void Update () {
		transform.LookAt(Camera.main.transform.position);
	}
}
