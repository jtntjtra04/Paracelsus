using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointFollower : MonoBehaviour
{
    public GameObject left_waypoint;
    public GameObject right_waypoint;
    private Rigidbody2D body;
    private Animator anim;
    private Transform current_dest;
    public float speed;

    // enemy follow player
    public Transform player_location;
    public bool chasing_player;
    public float chasing_distance;
    public float max_distance;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        current_dest = right_waypoint.transform;
        anim.SetBool("run", true);
    }
    void Update()
    {
        Vector2 waypoint = current_dest.position - transform.position;

        if (Vector2.Distance(transform.position, player_location.position) > max_distance)
        {
            chasing_player = false;
        }

        if (chasing_player)
        {
            if (transform.position.x > player_location.position.x)
            {
                transform.localScale = new Vector3(-0.2792043f, 0.2792043f, 0.2792043f);
                body.velocity = new Vector2(-speed, 0);
            }
            if (transform.position.x < player_location.position.x)
            {
                transform.localScale = new Vector3(0.2792043f, 0.2792043f, 0.2792043f);
                body.velocity = new Vector2(speed, 0);
            }
        }
        else
        {
            if (current_dest == right_waypoint.transform)
            {
                body.velocity = new Vector2(speed, 0);
            }
            else
            {
                body.velocity = new Vector2(-speed, 0);
            }

            if (Vector2.Distance(transform.position, current_dest.position) < 0.5f && current_dest == right_waypoint.transform)
            {
                flip();
                current_dest = left_waypoint.transform;
            }
            if (Vector2.Distance(transform.position, current_dest.position) < 0.5f && current_dest == left_waypoint.transform)
            {
                flip();
                current_dest = right_waypoint.transform;
            }

            if (Vector2.Distance(transform.position, player_location.position) < chasing_distance) // if player gets closer to enemy
            {
                chasing_player = true;
            }
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
        Gizmos.DrawWireSphere(left_waypoint.transform.position, 0.5f);
        Gizmos.DrawWireSphere(right_waypoint.transform.position, 0.5f);
        Gizmos.DrawLine(left_waypoint.transform.position, right_waypoint.transform.position);
    }

    
}
