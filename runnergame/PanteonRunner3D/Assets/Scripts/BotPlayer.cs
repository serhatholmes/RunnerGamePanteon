using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotPlayer : MonoBehaviour
{
    public float speedForward = (Vector3.forward.z)/15;
    public float rayDist;
    Animator anim;
    Rigidbody rb;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //anim = GetComponent<Animator>();

        //anim.SetBool("Idle",true);

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Translate(0, 0, speedForward);
        //anim.SetBool("Running",true);
        RaycastHit rc;
        
        if(Physics.Raycast(transform.position, Vector3.forward, out rc, rayDist)){
            Debug.Log("found object - distance" + rc.distance);
            if(rc.collider.tag=="Obstacle"){
                Debug.Log("gördüm");
                transform.Translate(-3,0,speedForward);
            }   
        }
        var offset = new Vector3(0, 1.5f, 0);
        Debug.DrawLine(transform.position + offset, transform.position + offset + Vector3.forward*rayDist, Color.red);

        
    }

    
}
