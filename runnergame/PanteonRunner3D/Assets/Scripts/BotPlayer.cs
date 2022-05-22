using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotPlayer : MonoBehaviour
{

    public float speedForward = (Vector3.forward.z)/15;
    public float rayDist;
    Animator anim;
    Rigidbody rb;

    private float nextPos;
    private bool moving;
    private Vector3 posBeforeSwerve;
    public List<GameObject> seenObstacles = new List<GameObject>();
    private List<float> randomPosChanges = new List<float> {3f, -3f};
    [SerializeField] Transform startPos;
    
    // Start is called before the first frame update
    void Start()
    {
        nextPos = 0.0f;
        moving = false;
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    
    void Update(){
        RaycastHit rc;
        if(Physics.Raycast(transform.position - new Vector3(0, 0.1f, 0), Vector3.forward, out rc, rayDist)){
            var obj = rc.collider.gameObject;
            if(rc.collider.tag=="Obstacle" && !seenObstacles.Contains(obj)){
                Debug.Log("gördüm " + obj.name);
                seenObstacles.Add(obj);
                nextPos = randomPosChanges[Random.Range(0, randomPosChanges.Count)];
                moving = true;
                posBeforeSwerve = transform.position;
            }   
        }
    }

    void FixedUpdate()
    {
        transform.Translate(0, 0, speedForward);
        anim.SetBool("Running",true);

        if (moving){
            var xPos = transform.position.x;
            var diff = Mathf.Lerp(xPos, posBeforeSwerve.x + nextPos, 0.05f);
            transform.position = new Vector3(diff, transform.position.y, transform.position.z);
            var checkDiff = Mathf.Abs(nextPos) / 10.0f;
            if (Mathf.Abs(xPos - posBeforeSwerve.x + nextPos) <= checkDiff){
                Debug.Log("moving false");
                nextPos = 0.0f;
                moving = false;
            }
        }

        var offset = new Vector3(0, 0.1f, 0);
        Debug.DrawLine(transform.position + offset, transform.position + offset + Vector3.forward*rayDist, Color.red);
    }

    private void OnCollisionEnter(Collision other){
        if (other.gameObject.CompareTag("Obstacle")){
            StartCoroutine(moveAway(transform.position.z));
        }

        if(other.gameObject.CompareTag("Restart")){
            seenObstacles.Clear();
            nextPos = 0.0f;
            this.transform.position = startPos.position;
            posBeforeSwerve = transform.position;
            moving = false;
            Debug.Log("degdi");
        }
    }

    IEnumerator moveAway(float zPos){
        yield return new WaitForSeconds(1);
        Debug.Log("ZPOS DIFF: " + (transform.position.z - zPos));
        if (transform.position.z - zPos <= 0.5f){
            nextPos = randomPosChanges[Random.Range(0, randomPosChanges.Count)];
            moving = true;
            posBeforeSwerve = transform.position;
            yield return new WaitForSeconds(1);
            moveAway(zPos);
        }
        else{
            StopCoroutine(moveAway(zPos));
        }
    }
}
