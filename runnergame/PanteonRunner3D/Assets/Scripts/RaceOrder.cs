using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class RaceOrder : MonoBehaviour
{

    public Text placement;

    static int SortByZPosition(Transform r1, Transform r2){
        return r1.position.z.CompareTo(r2.position.z);
    }
    public Transform racers;
    public List<Transform> racerPositions;
    void Start()
    {
        foreach (Transform child in racers){
            racerPositions.Add(child);
        }
    }

    // Update is called once per frame
    void Update()
    {
        racerPositions.Sort(SortByZPosition);
        racerPositions.Reverse();
        int placementIndex = racerPositions.FindIndex(obj => obj.name == "Player");
        placement.text = "Sıra: " + (placementIndex + 1).ToString();
    }
}
