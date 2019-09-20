using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class recursiveDrawForward : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        
        foreach (var t in transform.GetComponentsInChildren<Transform>())
        {
            
            Debug.DrawRay(t.position,t.forward, Color.red);
            Debug.DrawRay(t.position, t.right, Color.green);
            Debug.DrawRay(t.position, t.up, Color.blue);
        }
	}
}
