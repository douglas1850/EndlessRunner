using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathMotor : MonoBehaviour {

	public Transform playerTransform;
	private Vector3 moveVector;
	private Vector3 startOffset;

	// Use this for initialization
	void Start () {
		playerTransform = GameObject.FindGameObjectWithTag ("Player").transform; //finds the player game object
		startOffset = transform.position - playerTransform.position;
	}

	// Update is called once per frame
	void Update () {

		moveVector = playerTransform.position + startOffset; 

		moveVector.x = 0;
		moveVector.y = -8;

		transform.position = moveVector;

	}
}
