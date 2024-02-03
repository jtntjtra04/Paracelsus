using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrusherHorizontal : MonoBehaviour
{
    public float leftspeed;
    public float rightspeed;
    public Transform left;
    public Transform right;
    bool chop;

    void Update()
    {
        if (transform.position.x <= left.position.x)
        {
            chop = true;
        }
        if (transform.position.x >= right.position.x)
        {
            chop = false;
        }
        if (chop)
        {
            transform.position = Vector2.MoveTowards(transform.position, right.position, rightspeed * Time.deltaTime);
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, left.position, leftspeed * Time.deltaTime);
        }
    }
}

