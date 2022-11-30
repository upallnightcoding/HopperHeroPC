using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCntrl : MonoBehaviour
{
    [SerializeField] Transform hero;

    private Vector3 delta;

    // Start is called before the first frame update
    void Start()
    {
        delta = hero.position - transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = hero.position - delta;
    }
}
