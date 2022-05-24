using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagr : MonoBehaviour
{
    public GameObject beforeStart;
    public GameObject endPanel; 
    private void Start() {
        beforeStart.SetActive(true);
        Time.timeScale=0.0f;
    }
    public void playGame(){

        beforeStart.SetActive(false);
        Time.timeScale=1.0f;
   }
   public void restartGame()
   {
       SceneManager.LoadScene("GameScene");

   }
   public void skipPaintScene()
   {
       SceneManager.LoadScene("Paint");
   }
}
