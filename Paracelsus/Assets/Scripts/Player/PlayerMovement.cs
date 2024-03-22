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
    [SerializeField] private float glide_velocity; // glide

    // References
    private Rigidbody2D body;
    private Animator anim;
    private GameController ability; // To unlock ability

    private float coyote_time = 0.2f;
    private float coyote_counter;
    private float jumpbuffer_time = 0.2f;
    private float jumpbuffer_counter;
    private bool can_dash = true;
    private bool currently_dash;
    private int extra_jump = 1; // double jump
    private float initial_gravity; // glide

    //audio
    //public AudioSource PlayerAudio;
    //public AudioClip CelsusJump;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        initial_gravity = body.gravityScale; // glide
        ability = GetComponent<GameController>(); // Ability
    }

    private void Update()
    {
        if (DialogueManager.CutscenePlay == true)
        {
            body.velocity = new Vector2(0, body.velocity.y);
            anim.SetBool("run", false);
            return;
        }

        if (DialogueManager.GetInstance().dialogueIsPlaying)
        {
            body.velocity = new Vector2(0, body.velocity.y);
            anim.SetBool("run", false);
            return;
        }
        if (PauseMenu.game_paused)
        {
            return;
        }
        if (ability.knockback_counter > 0) // if the player got knockback, the player can't move
        {
            return;
        }
        if (currently_dash)
        {
            return;
        }

        float horizontal_input = Input.GetAxis("Horizontal");

        body.velocity = new Vector2(horizontal_input * speed, body.velocity.y);

        if (grounded())
        {
            coyote_counter = coyote_time;
            extra_jump = 1;
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

        //character flip
        if (horizontal_input > 0.01)
        {
            transform.localScale = new Vector3(0.46f, 0.46f, 0.46f);
        }
        else if (horizontal_input < -0.01)
        {
            transform.localScale = new Vector3(-0.46f, 0.46f, 0.46f);
        }

        if (Input.GetKey(KeyCode.Space) && body.velocity.y <= 0 && ability.glide)  // if glide unlocked
        {
            anim.SetTrigger("glide");
            body.gravityScale = 0;
            body.velocity = new Vector2(body.velocity.x, y: -glide_velocity);
        }
        else
        {
            body.gravityScale = initial_gravity;
        }

        if (jumpbuffer_counter > 0f && coyote_counter > 0f) // jump mechanic 
        {
            anim.SetBool("grounded", false);
            Jump();
            jumpbuffer_counter = 0f;
        }
        if (Input.GetKeyUp(KeyCode.Space) && body.velocity.y > 0f) // jump higher
        {
            ReleaseJump();
            coyote_counter = 0f;
        }
        if (Input.GetKeyDown(KeyCode.Space) && extra_jump > 0 && ability.double_jump) // if double jump unlocked
        {
            Jump();
            extra_jump--;
        }
        
        anim.SetBool("run", horizontal_input != 0);
        anim.SetBool("grounded", grounded());

        if (Input.GetKeyDown(KeyCode.LeftShift) && can_dash && ability.dash) // if dash unlocked
            StartCoroutine(Dash());
    }
    private void Jump()
    {
        coyote_counter = 0f;
        
        anim.SetTrigger("jump");
        body.velocity = new Vector2(body.velocity.x, jump_power);
        AudioManager.instance.PlaySFX("CelsusJump");
    }
    private void ReleaseJump()
    {
        anim.SetTrigger("jump");
        body.velocity = new Vector2(body.velocity.x, body.velocity.y * 0.5f); 
    }
    private bool grounded()
    {
        return Physics2D.OverlapCircle(ground_check.position, 0.2f, ground_layer);
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
