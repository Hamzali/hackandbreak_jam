using UnityEngine;
using System.Collections;

public class Printer : MonoBehaviour {
	int i;
	[Range(1,20)]
	public int maximumParticles;
	public GameObject SharpPaper;
	float time, throwFreq;
	// Use this for initialization
	void Start () {
		throwFreq = 0.3f;
	}
	
	// Update is called once per frame
	void Update () {
		time += Time.deltaTime;
		if (time >= throwFreq)
			ThrowSharpPapers ();
	}

	void ThrowSharpPapers(){
		if (i < maximumParticles) {
			Instantiate (SharpPaper, transform.position + Vector3.up/3, transform.rotation);
			i++;
		} else {
			i = 0;
			time = 0;
		}
	}
}
