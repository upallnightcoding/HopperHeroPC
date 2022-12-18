using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private Camera gameCamera;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject projectile;
    [SerializeField] private float blast;
    [SerializeField] private float disLimit;

    private RaycastHit hit;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) 
        {
            Ray ray = gameCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, disLimit)) 
            {
                GameObject go = Instantiate(projectile, firePoint.position, firePoint.rotation);
                Vector3 direction = (hit.point - firePoint.position).normalized;
                go.GetComponent<Rigidbody>().velocity = direction * blast;
                go.GetComponent<Rigidbody>().useGravity = false;

                // GameObject go = Instantiate(projectile, firePoint.position, firePoint.rotation);
                // Vector3 direction = (hit.point - firePoint.position).normalized;
                // Rigidbody rb = go.GetComponent<Rigidbody>();
                // go.GetComponent<Rigidbody>().useGravity = true;
                // go.GetComponent<Rigidbody>().velocity = direction * blast;

                // GameObject go = Instantiate(projectile, firePoint.position, firePoint.rotation);
                // Vector3 direction = (hit.point - firePoint.position).normalized;
                // Vector3 cast = (direction + Vector3.up).normalized;
                // go.GetComponent<Rigidbody>().useGravity = true;
                // go.GetComponent<Rigidbody>().velocity = cast * blast;
            }
        }
    }
}
