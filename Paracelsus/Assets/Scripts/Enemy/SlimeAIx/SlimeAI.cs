using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeAI : MonoBehaviour
{
    public float moveSpeed;
    public GameObject[] wayPoints;

    int nextWaypoint = 1;
    float distToPoint;

    void Update()
    {
        Move();
    }

    void Move()
    {
        distToPoint = Vector2.Distance(transform.position, wayPoints[nextWaypoint].transform.position);
        transform.position = Vector2.MoveTowards(transform.position, wayPoints[nextWaypoint].transform.position, moveSpeed*Time.deltaTime);

        if(distToPoint < 0.001f) //pivot '....' ke waypoint #makin kecil = 'flat'; makin besar = 'langsung'
        {
            TakeTurn();
        }

    }

    void TakeTurn()
    {
        Vector3 currRot = transform.eulerAngles;
        currRot.z += wayPoints[nextWaypoint].transform.eulerAngles.z;
        transform.eulerAngles = currRot;
        Vector3 scale = transform.localScale;

        // Flip the X scale
        scale.x *= 1;

        // Apply the new scale
        transform.localScale = scale;
        ChooseNextWaypoint();
    }

    void ChooseNextWaypoint()
        {
            nextWaypoint++;

            if(nextWaypoint == wayPoints.Length)
            {
                nextWaypoint = 0;
            }
        }
}
