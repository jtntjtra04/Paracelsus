using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private BoxCollider2D BoxCollider;
    private Animator anim;
    [SerializeField] private float speed;
    private bool hit;
    private float direction;
    private float life_time;

    private void Awake()
    {
        BoxCollider = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        if (hit)
        {
            return;
        }
        float movement_speed = speed * Time.deltaTime * direction;
        transform.Translate(movement_speed, 0, 0);

        // projectile lifetime
        life_time += Time.deltaTime;
        if (life_time > 5)
        {
            gameObject.SetActive(false);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        hit = true;
        BoxCollider.enabled = false;
        anim.SetTrigger("explode");
    }
    public void SetDirection(float _direction)
    {
        life_time = 0;
        gameObject.SetActive(true);
        hit = false;
        BoxCollider.enabled = true;
        direction = _direction;

        float localScaleX = transform.localScale.x;
        if (Mathf.Sign(localScaleX) != _direction) 
        {
            localScaleX = -localScaleX;
        }
        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
    }
    private void deactivate()
    {
        gameObject.SetActive(false);
    }

}
