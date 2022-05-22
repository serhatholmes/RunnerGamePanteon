using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagr : MonoBehaviour
{
    public GameObject beforeStart;
    
    private void Start() {
        beforeStart.SetActive(true);
        Time.timeScale=0.0f;
    }
    public void playGame(){

        beforeStart.SetActive(false);
        Time.timeScale=1.0f;
   }
}
