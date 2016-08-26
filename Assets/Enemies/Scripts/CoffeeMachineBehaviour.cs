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
	[Range(0,20)]
	public int coffeeLife;
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

        if(coffeeLife <= 0) {
            Destroy(gameObject);
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
	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.tag == "Punch")
			coffeeLife -= 1;
	}
}
