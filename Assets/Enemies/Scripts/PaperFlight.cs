using UnityEngine;
using System.Collections;

public class PaperFlight : MonoBehaviour {
	GameObject player;
	Rigidbody2D rbtd;
	[Range(300, 800)]
	public int spitForce;
	float time;
	// Use this for initialization
	void Start () {
		spitForce = Random.Range (300, 800);
		rbtd = GetComponent<Rigidbody2D> ();
		player = GameObject.Find ("MainPlayer");
		if (player.transform.position.x > gameObject.transform.position.x) //Player is to the RIGHT of toaster
			rbtd.AddForce (new Vector2 (spitForce, 100));
		else
			rbtd.AddForce (new Vector2 (-spitForce, 100));
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate (0, 0, 5);
		time += Time.deltaTime;
		if(time >= 2)
			Destroy (gameObject);
	}
}
