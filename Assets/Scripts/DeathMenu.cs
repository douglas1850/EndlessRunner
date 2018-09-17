using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DeathMenu : MonoBehaviour {

	public Text scoreText;
	public Image backgroundImage;

	private bool isShown = false;

	private float transition = 0.0f;

	// Use this for initialization
	void Start () {
		gameObject.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		if (!isShown)
			return;

		transition += Time.deltaTime;
		backgroundImage.color = Color.Lerp (new Color (0, 0, 0, 0), Color.black, transition);
	}

	public void ToggleScore(float level)
	{
		gameObject.SetActive (true);
		scoreText.text = "You've Reached level " + (level).ToString () + "!";
		isShown = true;
	}

	public void PlayAgain()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().name); //reload current scene
	}

	public void ToMenu()
	{
		SceneManager.LoadScene("Menu");
	}
}
