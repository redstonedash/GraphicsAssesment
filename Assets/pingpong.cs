using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pingpong : MonoBehaviour {
    [SerializeField] AnimationCurve ac;
	// Update is called once per frame
	void Update () {
		transform.position=new Vector3(ac.Evaluate(Time.time),0,0);
	}
}
