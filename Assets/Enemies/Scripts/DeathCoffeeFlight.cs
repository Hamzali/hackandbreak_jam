using UnityEngine;
using System.Collections;

public class DeathCoffeeFlight : MonoBehaviour {
	GameObject player;
	Rigidbody2D rbtd;
	[Range(50, 300)]
	public int spitForce;
	float time;
	// Use this for initialization
	void Start () {
		spitForce = Random.Range (50, 300);
		rbtd = GetComponent<Rigidbody2D> ();
		player = GameObject.Find ("MainPlayer");
		if (player.transform.position.x > gameObject.transform.position.x) //Player is to the RIGHT of toaster
			rbtd.AddForce (new Vector2 (spitForce, spitForce));
		else
			rbtd.AddForce (new Vector2 (-spitForce, spitForce));
	}
	
	// Update is called once per frame
	void Update () {
		time += Time.deltaTime;
		if (time >= 1)
			Destroy (gameObject);
	}

}
