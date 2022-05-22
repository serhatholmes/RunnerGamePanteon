using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControl : MonoBehaviour
{
    //Vector3 startPost = new Vector3(0,-2,-8);
    Rigidbody rb;
    Animator anim;
    bool isRunning = true;
    bool isRotating = false;
    float rotationForce;

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
    private void FixedUpdate() 
    {
         if(isRunning){
            anim.SetBool("Running",true);
            float swerveAmount = Time.fixedDeltaTime * swerveSpeed * _swerveInputSystem.MoveFactorX;
            swerveAmount = Mathf.Clamp(swerveAmount, -maxSwerveAmount, maxSwerveAmount);
            transform.Translate(swerveAmount, 0, speedForward);
        }

        if (isRotating){
            rb.velocity = new Vector3(rotationForce, rb.velocity.y, rb.velocity.z);
        }
        /*Vector3 forwardMove = transform.forward * speed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + forwardMove);*/
    }
    private void LateUpdate()
    {
        transform.localEulerAngles = new Vector3(0, 0, 0);
    }
    private void OnTriggerEnter(Collider other) {
        var tag = other.tag;
        if (tag == "Rotating"){
            var objScript = other.gameObject.GetComponent<RotatingPlt>();
            isRotating = true;
            var rotForce = objScript.RotationSpeed / 30.0f;
            rotationForce = objScript.ClockwiseRotation == true ? 1 * rotForce : -1 * rotForce;
        }

        if (tag == "Stop"){
            //SceneManager.LoadScene("PaintingScene");
            isRunning = false;
            anim.SetBool("Coloring",true);
            anim.SetBool("Running", false);
        }
    }

    private void OnTriggerExit(Collider other) {
        var tag = other.tag;
        if (tag == "Rotating"){
            isRotating = false;
            rotationForce = 0.0f;
        }
    }
    
}
