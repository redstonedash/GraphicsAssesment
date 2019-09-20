using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class maitainOffset : MonoBehaviour {
    [SerializeField] Transform[] offsets;
	void Update () {
        Vector3 vectorSum = Vector3.zero;
		foreach(Transform t in offsets)
        {
            vectorSum+=t.transform.position;
        }
        transform.position = vectorSum/offsets.Length;
	}
}
