using UnityEngine;
using System.Collections;

public class ToastMachineBehaviour : MonoBehaviour {
	GameObject player;
	enum Direction {LEFT, RIGHT};
	Direction toasterDir;
	Rigidbody2D rbtd;
	[Range(500, 750)]
	public float jumpForce;
	[Range(0.5f, 2)]
	public float jumpFreq;
	[Range(0,20)]
	public int toastLife;
	float time;

    Animator anim;
    // Use this for initialization
	void Start () {
		player = GameObject.Find ("MainPlayer");
		rbtd = GetComponent<Rigidbody2D> ();
        toasterDir = Direction.RIGHT;
	}
	
	// Update is called once per frame
	void Update () {
		time += Time.deltaTime;
        
        if (player.transform.position.x > gameObject.transform.position.x) { //Player is to the RIGHT of toaster
            if(toasterDir == Direction.LEFT) {
                Vector3 scale = gameObject.transform.localScale;
                scale.x *= -1;
                gameObject.transform.localScale = scale;
            }
            toasterDir = Direction.RIGHT;
        }
		else {
            if(toasterDir == Direction.RIGHT) {
                Vector3 scale = gameObject.transform.localScale;
                scale.x *= -1;
                gameObject.transform.localScale = scale;
            }
            toasterDir = Direction.LEFT;

        }
        
		if (time >= jumpFreq)
			ToasterLeap ();

		if (toastLife <= 0)
			Destroy (gameObject);
	}

	void ToasterLeap(){
		if (toasterDir == Direction.LEFT) { //Leap towards LEFT
			rbtd.AddForce (new Vector2 (-jumpForce, jumpForce));
		} else
			rbtd.AddForce (new Vector2 (jumpForce, jumpForce));
		time = 0;
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.tag == "Punch")
			toastLife -= 1;
	}
}
