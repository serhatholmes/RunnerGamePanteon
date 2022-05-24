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
    public int layer = 3;
    public int lm;
    float rotationForce;
    
    Paintable paintable;
    public GameObject cameraMain;

    public GameObject paintButton;

    private SwerveInputSystem _swerveInputSystem;
    [SerializeField] private float swerveSpeed = 0.5f;
    [SerializeField] private float maxSwerveAmount = 1f;

    public float speedForward = (Vector3.forward.z)/15;
    
    private void Awake() {
        _swerveInputSystem = GetComponent<SwerveInputSystem>();
        paintButton.SetActive(false);
    }
    void Start()
    {
        lm = 1 << layer;
        //this.transform.position = startPost;
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        paintable = FindObjectOfType<Paintable>();

    }

    private void Update() {
        Debug.DrawLine(transform.position, transform.position + Vector3.forward * 15f, Color.green);
        RaycastHit rc;
        
        if(Physics.Raycast(transform.position + Vector3.up * 0.5f + Vector3.forward * 1.5f, Vector3.forward, out rc, 100f)){
            Debug.Log(rc.point);
        }
        
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
            var rotForce = objScript.RotationSpeed / 20.0f;
            rotationForce = objScript.ClockwiseRotation == true ? 1 * rotForce : -1 * rotForce;
        }

        if (tag == "Stop"){
            //SceneManager.LoadScene("PaintingScene");
            isRunning = false;
            anim.SetBool("Coloring",true);
            anim.SetBool("Running", false);
            paintable.isPainting = true;
            //cameraMain.transform.Rotate(-13f,0,0,Space.Self);
            //cameraMain.transform.Translate(0,0,1.0f,Space.Self);
            paintButton.SetActive(true);
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
