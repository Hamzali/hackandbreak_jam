using UnityEngine;
using System.Collections;

public class CameraFollowPlayer : MonoBehaviour {
	public Transform MainPlayer;
	public Vector3 ScreenPos;
	Vector3 moveCamera;
	Camera thisCamera;
	// Use this for initialization
	void Start () {
		thisCamera = GetComponent<Camera> ();
		moveCamera = thisCamera.ScreenToWorldPoint (new Vector3(Screen.width, 0, 0));
		//moveCamera = 18;
	}
	
	// Update is called once per frame
	void Update () {
		ScreenPos = thisCamera.WorldToScreenPoint (MainPlayer.position);
		if (ScreenPos.x <= 0) {
			gameObject.transform.Translate (new Vector2 (-moveCamera.x, 0));
		} else if (ScreenPos.x >= Screen.width) {
			gameObject.transform.Translate (new Vector2 (moveCamera.x, 0));
		}
	}


}
