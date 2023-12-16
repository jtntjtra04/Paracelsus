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
    [SerializeField] private Transform ground_check;
    [SerializeField] private LayerMask ground_layer;

    private Rigidbody2D body;
    private Animator anim;
    //private bool grounded;
    private float coyote_time = 0.2f;
    private float coyote_counter;
    private float jumpbuffer_time = 0.2f;
    private float jumpbuffer_counter;
    private bool can_dash = true;
    private bool currently_dash;
    private bool double_jump;

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

        if (grounded())
        {
            coyote_counter = coyote_time;
        }
        else
        {
            coyote_counter -= Time.deltaTime;
        }
        
        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            jumpbuffer_counter = jumpbuffer_time;
        }
        else
        {
            jumpbuffer_counter -= Time.deltaTime;
        }

        if (horizontal_input > 0.01)
            transform.localScale = new Vector3(3, 3, 3);
        else if (horizontal_input < -0.01)
            transform.localScale = new Vector3(-3, 3, 3);

        if (jumpbuffer_counter > 0f && coyote_counter > 0f)
            Jump();
        //if (Input.GetKeyUp(KeyCode.Space) && body.velocity.y > 0f)
        //    ReleaseJump();

        anim.SetBool("run", horizontal_input != 0);
        anim.SetBool("grounded", grounded());

        if (Input.GetKeyDown(KeyCode.LeftShift) && can_dash)
            StartCoroutine(Dash());
    }
    private void Jump()
    {
        anim.SetTrigger("jump");
        body.velocity = new Vector2(body.velocity.x, jump_power);
        
        //grounded = false;
        //double_jump = !double_jump;
        coyote_counter = 0f;
        jumpbuffer_counter = 0f;
    }
    //private void ReleaseJump()
    //{
    //    body.velocity = new Vector2(body.velocity.x, body.velocity.y * 0.5f);
    //    anim.SetTrigger("jump");  
    //}
    private bool grounded()
    {
        return Physics2D.OverlapCircle(ground_check.position, 0.2f, ground_layer);
    }
    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.tag == "Ground")
    //        grounded = true;
    //}
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
