﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OvireColision : MonoBehaviour {

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "bullet")
        {
            Destroy(coll.gameObject);
            Destroy(gameObject);
        }
           
    }
}