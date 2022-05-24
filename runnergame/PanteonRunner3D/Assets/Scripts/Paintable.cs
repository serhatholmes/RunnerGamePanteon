using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Paintable : MonoBehaviour
{
  public GameObject Brush;
    public float BrushSize = 0.01f;
    public RenderTexture RTexture;
    public bool isPainting = false;
	void Update () 
    {
        if(isPainting){
            PaintFunc();
        }
	}
    private void PaintFunc2(){
        if (Input.GetMouseButton(0)){

        }
    }
    private void PaintFunc(){
        if (Input.GetMouseButton(0))
        {
            //cast a ray to the plane
            //transform.localRotation=Quaternion.Euler(-90,0,0);
            var Ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            Debug.Log("ray"+ Ray);
            if(Physics.Raycast(Ray, out hit))
            {
                //instanciate a brush
                var go = Instantiate(Brush, hit.point, Quaternion.identity, transform);
                go.transform.localScale = Vector3.one * BrushSize;
            }
        }
    }

    public void Save()
    {
        StartCoroutine(CoSave());
    }

    private IEnumerator CoSave()
    {
        //wait for rendering
        yield return new WaitForEndOfFrame();
        Debug.Log(Application.dataPath + "/savedImage.png");

        //set active texture
        RenderTexture.active = RTexture;

        //convert rendering texture to texture2D
        var texture2D = new Texture2D(RTexture.width, RTexture.height);
        texture2D.ReadPixels(new Rect(0, 0, RTexture.width, RTexture.height), 0, 0);
        texture2D.Apply();

        //write data to file
        var data = texture2D.EncodeToPNG();
        File.WriteAllBytes(Application.dataPath + "/savedImage.png", data);
    }
}
