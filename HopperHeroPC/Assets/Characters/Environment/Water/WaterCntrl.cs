using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterCntrl : MonoBehaviour
{
    [SerializeField] private GameObject waterWavesPreFab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision other) 
    {
        Debug.Log("WaterCntrl ... OnCollisionEnter");
    }
}
