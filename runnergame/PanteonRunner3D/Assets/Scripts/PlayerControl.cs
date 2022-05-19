using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    //Vector3 startPost = new Vector3(0,-2,-8);
    Rigidbody rb;
    Animator anim;
    bool isRunning = true;

    //public float speed = 4f;

    private SwerveInputSystem _swerveInputSystem;
    [SerializeField] private float swerveSpeed = 0.5f;
    [SerializeField] private float maxSwerveAmount = 1f;

    public float speedForward = (Vector3.forward.z)/15;
    
    private void Awake() {
         _swerveInputSystem = GetComponent<SwerveInputSystem>();
    }
    void Start()
    {
        //this.transform.position = startPost;
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    
    void Update()
    {
        if(isRunning){
            anim.SetBool("Running",true);
        }

        

    }

    private void FixedUpdate() 
    {
        /*Vector3 forwardMove = transform.forward * speed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + forwardMove);*/

        float swerveAmount = Time.fixedDeltaTime * swerveSpeed * _swerveInputSystem.MoveFactorX;
        swerveAmount = Mathf.Clamp(swerveAmount, -maxSwerveAmount, maxSwerveAmount);
        transform.Translate(swerveAmount, 0, speedForward);
    }

    private void LateUpdate()
    {
        transform.localEulerAngles = new Vector3(0, 0, 0);
    }

    private void OnCollisionStay(Collision col){
        
        if(col.gameObject.CompareTag("Stop")){
            //Debug.Log("collision"); 
            speedForward = 0;
            anim.SetBool("Idle",true);
            rb.gameObject.transform.position = new Vector3(0,0,0);
            rb.constraints = RigidbodyConstraints.FreezePosition;
        }
    }

    
}
