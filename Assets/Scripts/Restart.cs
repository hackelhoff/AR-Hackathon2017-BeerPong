﻿using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Restart : MonoBehaviour {

	public void RestartGame() {
		Application.LoadLevel("Player1");
	}

}