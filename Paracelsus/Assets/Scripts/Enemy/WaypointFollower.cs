using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointFollower : MonoBehaviour
{
    public GameObject Waypoint1;
    public GameObject Waypoint2;
    private Rigidbody2D rb;
    private Animator Anim;
    private Transform CurrentWaypoint;
    public float speed;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Anim = GetComponent<Animator>();
        CurrentWaypoint = Waypoint2.transform;
        Anim.SetBool("isRunning", true);
    }
    void Update()
    {
        Vector2 waypoint = CurrentWaypoint.position - transform.position;
        if (CurrentWaypoint == Waypoint2.transform)
        {
            rb.velocity = new Vector2(speed, 0);
        }
        else
        {
            rb.velocity = new Vector2(-speed, 0);
        }
        
        if (Vector2.Distance(transform.position, CurrentWaypoint.position) < 0.5f && CurrentWaypoint == Waypoint2.transform)
        {
            flip();
            CurrentWaypoint = Waypoint1.transform;
        }
        if (Vector2.Distance(transform.position, CurrentWaypoint.position) < 0.5f && CurrentWaypoint == Waypoint1.transform)
        {
            flip();
            CurrentWaypoint = Waypoint2.transform;
        }
    }
    private void flip()
        {
            Vector3 localScale = transform.localScale;
            localScale.x *= -1;
            transform.localScale = localScale;
        }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(Waypoint1.transform.position, 0.5f);
        Gizmos.DrawWireSphere(Waypoint2.transform.position, 0.5f);
        Gizmos.DrawLine(Waypoint1.transform.position, Waypoint2.transform.position);
    }

    
}
