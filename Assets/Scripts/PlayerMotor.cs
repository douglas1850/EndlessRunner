using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMotor : MonoBehaviour {

	public CharacterController controller;
	//private Score score;
	private Vector3 moveVector;
	private Vector3 pos;

	private float speed = 5.0f;
	private float verticalVelocity = 0.0f; //increment if falling, set to 0 if not
	private float gravity = 12.0f;
	private float animationDuration = 1.5f; //1.5 second delay
	private float startTime;

	private float ghostPowerUpTimer;
	private float generalPowerUpTimer;

	private bool isDead = false;
	private bool ghostPowerUp = false;

	public Text powerUpTimer;

	// Use this for initialization
	void Start () {
		controller = GetComponent<CharacterController> ();
		startTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {

		if (isDead)
			return; //don't need to move anymore if dead

		if (Time.time - startTime < animationDuration) {
			controller.Move (Vector3.forward * speed * Time.deltaTime);
			return; //causes us to exit update function for first 3 seconds
		}
			
		moveVector = Vector3.zero; //ever frame reset this
	
		if (Time.time - ghostPowerUpTimer >= 10.0f) {
			ghostPowerUp = false;
			NormalWalk ();
		}
		
		if (ghostPowerUp) {
			//pos.y = Mathf.Clamp (transform.position.y, 0, 0.5f);
			generalPowerUpTimer -= Time.deltaTime;
			powerUpTimer.text = ((int)generalPowerUpTimer).ToString();
			powerUpTimer.fontSize = 38;
			print (generalPowerUpTimer);
			GhostWalk ();
			
		}

		if (!ghostPowerUp) {
			verticalVelocity -= gravity * Time.deltaTime;
			if (gameObject.transform.position.y < -5) { //fall death
				Death ();
			}
		}

		//X is left/right, Y is up/down, Z is forward/backward
		moveVector.x = Input.GetAxisRaw("Horizontal") * speed; //Raw so sensitivity and gravity input don't affect it
		moveVector.z = speed; //only going forward and speed is positive
		moveVector.y = verticalVelocity;


		controller.Move (moveVector * Time.deltaTime); //accounts for fps of different computers using Time.deltaTime
	}

	public void SetSpeed(float modifier)
	{
		speed = 5.0f + modifier; //base speed + modifier
	}

	//called anytime our player collides with something
	private void OnControllerColliderHit(ControllerColliderHit hit)
	{
		if(!ghostPowerUp)
		{
			if (hit.point.z > transform.position.z + 0.1f && hit.gameObject.tag == "EnemyTag") {
				print ("Dead!");
				Death ();
			} 
		}
		if (hit.point.z > transform.position.z + 0.1f && hit.gameObject.tag == "GhostTag") {
			ghostPowerUp = true;
			ghostPowerUpTimer = Time.time;
			generalPowerUpTimer = 10.0f;
			Destroy (hit.gameObject);
		} else if (hit.point.z > transform.position.z + 0.1f && hit.gameObject.tag == "TimeTag") {
			Score score = GetComponent<Score>();
			score.LowerTime ();
			Destroy (hit.gameObject);
		}
	}

	private void Death()
	{
		if (!ghostPowerUp) {
			isDead = true;
			GetComponent<Score> ().OnDeath ();
		}
	}

	private void GhostWalk()
	{
		verticalVelocity = 0.0f;
		gameObject.layer = LayerMask.NameToLayer ("Not Solid");
	}

	private void NormalWalk()
	{
		gameObject.layer = LayerMask.NameToLayer ("Default");

	}
}
