using UnityEngine;
using System.Collections;

public class DeathCoffeeFlight : MonoBehaviour {
	GameObject player;
	Rigidbody2D rbtd;
	[Range(50, 300)]
	public int spitForce;
	// Use this for initialization
	void Start () {
		spitForce = Random.Range (50, 300);
		rbtd = GetComponent<Rigidbody2D> ();
		player = GameObject.FindGameObjectWithTag ("Player");
		if (player.transform.position.x > gameObject.transform.position.x) //Player is to the RIGHT of toaster
			rbtd.AddForce (new Vector2 (spitForce, spitForce));
		else
			rbtd.AddForce (new Vector2 (-spitForce, spitForce));
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.localScale.x < 0.4f)
			transform.localScale += new Vector3 (0.005f, 0.005f, 0);
		else
			Destroy (gameObject);
	}

}
