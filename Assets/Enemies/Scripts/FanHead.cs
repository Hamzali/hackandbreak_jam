using UnityEngine;
using System.Collections;

public class FanHead : MonoBehaviour {
	GameObject Player;
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
}
