using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour {

	private float timer = 30.0f;
	private float currentTime;

	private int level = 1;
	private int difficulty = 1;

	private bool isDead = false;

	public Text scoreText;
	public DeathMenu deathMenu;
	public Text levelText;

	// Update is called once per frame
	void Update () {

		if (isDead)
			return;
		
		
		timer -= Time.deltaTime; // * difficulty;
		scoreText.text = ((int)timer).ToString();

		if (timer < 1.0) {
			LevelUp ();
		}
	}

	void LevelUp(){

		timer += 30.0f; //reset timer

		level++; //increase level
		levelText.text = "Level: " + level.ToString ();

		difficulty++; //increase difficulty

		GetComponent<PlayerMotor> ().SetSpeed (difficulty);

	}

	public void OnDeath()
	{
		isDead = true;

		//saves highscore to computer registry
		if(PlayerPrefs.GetFloat("Highscore") < level)
			PlayerPrefs.SetFloat ("Highscore", level); //key-value pair

		deathMenu.ToggleScore (level);
	}

	public void LowerTime()
	{
		if (timer < 10.0f) {
			timer = timer - 10.0f; //7. -10 = -3. +30 = 27 
			LevelUp ();
		} else {
			timer -= 10.0f;
		}
	}
}
