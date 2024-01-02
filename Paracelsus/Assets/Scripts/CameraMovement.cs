using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float follow_speed = 2f;
    public float y_Offset = 1f;
    public Transform target;

    void Update()
    {
        Vector3 newpos = new Vector3(target.position.x, target.position.y + y_Offset, -10f);
        transform.position = Vector3.Slerp(transform.position, newpos, follow_speed * Time.deltaTime);
    }
}

