using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerBehaviour : MonoBehaviour {
	Rigidbody2D rbtd;
	Transform AttackCollider;
	BoxCollider2D playerCollider;
	[Range(0.5f,5)]
	public float attackRange;
	[Range(5,30)]
	public float walkSpeed;
	[Range(0.02f, 0.3f)]
	public float attackSpeed;
	[Range(0,100)]
	public int playerLife;
	bool Rolling, Jumping, Crouch;
	enum Direction {LEFT, RIGHT};
	Direction playerDir;

	float time, attacktime;

    Animator anim;
    

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
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

		if (rbtd.velocity.magnitude == Vector2.zero.magnitude) { 
			Rolling = false;
            
        }
		if (rbtd.velocity.y == 0) { 
			Jumping = false;
            anim.speed = 1;
        }
        if (Input.GetKeyUp (KeyCode.S) )
            anim.speed = 1;
		if (Input.GetKey (KeyCode.S) ) { //çömelme
			AdjustColliders ();
            anim.SetTrigger("Crouch");
            anim.speed = 0;
            Crouch = true;
			if (Input.GetKeyDown (KeyCode.A) && !Rolling) { // dönüyorum
				Rolling = true;
				rbtd.AddForce (Vector2.left * 4000);
                if (playerDir == Direction.RIGHT) {
                    var scale = gameObject.transform.localScale;
                    scale.x *= -1;
                    gameObject.transform.localScale = scale;
                }
				playerDir = Direction.LEFT;
                /*Vector2  roll =  gameObject.transform;
                roll.z++;*/
                /*gameObject.transform*/
			} else if (Input.GetKeyDown (KeyCode.D) && !Rolling) {
				Rolling = true;
				rbtd.AddForce (Vector2.right * 4000);
                if (playerDir == Direction.LEFT) {
                    var scale = gameObject.transform.localScale;
                    scale.x *= -1;
                    gameObject.transform.localScale = scale;
                }
				playerDir = Direction.RIGHT;
			} else if(Input.GetKeyUp (KeyCode.S) && !Crouch) {
                Crouch = false;
            }
            
			//RollAnimation
		} else {
			ReAdjustColliders ();
			if (Input.GetKeyDown (KeyCode.W) && !Jumping) {
				Jumping = true;
				rbtd.AddForce (Vector2.up * 4000);
                anim.SetTrigger("Jump");
                anim.speed = 0;
				//JumpAnimation
			}
			if (Input.GetKey (KeyCode.A) && rbtd.velocity.magnitude <= (walkSpeed * Vector2.left).magnitude) {
				rbtd.AddForce (Vector2.left * 500);
                if (playerDir == Direction.RIGHT) {
                    var scale = gameObject.transform.localScale;
                    scale.x *= -1;
                    gameObject.transform.localScale = scale;
                }
                anim.SetTrigger("playerWalk");
				playerDir = Direction.LEFT; //sola yürüme
                
			} //flip sprite
			else if (Input.GetKey (KeyCode.D) && rbtd.velocity.magnitude <= (walkSpeed * Vector2.right).magnitude){
				rbtd.AddForce (Vector2.right * 500);
                if (playerDir == Direction.LEFT) {
                    var scale = gameObject.transform.localScale;
                    scale.x *= -1;
                    gameObject.transform.localScale = scale;
                }
                anim.SetTrigger("playerWalk");
                playerDir = Direction.RIGHT; //sağa yürüme
                
                
			} //flip sprite
		}
	}

	void AttackControls(){
		if (Input.GetKeyDown (KeyCode.Space)) {
			attacktime = time;
            if(Crouch == false) { 
                anim.SetTrigger("playerPunch");
                anim.speed = 0;
            }
            else{
                anim.SetTrigger("crouchPunch");
                anim.speed = 1;
                Crouch = false;
            }
			if (playerDir == Direction.LEFT)
				AttackCollider.transform.Translate (Vector2.left/2 * attackRange);
			else
				AttackCollider.transform.Translate (Vector2.right/2 * attackRange);
		} else if (true) {
			AttackCollider.transform.position = gameObject.transform.position;
			time = 0;
		}else if (Input.GetKeyUp (KeyCode.Space)) {
             anim.speed = 1;
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

	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.tag == "Keyboard") {
			if (other.gameObject.transform.position.x < transform.position.x)
				rbtd.AddForce (Vector2.right * 2000);
			else
				rbtd.AddForce (Vector2.left * 2000);
			playerLife -= 1;
		}

		
	}

	void OnCollisionEnter2D(Collision2D other){
		if (other.gameObject.tag == "Coffee")
			playerLife -= 1;
	}

}
