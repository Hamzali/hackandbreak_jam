using UnityEngine;
using System.Collections;

public class FanHead : MonoBehaviour {
	GameObject Player;
	[Range(200, 300)]
	public int WindPower;
	// Use this for initialization
	void Start () {
		Player = GameObject.Find ("MainPlayer");
	}
	
	// Update is called once per frame
	void Update () {
		
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
