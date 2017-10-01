﻿using UnityEngine;
using System.Collections;

public class QuitApplication : MonoBehaviour {

	public void Quit()
	{
		//If we are running in the editor
	#if UNITY_EDITOR
		//Stop playing the scene
		UnityEditor.EditorApplication.isPlaying = false;
		return;
	#endif

		//If we are running in a standalone build of the game
		//Quit the application
		Application.Quit();
	}
}