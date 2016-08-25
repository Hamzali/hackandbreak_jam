using UnityEngine;
using System.Collections;

public class FanHead : MonoBehaviour {
	GameObject Player;
	[Range(200, 400)]
	public int WindPower;
	// Use this for initialization
	void Start () {
		Player = GameObject.FindGameObjectWithTag ("Player");
	}
	
	// Update is called once per frame
	void Update () {
		if (Player.transform.position.x > gameObject.transform.position.x) //Player is to the RIGHT of toaster
			transform.rotation = Quaternion.Euler (0, 0, 0);
		else
			transform.rotation = Quaternion.Euler (0, 180, 0);
	}

	void OnTriggerStay2D(Collider2D other){
		if (other.gameObject.tag == "Player") {
			if(other.transform.position.x > transform.position.x)
				other.GetComponent<Rigidbody2D>().AddForce (Vector2.right * WindPower);
			else
				other.GetComponent<Rigidbody2D>().AddForce (Vector2.left * WindPower);
		}
	}
}
