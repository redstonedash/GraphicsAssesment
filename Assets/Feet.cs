using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Feet : MonoBehaviour {
    public Rigidbody rb;
    public Collider col;
    public bool grounded = false;
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>(); //TODO replace this with the actuall collider

    }

    // Update is called once per frame
    private void OnCollisionEnter(Collision collision)
    {
        OnCollisionStay(collision);
    }
    private void OnCollisionStay(Collision collision)
    {
        grounded = true;
    }
    private void OnCollisionExit(Collision collision)
    {
        grounded = false;
    }
    
}
