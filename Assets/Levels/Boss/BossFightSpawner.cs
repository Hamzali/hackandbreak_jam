using UnityEngine;
using System.Collections;

public class BossFightSpawner : MonoBehaviour {
	public GameObject Toaster;
	Terminal Terminal;
	float time;
	int mock;
	// Use this for initialization
	void Start () {
		Terminal = GameObject.Find ("boss").GetComponent<Terminal> ();
	}
	
	// Update is called once per frame
	void Update () {
		time += Time.deltaTime;
		mock = Random.Range (0, 5);
		if (time >= 3 && mock <= 1) {
			Instantiate (Toaster, new Vector2 (0, 5), Quaternion.identity);
			Terminal.changeText (1);
			time = 0;
		} else if (time >= 3 && mock > 1) {
			Terminal.changeText (mock);
			time = 0;
		}
	}
}
