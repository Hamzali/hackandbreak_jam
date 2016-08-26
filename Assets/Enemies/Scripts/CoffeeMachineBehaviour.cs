using UnityEngine;
using System.Collections;

public class CoffeeMachineBehaviour : MonoBehaviour {
	public GameObject DeathCoffee;
	GameObject player;
	[Range(1, 3)]
	public float spitFreq;

    Animator anim;
    float time;
	[Range(3, 10)]
	public int maximumParticles;
	int i;
	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
		player = GameObject.Find ("MainPlayer");
	}
	
	// Update is called once per frame
	void Update () {
		time += Time.deltaTime;

		if (time >= spitFreq) { 
			SpitDeadlyCoffee ();
            anim.SetTrigger("coffeeAttack");
        }
	}

	void SpitDeadlyCoffee(){
		if (i < maximumParticles) {
			Instantiate (DeathCoffee, transform.position + Vector3.up, transform.rotation);
			i++;
		} else {
			i = 0;
			time = 0;
		}
	}
}
