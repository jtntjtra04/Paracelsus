using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    private Rigidbody2D body;
    private Animator anim;
    private bool grounded;
    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        float horizontal_input = Input.GetAxis("Horizontal");

        body.velocity = new Vector2(horizontal_input * speed, body.velocity.y);

        if (horizontal_input > 0.01)
            transform.localScale = new Vector3(3, 3, 3);
        else if (horizontal_input < -0.01)
            transform.localScale = new Vector3(-3, 3, 3);

        if (Input.GetKeyDown(KeyCode.Space) && grounded)
            Jump();

        anim.SetBool("run", horizontal_input != 0);
        anim.SetBool("grounded", grounded);
    }
    private void Jump()
    {
        body.velocity = new Vector2(body.velocity.x, speed);
        anim.SetTrigger("jump");
        grounded = false;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
            grounded = true;
    }
}
