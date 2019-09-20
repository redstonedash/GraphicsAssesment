using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour {	
	// Update is called once per frame
    float life = 0;
    [SerializeField] AnimationCurve curve;
	void Update () {
		life += Time.deltaTime;
        GetComponent<Rigidbody>().AddForce(transform.forward*curve.Evaluate(life));
	}
}
