using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jump_power;
    [SerializeField] private float dash_power;
    [SerializeField] private float dash_cooldown;
    [SerializeField] private float dash_time;

    private Rigidbody2D body;
    private Animator anim;
    private bool grounded;
    private float coyote_time = 0.2f;
    private float coyote_counter;
    private bool can_dash = true;
    private bool currently_dash;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (currently_dash)
        {
            return;
        }

        float horizontal_input = Input.GetAxis("Horizontal");

        body.velocity = new Vector2(horizontal_input * speed, body.velocity.y);

        if (grounded)
        {
            coyote_counter = coyote_time;
        }
        else
        {
            coyote_counter -= Time.deltaTime;
        }

        if (horizontal_input > 0.01)
            transform.localScale = new Vector3(3, 3, 3);
        else if (horizontal_input < -0.01)
            transform.localScale = new Vector3(-3, 3, 3);

        if (Input.GetKeyDown(KeyCode.Space) && coyote_counter > 0f)
            Jump();
        if (Input.GetKeyDown(KeyCode.Space) && body.velocity.y > 0f)
            ReleaseJump();

        anim.SetBool("run", horizontal_input != 0);
        anim.SetBool("grounded", grounded);

        if (Input.GetKeyDown(KeyCode.LeftShift) && can_dash)
            StartCoroutine(Dash());
    }
    private void Jump()
    {
        body.velocity = new Vector2(body.velocity.x, jump_power);
        anim.SetTrigger("jump");
        grounded = false;
    }
    private void ReleaseJump()
    {
        body.velocity = new Vector2(body.velocity.x, body.velocity.y * 0.5f);
        coyote_counter = 0f;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
            grounded = true;
    }
    private IEnumerator Dash()
    {
        can_dash = false;
        currently_dash = true;
        float gravity = body.gravityScale;
        body.gravityScale = 0f;
        body.velocity = new Vector2(dash_power * transform.localScale.x, 0f);
        yield return new WaitForSeconds(dash_time);
        currently_dash = false;
        body.gravityScale = gravity;
        yield return new WaitForSeconds(dash_cooldown);
        can_dash = true;
    }
}
