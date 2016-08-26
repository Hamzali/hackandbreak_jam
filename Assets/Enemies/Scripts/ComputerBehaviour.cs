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
	[Range(0, 100)]
	public int maxLife = 50;
	int randomAttack;
	enum Direction {LEFT, RIGHT};
	Direction playerDir;
	Direction mouseDir, computerDir;
    public float smackForce;
    Animator anim;

    // Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        attackSpeed = 0.2f;
		ActualPlayer = GameObject.Find ("MainPlayer");
		Mouse = transform.FindChild ("Mouse").gameObject;
        computerDir = Direction.RIGHT;
        Keyboard = transform.FindChild ("Keyboard").gameObject;
		Mouse.GetComponent<BoxCollider2D> ().enabled = false;
		rbtd = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (maxLife <= 0)
			Destroy (gameObject);

		time += Time.deltaTime;

		WhereIsHe ();
        if(computerDir != playerDir) {
                Vector3 scale = gameObject.transform.localScale;
                scale.x *= -1;
                gameObject.transform.localScale = scale;
                if(computerDir == Direction.LEFT) {
                    computerDir = Direction.RIGHT;
                } else {
                    computerDir = Direction.LEFT;
                }
                
            }
		if (randomAttack == 0) {
			if (time >= 2) {
				if (grabbedPlayer == null && !missed) {
				    anim.SetTrigger("computerAttack");
					ThrowMouse ();
				}
				
				if (grabbedPlayer == null && missed)
					PullBack ();
			}

			if (grabbedPlayer != null)
				PullPlayer ();

			
		} else if (randomAttack >= 1) {
			if (ActualPlayer.transform.position.x > gameObject.transform.position.x) //Player is to the RIGHT of Computer
				rbtd.AddForce (new Vector2 (5000, 5000));
			else
				rbtd.AddForce (new Vector2 (-5000, 5000));
			randomAttack = Random.Range (0, 5);
		}
	}

	void WhereIsHe(){
		if (ActualPlayer.transform.position.x > gameObject.transform.position.x) { //Player is to the RIGHT of Computer
            playerDir = Direction.RIGHT;
        } else {
			playerDir = Direction.LEFT;
        }
	}

	#region MousePhase
	void ThrowMouse(){
		if (Vector2.Distance (transform.position, Mouse.transform.position) < 4 && grabbedPlayer == null) {
			Mouse.GetComponent<BoxCollider2D> ().enabled = true;
			if (playerDir == Direction.RIGHT)
				Mouse.transform.Translate (Vector2.right/5);
			else if (playerDir == Direction.LEFT)
				Mouse.transform.Translate (Vector2.left/5);
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

	void OnTriggerEnter2D(Collider2D other){
		if(other.gameObject.tag == "Player")
			grabbedPlayer = other.gameObject;
		if (other.gameObject.tag == "Punch")
			maxLife -= 1;
	}
	void OnCollisionEnter2D(Collision2D other){
		if (other.gameObject.tag == "Player") {
			time = 0;
			attacktime = 0;
			hitPlayer = true;
		}
	}
    void OnCollisionStay2D(Collision2D other){
         
        if(other.gameObject.tag == "Player") { 
            anim.SetTrigger("computerSmack");
             if (other.gameObject.transform.position.x > transform.position.x)
		        ActualPlayer.GetComponent<Rigidbody2D>().AddForce (new Vector2 (2000, 0));
		    else
			    ActualPlayer.GetComponent<Rigidbody2D>().AddForce (new Vector2 (-2000, 0));
        }
     }

}
