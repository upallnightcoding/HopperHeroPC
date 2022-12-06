using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scoreable : MonoBehaviour
{
    [SerializeField] private int hitPoints;

    public void OnTriggerEnter(Collider other) {
        Debug.Log("Trigger was tripped ...");
    }
}
