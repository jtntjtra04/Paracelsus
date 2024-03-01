using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeAIWS4 : MonoBehaviour
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
        Vector3 scaleX = transform.localScale; // Change the variable name to scaleX

        // Flip the X scale
        scaleX.x *= 1;
        // Apply the new scale
        transform.localScale = scaleX;

        if (ShouldFlipYAxis())
        {
            // Flip the Y scale to change the direction
            Vector3 scaleY = transform.localScale; // Change the variable name to scaleY
            scaleY.y *= -1; // Flip along the Y-axis maybe?
            transform.localScale = scaleY;
        }
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
    
    bool ShouldFlipYAxis()
    {
        return nextWaypoint == 3 || nextWaypoint == wayPoints.Length - 1;
    }
}