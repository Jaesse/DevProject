using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attractor : MonoBehaviour
{
    public Rigidbody rb;
    const float G = 667.4f;

 

    private void FixedUpdate()
    {
        Attractor[] attractors = FindObjectsOfType<Attractor>();
        foreach(Attractor attractor in attractors)
        {
            if(attractor != this)
            {
                Attract(attractor);
            }
        }


    }

    void Attract( Attractor objToAttract )
    {
        /*
        Rigidbody rbToAttract = objToAttract.rb;

        Vector3 direction = rb.position - rbToAttract.position;
        float distance = direction.magnitude;

        if (distance == 0)
            return;

        float forceMagnitude = G * (rb.mass * rbToAttract.mass) / Mathf.Pow(distance, 2);
        Vector3 force = direction.normalized * forceMagnitude;

        // velocity
       
        //
         rbToAttract.AddForce(force);
        */

        Rigidbody rbToAttract = objToAttract.rb;

        Vector3 difference = rb.position - rbToAttract.position;
        float dist = difference.magnitude;
        Vector3 gravityDirection = difference.normalized;
        float gravity = 6.7f * (rb.mass * rbToAttract.mass * 8) / (dist * dist);
        Vector3 gravityVector = (gravityDirection * gravity);


        //Add sideways velocity
        if (rbToAttract.tag == "Sun") { }
        else
        {
            rbToAttract.AddForce((rbToAttract.transform.forward) * 5, ForceMode.Acceleration);

            //
            rbToAttract.AddForce(gravityVector, ForceMode.Acceleration);
        }
    }

}
