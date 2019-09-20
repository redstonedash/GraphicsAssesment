using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightFlicker : MonoBehaviour {
    Light l;
    float seed;
	// Use this for initialization
	void Start () {
        seed = Random.value*100;
		l =  GetComponent<Light>();
	}
	
	// Update is called once per frame
	void Update () {
		transform.localPosition = new Vector3(Mathf.Pow(Mathf.PerlinNoise(Time.time,seed)-0.5f,2), Mathf.Pow(Mathf.PerlinNoise(Time.time+seed, Time.time+seed)-0.5f,2), Mathf.Pow(Mathf.PerlinNoise(seed, Time.time)-0.5f,2));
	}
}
