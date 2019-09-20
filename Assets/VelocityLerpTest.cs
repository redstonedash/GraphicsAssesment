using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VelocityLerpTest : MonoBehaviour {
    Rigidbody rb;
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
        rb.velocity = Vector3.right*20;
	}
	
	// Update is called once per frame
	void Update () {
		rb.velocity = Vector3.MoveTowards(rb.velocity, Vector3.up*20, 25*Time.deltaTime);
	}
}
