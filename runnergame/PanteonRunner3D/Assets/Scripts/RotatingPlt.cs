using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingPlt : MonoBehaviour
{
    private float rotZ;
    public float RotationSpeed=2;
    public bool ClockwiseRotation;
    private Rigidbody rb;
    public GameObject playerChar;
    Transform tempT;
    void Update()
    {
        if(ClockwiseRotation==false){
            rotZ += Time.deltaTime*RotationSpeed;
        }
        else{
            rotZ += -Time.deltaTime*RotationSpeed;
        }

        transform.rotation = Quaternion.Euler(0,0,rotZ);
    }

    /*private void OnCollisionEnter(Collision col) {
        if(col.gameObject.CompareTag("Player")){
            playerChar.transform.parent = this.transform;
        }
    }
    private void OnCollisionExit(Collision col) {
        if(col.gameObject.CompareTag("Player")){
            playerChar.transform.parent = tempT;
        }
    }*/
}
