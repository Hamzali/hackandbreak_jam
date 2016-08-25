﻿using UnityEngine;
using System.Collections;

public class FanBehaviour : MonoBehaviour {
	public GameObject FanNeck, FanHead;

	int neckCount, i;
	// Use this for initialization
	void Start () {
		neckCount = Random.Range (1, 4);
		while (i <= neckCount) {
			Instantiate (FanNeck, transform.position + new Vector3 (0, 0.5f, 0) * i, transform.rotation);
			i++;
		}
		Instantiate (FanHead, transform.position + new Vector3 (0, 0.5f, 0) * i, transform.rotation);
	}
}
