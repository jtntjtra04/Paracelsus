using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeAI2wp : MonoBehaviour
{
    public float moveSpeed;
    public GameObject[] wayPoints;

    [SerializeField] private float rotationX;
    [SerializeField] private float rotationY;
    [SerializeField] private float rotationZ;

    int nextWaypoint = 1;
    float distToPoint;

    void Update()
    {
        Move();
    }

    void Move()
    {
        distToPoint = Vector2.Distance(transform.position, wayPoints[nextWaypoint].transform.position);
        transform.position = Vector2.MoveTowards(transform.position, wayPoints[nextWaypoint].transform.position, moveSpeed * Time.deltaTime);

        if (distToPoint < 0.001f)
        {
            TakeTurn();
        }
    }

    void TakeTurn()
    {
        Vector3 currRot = transform.eulerAngles;
        currRot.x += rotationX;
        currRot.y += rotationY;
        currRot.z += rotationZ;
        transform.eulerAngles = currRot;
        ChooseNextWaypoint();
    }

    void ChooseNextWaypoint()
    {
        nextWaypoint++;

        if (nextWaypoint == wayPoints.Length)
        {
            nextWaypoint = 0;
        }
    }
}
