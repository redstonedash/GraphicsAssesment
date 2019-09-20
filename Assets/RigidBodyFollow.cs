using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidBodyFollow : MonoBehaviour {
    [SerializeField] Transform target;
    Vector3 error;
    Rigidbody rb;
    [SerializeField] float P, I, D;
    [SerializeField] float leashDistance;
    [SerializeField] Rigidbody parent;
    [SerializeField] ParticleSystem kickDirt;
    bool onGround = false;
    Vector3 lastHit;
    public bool jumping;
	// Use this for initialization
	void Start () {
        transform.position = target.position;
		rb = GetComponent<Rigidbody>();
	}
    private void Update()
    {
        
        Vector3 PIDCorrectionGround;
        Vector3 PIDCorrectionAir;
        error += Time.deltaTime * new Vector3((transform.position.x - target.position.x),
            (transform.position.y - target.position.y),
            (transform.position.z - target.position.z)); //used for the I term
        //P Proportional, difference multiplied by a force
        PIDCorrectionGround = new Vector3(P * -(transform.position.x - target.position.x),
            P * -(transform.position.y - target.position.y),
            P * -(transform.position.z - target.position.z));
        PIDCorrectionAir = new Vector3(P * -(transform.position.x - target.position.x),
            P * -(transform.position.y - target.position.y),
            P * -(transform.position.z - target.position.z));

        //I Intergal, the sum error since this code began
        PIDCorrectionGround += error * I;
        PIDCorrectionAir += error * I;

        //D Derivitive, the rate of change, or velocity in this case
        if (parent != null) {
            PIDCorrectionGround += new Vector3(-parent.velocity.x * D, -parent.velocity.y * D, -parent.velocity.z * D);
            PIDCorrectionAir += new Vector3((rb.velocity.x - parent.velocity.x) * D, (rb.velocity.y - parent.velocity.y) * D, (rb.velocity.z - parent.velocity.z) * D);
        } else
        {
            PIDCorrectionAir += new Vector3((rb.velocity.x) * D, (rb.velocity.y) * D, (rb.velocity.z) * D);

        }
        //print(PIDCorrection);
        if (onGround && parent!=null) {
            parent.AddForce(-PIDCorrectionGround * Time.deltaTime,ForceMode.Impulse);
        }
        rb.velocity += PIDCorrectionAir * Time.deltaTime;

        if(leashDistance>0 && Vector3.Distance(transform.position, target.position) > leashDistance)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, Vector3.Distance(transform.position, target.position) - leashDistance);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(kickDirt!=null){
            float size = Mathf.Sqrt(rb.velocity.magnitude);
            //kickDirt.transform.localScale = new Vector3(size, size, size);
            var settings = kickDirt.main;
            //var colorOverLife = kickDirt.colorOverLifetime;
            //Gradient gradient = new Gradient();
            //gradient.SetKeys(new GradientColorKey[] { new GradientColorKey(new Color(1, 1, 1), 0) }, new GradientAlphaKey[] { new GradientAlphaKey(95 / 255.0f, 0), new GradientAlphaKey(255 / 255.0f, .109f), new GradientAlphaKey((255 - (size * 125)) / 255.0f, .297f), new GradientAlphaKey(0 / 255.0f, 1.0f) });
            //colorOverLife.color = gradient;
            settings.startSize = new ParticleSystem.MinMaxCurve(size);
            if (size > 1)
            {
                kickDirt.Emit(1);
            }
        }
        OnCollisionStay(collision);
    }
    private void OnCollisionStay(Collision collision)
    {
        if (kickDirt != null)
        {
            float size = Mathf.Sqrt(rb.velocity.magnitude);
            var colorOverLife = kickDirt.colorOverLifetime;
            //Gradient gradient = new Gradient();
            //gradient.SetKeys(new GradientColorKey[] { new GradientColorKey(new Color(1, 1, 1), 0) }, new GradientAlphaKey[] { new GradientAlphaKey(95 / 255.0f, 0), new GradientAlphaKey(255 / 255.0f, .109f), new GradientAlphaKey((255 - (size * 125)) / 255.0f, .297f), new GradientAlphaKey(0 / 255.0f, 1.0f) });
            //colorOverLife.color = gradient;
        
            if (size > 1)
            {
                kickDirt.Emit(1);
            }
        }
        lastHit = collision.contacts[0].normal;
        onGround = true;
    }
    private void OnCollisionExit(Collision collision)
    {
        if (kickDirt != null)
        {
            float size = Mathf.Sqrt(rb.velocity.magnitude);
            var settings = kickDirt.main;
            var emmision = kickDirt.emission;
            var colorOverLife = kickDirt.colorOverLifetime;
            //Gradient gradient = new Gradient();
            //gradient.SetKeys(new GradientColorKey[]{new GradientColorKey(new Color(1,1,1),0) },new GradientAlphaKey[] {new GradientAlphaKey(95/255.0f,0), new GradientAlphaKey(255 / 255.0f, .109f), new GradientAlphaKey((255-(size*125)) / 255.0f, .297f), new GradientAlphaKey(0 / 255.0f, 1.0f) });
            //colorOverLife.color=gradient;
            emmision.rateOverTime = 0;
            settings.startSize = new ParticleSystem.MinMaxCurve(size);
            //kickDirt.transform.localScale = new Vector3(size,size,size);
            if (size > 1)
            {
                kickDirt.Emit(1);
            }
        }
        onGround = false;
    }
}
