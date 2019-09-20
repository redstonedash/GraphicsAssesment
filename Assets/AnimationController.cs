using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour {
    public Rigidbody rb;
    [SerializeField] Transform ikTarget;
    bool onGround = true;
    Vector3 error = Vector3.zero;
    [SerializeField] Feet[] feet;
    [SerializeField] AnimationCurve stepX, stepY, pushX, pushY;
    // Use this for initialization
    void Start () {
        
		rb = GetComponent<Rigidbody>();
        StartCoroutine(Step(feet[0]));
    }
	
	// Update is called once per frame
	void Update () {
        
        Vector3 vectorSum = Vector3.zero;
        int i = 0;
        foreach (Feet t in feet)
        {
            
            if(!t.grounded){
                
                continue;
            }
            i++;
            vectorSum += t.transform.position;
        }
        
        ikTarget.position = vectorSum / Mathf.Max(i,1)-new Vector3(0,.3f,0);
        Vector3 PIDCorrection;
        error += Time.deltaTime * new Vector3((transform.position.x - ikTarget.position.x),
            (transform.position.y - ikTarget.position.y), 
            (transform.position.z - ikTarget.position.z)); //used for the I term
        onGround = feet[0].grounded || feet[1].grounded;
        if (onGround) {
            //P Proportional, difference multiplied by a force
            PIDCorrection = new Vector3(60 * -(transform.position.x - ikTarget.position.x), 
                30*-(transform.position.y - ikTarget.position.y), 
                30 * -(transform.position.z - ikTarget.position.z));

            //I Intergal, the sum error since this code began
            PIDCorrection += error*-10;

            //D Derivitive, the rate of change, or velocity in this case
            PIDCorrection += new Vector3(rb.velocity.x * -1.9f, rb.velocity.y * -1.9f, rb.velocity.z * -1.9f);
            //print(PIDCorrection);
            rb.velocity += PIDCorrection*Time.deltaTime;
        }
        
	}
    IEnumerator Step(Feet foot)
    {
        float i = 0;
        float preX = stepX.Evaluate(0);
        float preY = stepY.Evaluate(0);
        while (i<Mathf.Max(stepX.length,stepY.length))
        {
            i+=Time.deltaTime;
            
            float X = stepX.Evaluate(i);
            float Y = stepY.Evaluate(i);
            foot.transform.localPosition += new Vector3(0, Y - preY, X - preX);//don't question it, Z=X now
            print(new Vector3(X - preX, 0, Y - preY));
            preX = X;
            preY = Y;
            if(i > 0.1f && foot.grounded)
            {
                break;
            }
            rb.AddForce(new Vector3(0, Y - preY, X - preX) * (1/Time.deltaTime),ForceMode.VelocityChange);
            yield return null;
        }
    }
}
