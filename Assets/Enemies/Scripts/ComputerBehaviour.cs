using UnityEngine;
using System.Collections;

public class ComputerBehaviour : MonoBehaviour {
	GameObject Mouse, Keyboard;
	Rigidbody2D rbtd;
	public GameObject grabbedPlayer;
	GameObject ActualPlayer;
	[Range(0.5f,2)]
	public float attackRange;
	bool missed, hitPlayer;
	float time, attacktime, attackSpeed;
	public int randomAttack;
	enum Direction {LEFT, RIGHT};
	Direction playerDir;
	Direction mouseDir;
	// Use this for initialization
	void Start () {
		attackSpeed = 0.2f;
		ActualPlayer = GameObject.Find ("MainPlayer");
		Mouse = transform.FindChild ("Mouse").gameObject;
		Keyboard = transform.FindChild ("Keyboard").gameObject;
		Mouse.GetComponent<BoxCollider2D> ().enabled = false;
		rbtd = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		time += Time.deltaTime;

		WhereIsHe ();
		if (randomAttack == 0) {
			if (time >= 2) {
				if (grabbedPlayer == null && !missed) {
				
					ThrowMouse ();
				}
				
				if (grabbedPlayer == null && missed)
					PullBack ();
			}

			if (grabbedPlayer != null)
				PullPlayer ();

			if (hitPlayer)
				KeyboardSmack ();
		} else if (randomAttack >= 1) {
			if (ActualPlayer.transform.position.x > gameObject.transform.position.x) //Player is to the RIGHT of toaster
				rbtd.AddForce (new Vector2 (5000, 5000));
			else
				rbtd.AddForce (new Vector2 (-5000, 5000));
			randomAttack = Random.Range (0, 5);
		}
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
			Mouse.GetComponent<BoxCollider2D> ().enabled = true;
			if (playerDir == Direction.RIGHT)
				Mouse.transform.Translate (Vector2.right/2);
			else if (playerDir == Direction.LEFT)
				Mouse.transform.Translate (Vector2.left/2);
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
		} else {
			missed = false;
			Mouse.GetComponent<BoxCollider2D> ().enabled = false;
			grabbedPlayer = null;
		}
			
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
			randomAttack = Random.Range (0, 5);
			missed = false;
			time = 0;
		}
	}
	#endregion

	void KeyboardSmack(){
		if (playerDir == Direction.LEFT) {
			Keyboard.transform.position = new Vector2(transform.position.x + Vector2.left.x * attackRange, transform.position.y);
		}
		else
			Keyboard.transform.position = new Vector2(transform.position.x + Vector2.right.x * attackRange, transform.position.y);
		if (time >= attacktime + attackSpeed) {	
			Keyboard.transform.position = gameObject.transform.position;
			randomAttack = Random.Range (0, 5);
			hitPlayer = false;
			time = 0;
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		if(other.gameObject.tag == "Player")
			grabbedPlayer = other.gameObject;
	}
	void OnCollisionStay2D(Collision2D other){
		if (other.gameObject.tag == "Player") {
			
			time = 0;
			attacktime = 0;
			hitPlayer = true;
		}
	}
}
