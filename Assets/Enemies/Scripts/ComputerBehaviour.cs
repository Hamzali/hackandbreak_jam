using UnityEngine;
using System.Collections;

public class ComputerBehaviour : MonoBehaviour {
	GameObject Mouse, Keyboard;
	public GameObject grabbedPlayer;
	GameObject ActualPlayer;
	[Range(0.5f,2)]
	public float attackRange;
	bool missed;
	float time, attacktime, attackSpeed;
	int randomAttack;
	enum Direction {LEFT, RIGHT};
	Direction playerDir;
	Direction mouseDir;
	// Use this for initialization
	void Start () {
		attackSpeed = 0.2f;
		ActualPlayer = GameObject.FindGameObjectWithTag ("Player");
		Mouse = transform.FindChild ("Mouse").gameObject;
		Keyboard = transform.FindChild ("Keyboard").gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		time += Time.deltaTime;

		WhereIsHe ();

		if (time >= 2) {
			if (grabbedPlayer == null && !missed)
				ThrowMouse ();
			if (grabbedPlayer == null && missed)
				PullBack ();
		}
		if (grabbedPlayer != null)
			PullPlayer ();
	}

	void WhereIsHe(){
		if (ActualPlayer.transform.position.x > gameObject.transform.position.x) //Player is to the RIGHT of toaster
			playerDir = Direction.RIGHT;
		else
			playerDir = Direction.LEFT;
	}

	#region MousePhase
	void ThrowMouse(){
		if (Vector2.Distance (transform.position, Mouse.transform.position) < 10 && grabbedPlayer == null) {
			if (playerDir == Direction.RIGHT)
				Mouse.transform.Translate (Vector2.right);
			else if (playerDir == Direction.LEFT)
				Mouse.transform.Translate (Vector2.left);
		}
		else
			missed = true;
	}

	void PullPlayer(){
		if (Vector2.Distance (transform.position, Mouse.transform.position) > 1) {
			if (playerDir == Direction.RIGHT) {
				Mouse.transform.Translate (Vector2.left);
				grabbedPlayer.transform.Translate (Vector2.left);
			} else if (playerDir == Direction.LEFT) {
				Mouse.transform.Translate (Vector2.right);
				grabbedPlayer.transform.Translate (Vector2.right);
			}
		} else
			missed = false;
	}

	void PullBack(){
		if (Mouse.transform.position.x > gameObject.transform.position.x) //Player is to the RIGHT of toaster
			mouseDir = Direction.RIGHT;
		else
			mouseDir = Direction.LEFT;
		
		if (Vector2.Distance (transform.position, Mouse.transform.position) > 1) {
			if(mouseDir == Direction.RIGHT)
				Mouse.transform.Translate (Vector2.left);
			else
				Mouse.transform.Translate (Vector2.right);
		} else {
			missed = false;
			time = 0;
		}
	}
	#endregion

	void KeyboardSmack(){
		attacktime = time;
		if (playerDir == Direction.LEFT)
			Keyboard.transform.Translate (Vector2.left * attackRange);
		else
			Keyboard.transform.Translate (Vector2.right * attackRange);
		if (time >= attacktime + attackSpeed) {
			Keyboard.transform.position = gameObject.transform.position;
			time = 0;
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		if(other.gameObject.tag == "Player")
			grabbedPlayer = other.gameObject;
	}
	void OnCollisionEnter2D(Collision2D other){
		if (other.gameObject.tag == "Player") {
			grabbedPlayer = null;
			time = 0;
		}
	}
}
