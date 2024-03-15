using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JumpEnemyAttack : MonoBehaviour
{
    [Header("For Patrolling")]
    public float speed;
    private float direction = 1;
    private bool facing_right = true;

    [SerializeField] Transform groundCheckPoint;
    [SerializeField] Transform wallCheckPoint;
    [SerializeField] float radius;
    [SerializeField] LayerMask ground_layer;
    private bool check_ground;
    private bool check_wall;

    [Header("For JumpAttacking")]
    [SerializeField] float jump_height;
    [SerializeField] Transform player;
    [SerializeField] Transform ground_check;
    [SerializeField] Vector2 box_radius;
    private bool grounded;

    [Header("For DetectPlayer")]
    [SerializeField] Vector2 boss_sight;
    [SerializeField] LayerMask player_layer;
    private bool player_detected;

    [Header("Other")]
    private Rigidbody2D body;
    private Animator anim;
    private BossHPSystem boss_hp;
    public GameController player_hp;
    public Vector3 original_position;

    [Header("BossGate")]
    [SerializeField] private GameObject EntryBossGate;
    [SerializeField] private GameObject ExitBossGate;
    [SerializeField] private Animator EntryBossGate_anim;
    [SerializeField] private Animator ExitBossGate_anim;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boss_hp = GetComponent<BossHPSystem>();
        original_position = transform.position;
    }
    void FixedUpdate()
    {
        check_ground = Physics2D.OverlapCircle(groundCheckPoint.position, radius, ground_layer);
        check_wall = Physics2D.OverlapCircle(wallCheckPoint.position, radius, ground_layer);
        grounded = Physics2D.OverlapBox(ground_check.position, box_radius, 0, ground_layer);
        player_detected = Physics2D.OverlapBox(transform.position, boss_sight, 0, player_layer);

        anim.SetBool("PlayerDetected", player_detected && player_hp.currHP > 0);
        anim.SetBool("Grounded", grounded);

        if (!player_detected && grounded)
        {
            Patrolling();
        }
        if (player_detected)
        {
            if(player_hp.currHP <= 0)
            {
                Debug.Log("Disable boss HP bar");
                boss_hp.boss_healthbar.gameObject.SetActive(false);
                EntryBossGate_anim.SetTrigger("Rise");
                ExitBossGate_anim.SetTrigger("Rise");
                speed = 0;
            }
            else
            {
                if (!boss_hp.boss_defeat)
                {
                    boss_hp.boss_healthbar.gameObject.SetActive(true);
                    EntryBossGate_anim.SetTrigger("Fall");
                    ExitBossGate_anim.SetTrigger("Fall");
                }
            }
        }
    }
    void Patrolling()
    {
        if (!check_ground || check_wall)
        {
            if (facing_right)
            {
                Debug.Log("GO to left");
                Flip();
            }
            else if (!facing_right)
            {
                Debug.Log("Go to right");
                Flip();
            }
        }
        body.velocity = new Vector2(speed * direction, body.velocity.y);
    }
    void JumpAttack()
    {
        float distance = player.position.x - transform.position.x;

        if (grounded)
        {
            body.AddForce(new Vector2(distance, jump_height), ForceMode2D.Impulse);
            anim.SetTrigger("Jump");
        }
        speed = 10;
    }
    void FlipTowardsPlayer()
    {
        float player_position = player.position.x - transform.position.x;

        if (player_position < 0 && facing_right)
        {
            Flip();
        }
        else if (player_position > 0 && !facing_right)
        {
            Flip();
        }
    }
    void Flip()
    {
        direction *= -1;
        facing_right = !facing_right;
        transform.Rotate(0, 180, 0);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(groundCheckPoint.position, radius);
        Gizmos.DrawWireSphere(wallCheckPoint.position, radius);

        Gizmos.color = Color.green;
        Gizmos.DrawCube(ground_check.position, box_radius);

        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, boss_sight);
    }
}
