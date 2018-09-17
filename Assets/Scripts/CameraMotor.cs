using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMotor : MonoBehaviour {

	private Transform lookAt; //Transform holds position of the player object
	private Vector3 startOffset;
	private Vector3 moveVector;

	private float transition = 0.0f; //transition from start menu to game delay
	private float animationDuration = 1.5f; //1.5 second delay
	private Vector3 animationOffset = new Vector3(0,5,5); 

	// Use this for initialization
	void Start () {
		lookAt = GameObject.FindGameObjectWithTag ("Player").transform; //finds the player game object
		startOffset = transform.position - lookAt.position;
	}
	
	// Update is called once per frame
	void Update () {
		moveVector = lookAt.position + startOffset; 

		moveVector.x = 0;
		moveVector.y = Mathf.Clamp (moveVector.y, 1, 4); //gives limited offset for y vector incase of stairs

		//transition is at 0 at beginning of game, so will enter else statement
		if (transition > 1.5f) {
			transform.position = moveVector; //position of camera is now == to player position
		} else {
			transform.position = Vector3.Lerp (moveVector + animationOffset, moveVector, transition);
			transition += Time.deltaTime * 1 / animationDuration;
			//transform.LookAt (lookAt.position + Vector3.up); //rotates camera to look at character as it moves
		}


	}
}
