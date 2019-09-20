using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    [SerializeField] AnimationCurve strideZ, strideY;
    [SerializeField] Transform leftFoot, rightFoot;
    [SerializeField] RigidBodyFollow leftIKTarget, rightIKTarget;
    float strideCounter = 0;
    float LRstrideoffset = 0.5f;
    float lastVertical = 0;
    float lastVerticalRight = 0;
    public float stability = 0.3f;
    public float speed = 2.0f;
    Rigidbody rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    // Update is called once per frame
    //void FixedUpdate()
    //{
    //    Vector3 predictedUp = Quaternion.AngleAxis(
    //        rb.angularVelocity.magnitude * Mathf.Rad2Deg * stability / speed,
    //        rb.angularVelocity
    //    ) * transform.up;
    //    Vector3 torqueVector = Vector3.Cross(predictedUp, Vector3.up);
    //    rb.AddTorque(torqueVector * speed * speed);
    //}



    void Update () {
        //strideCounter += Time.deltaTime * Input.GetAxis("Vertical");
        //      if(strideY.Evaluate(strideCounter)>strideY.Evaluate(strideCounter-Time.deltaTime))
        //      {
        //          leftIKTarget.jumping = true;
        //      } else
        //      {
        //          leftIKTarget.jumping = false;
        //      }
        //      if (strideY.Evaluate(strideCounter+LRstrideoffset) > strideY.Evaluate(strideCounter - Time.deltaTime+LRstrideoffset))
        //      {
        //          rightIKTarget.jumping = true;
        //      }
        //      else
        //      {
        //          rightIKTarget.jumping = false;
        //      }

        //      leftFoot.localPosition = new Vector3(-2, strideY.Evaluate(strideCounter), strideZ.Evaluate(strideCounter));
        //      rightFoot.localPosition = new Vector3(2, strideY.Evaluate(strideCounter+LRstrideoffset), strideZ.Evaluate(strideCounter+LRstrideoffset));
        //if(Input.GetAxis("Vertical") > lastVertical)
        //{
        //    leftIKTarget.jumping = true;
        //} else { 
        //    leftIKTarget.jumping = false;
        //}
        leftFoot.localPosition = new Vector3(-2, Input.GetAxis("Vertical") * 3.5f, Input.GetAxis("Horizontal") * 3.5f);
        //leftFoot.localPosition = new Vector3(-2, Mathf.Sin(Time.time*8) * 5, Mathf.Cos(Time.time*8) * 5);
        //if (Input.GetAxis("VerticalRight") > lastVerticalRight)
        //{
        //    rightIKTarget.jumping = true;
        //}
        //else
        //{
        //    rightIKTarget.jumping = false;
        //}
        rightFoot.localPosition = new Vector3(2, Input.GetAxis("VerticalRight") * 3.5f, Input.GetAxis("HorizontalRight") * 3.5f);
        //rightFoot.localPosition = new Vector3(2, Mathf.Cos(Time.time*8) * 5, Mathf.Sin(Time.time*8) * 5);
    }
}
