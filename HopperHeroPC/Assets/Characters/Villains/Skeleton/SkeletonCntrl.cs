using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonCntrl : MonoBehaviour
{
    [SerializeField] private float skeletonSpeed;
    [SerializeField] private int nWayPoints;

    private CharacterController controller;
    private Animator animator;

    private Vector3[] wayPointsWC = null;
    private int traversePoint = 0;
    private Vector3 position;

    private SkeletonMoveType skeletonMove = SkeletonMoveType.TURN;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();

        InitializePathPoints();
    }

    void FixedUpdate() 
    {
        MoveSkeleton();

        switch(skeletonMove) 
        {
            case SkeletonMoveType.IDLE:
                break;
            case SkeletonMoveType.MOVE:
                skeletonMove = MoveSkeleton();
                break;
            case SkeletonMoveType.TURN:
                skeletonMove = TurnSkeleton();
                break;
        }
    }

    // Update is called once per frame
    private SkeletonMoveType MoveSkeleton()
    {
        SkeletonMoveType nextMove = SkeletonMoveType.MOVE;

        float distance = Vector3.Distance(gameObject.transform.position, wayPointsWC[traversePoint]);

        if (distance < 0.3) {
            traversePoint = (traversePoint + 1) % nWayPoints;
            nextMove = SkeletonMoveType.TURN;
        } else {
            Vector3 direction = (wayPointsWC[traversePoint] - gameObject.transform.position).normalized;

            controller.Move(direction * skeletonSpeed * Time.fixedDeltaTime);
            nextMove = SkeletonMoveType.MOVE;
        }

        return(nextMove);
    }

    private SkeletonMoveType TurnSkeleton() 
    {
        position.x = gameObject.transform.position.x;
        position.y = 0.0f;
        position.z = gameObject.transform.position.z;

        Vector3 direction = wayPointsWC[traversePoint] - position;

        Quaternion rotation = Quaternion.LookRotation(direction);
        Quaternion current = gameObject.transform.localRotation;

        transform.localRotation = Quaternion.Slerp(current, rotation, Time.fixedDeltaTime);

        return(Vector3.Angle(gameObject.transform.forward, direction) < 0.3 ? SkeletonMoveType.MOVE : SkeletonMoveType.TURN);
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

    private void OnCollisionEnter(Collision other) 
    {
        Debug.Log("OnCollisionEnter");
    }

    private void OnTriggerEnter(Collider other) 
    {
        Debug.Log("OnTriggerEnter");
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

public enum SkeletonMoveType 
{
    IDLE = 0,
    MOVE = 1,
    TURN = 2
}
