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
	float time;
	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		rbtd = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		time += Time.deltaTime;
		if (player.transform.position.x > gameObject.transform.position.x) //Player is to the RIGHT of toaster
			toasterDir = Direction.RIGHT;
		else
			toasterDir = Direction.LEFT;

		if (time >= jumpFreq)
			ToasterLeap ();
	}

	void ToasterLeap(){
		if (toasterDir == Direction.LEFT) { //Leap towards LEFT
			rbtd.AddForce (new Vector2 (-jumpForce, jumpForce));
		} else
			rbtd.AddForce (new Vector2 (jumpForce, jumpForce));
		time = 0;
	}
}
