using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ButtonScript : MonoBehaviour {
	GameObject Wall;
	public GameObject pc;

	void Start(){
		pc = GameObject.Find ("pc_0");
	}

	public void StartGame(){
		Wall = GameObject.Find ("TestGround (1)");
		Wall.SetActive (false);
		Wall = GameObject.Find ("TestGround (2)");
		Wall.SetActive (false);
		pc.gameObject.GetComponent<Rigidbody2D> ().AddForce (new Vector2(-17000, 17000));
	}

	public void QuitGame(){
		Application.Quit();
	}

	void OnTriggerEnter2D(Collider2D other){
		if(other.gameObject.tag == "Player")
			SceneManager.LoadScene (1);
	}
}
