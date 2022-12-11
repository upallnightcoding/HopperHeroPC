using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonCntrl : MonoBehaviour
{
    private CharacterController controller;

    private int nWayPoints = 7;
    private Vector3[] wayPointsWC = null;
    private float skeletonSpeed = 1.0f;
    private int traversePoint = 0;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();

        InitializePathPoints();
    }

    void Update() 
    {
        MoveSkeleton();
    }

    // Update is called once per frame
    void MoveSkeleton()
    {
        float distance = Vector3.Distance(gameObject.transform.position, wayPointsWC[traversePoint]);

        traversePoint = (distance < 0.1) ? (traversePoint+1) % nWayPoints : traversePoint;

        Vector3 direction = (wayPointsWC[traversePoint] - gameObject.transform.position).normalized;

        controller.Move(direction * skeletonSpeed * Time.deltaTime);
    }

    private void InitializePathPoints() 
    {
        wayPointsWC = new Vector3[nWayPoints];

        for (int wayPoint = 0; wayPoint < nWayPoints; wayPoint++) {
            Vector2 p = Random.insideUnitCircle * 5;
            wayPointsWC[wayPoint] = transform.position + new Vector3(p.x, 0.0f, p.y);
        }

        gameObject.transform.position = wayPointsWC[nWayPoints - 1];
    }

    private void OnDrawGizmos() 
    {
        Gizmos.color = Color.red;
        for (int pathPoint = 0; pathPoint < nWayPoints; pathPoint++) 
        {
            Gizmos.DrawSphere(wayPointsWC[pathPoint], 0.1f);
        }
    }
}
