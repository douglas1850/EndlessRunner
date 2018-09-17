using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {

	public Text highScore;

	// Use this for initialization
	void Start () {
		highScore.text = "Highscore : Level " + ((int)PlayerPrefs.GetFloat("Highscore")).ToString();
	}

	public void PlayGame()
	{
		SceneManager.LoadScene ("Game");
	}
}
