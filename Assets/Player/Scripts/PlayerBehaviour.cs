using UnityEngine;
using System.Collections;

public class PlayerBehaviour : MonoBehaviour {
	Rigidbody2D rbtd;
	Transform AttackCollider;
	BoxCollider2D playerCollider;
	[Range(0.5f,1)]
	public float attackRange;
	[Range(10,30)]
	public float walkSpeed;
	[Range(0.02f, 0.3f)]
	public float attackSpeed;
	bool Rolling, Jumping;
	enum Direction {LEFT, RIGHT};
	Direction playerDir;

	float time, attacktime;



	// Use this for initialization
	void Start () {
		playerDir = Direction.LEFT;
		rbtd = GetComponent<Rigidbody2D> ();
		AttackCollider = transform.FindChild ("Attack");
		playerCollider = GetComponent<BoxCollider2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		time += Time.deltaTime;
		WalkControls ();
		AttackControls ();
	}

	void WalkControls(){
		if (rbtd.velocity.magnitude == Vector2.zero.magnitude)
			Rolling = false;
		if (rbtd.velocity.y == 0)
			Jumping = false;

		if (Input.GetKey (KeyCode.S) ) {
			AdjustColliders ();
			if (Input.GetKeyDown (KeyCode.A) && !Rolling) {
				Rolling = true;
				rbtd.AddForce (Vector2.left * 4000);
				playerDir = Direction.LEFT;
			} else if (Input.GetKeyDown (KeyCode.D) && !Rolling) {
				Rolling = true;
				rbtd.AddForce (Vector2.right * 4000);
				playerDir = Direction.RIGHT;
			}
			//RollAnimation
		} else {
			ReAdjustColliders ();
			if (Input.GetKeyDown (KeyCode.W) && !Jumping) {
				Jumping = true;
				rbtd.AddForce (Vector2.up * 4000);
				//JumpAnimation
			}
			if (Input.GetKey (KeyCode.A) && rbtd.velocity.magnitude <= (walkSpeed * Vector2.left).magnitude) {
				rbtd.AddForce (Vector2.left * 500);
				playerDir = Direction.LEFT;
			} //flip sprite
			else if (Input.GetKey (KeyCode.D) && rbtd.velocity.magnitude <= (walkSpeed * Vector2.right).magnitude){
				rbtd.AddForce (Vector2.right * 500);
				playerDir = Direction.RIGHT;
			} //flip sprite
		}
	}

	void AttackControls(){
		if (Input.GetKeyDown (KeyCode.Space)) {
			attacktime = time;
			if (playerDir == Direction.LEFT)
				AttackCollider.transform.Translate (Vector2.left * attackRange);
			else
				AttackCollider.transform.Translate (Vector2.right * attackRange);
		} else if (time >= attacktime + attackSpeed) {
			AttackCollider.transform.position = gameObject.transform.position;
			time = 0;
		}
	}

	void AdjustColliders(){
		playerCollider.size = new Vector2 (0.14f, 0.14f);
		playerCollider.offset = new Vector2 (0, -0.14f);
		AttackCollider.GetComponent<BoxCollider2D> ().offset = new Vector2 (0, -0.14f);
	}

	void ReAdjustColliders(){
		playerCollider.size = new Vector2 (0.14f, 0.45f);
		playerCollider.offset = new Vector2 (0, 0);
		AttackCollider.GetComponent<BoxCollider2D> ().offset = new Vector2 (0, 0);
	}

	void OnTriggerStay2D(Collider2D other){
		if (other.gameObject.tag == "Wind") {
			if(other.transform.position.x < transform.position.x)
				rbtd.AddForce (Vector2.right * 200);
			else
				rbtd.AddForce (Vector2.left * 200);
		}
	}

}
