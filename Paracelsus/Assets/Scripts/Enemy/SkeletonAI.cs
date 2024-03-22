using Ink.Parsed;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SkeletonAI : MonoBehaviour
{
    [SerializeField] private float attack_CD;
    [SerializeField] private float range;
    [SerializeField] private float collider_distance;
    [SerializeField] private int damage;
    [SerializeField] private BoxCollider2D BoxCollider;
    [SerializeField] private LayerMask player_layer;
    [SerializeField] private float suspensionForce;

    private float CD_timer = 100;
    private Rigidbody2D body;
    public Transform player;
    public float stoppingDistance = 1f;
    //references
    private Animator anim;
    private GameController player_HP;
    private SwitchSkills skills;
    private float suspendedGravityScale = 0.5f;
    private float suspensionDuration = 1f;
    private bool isSuspended = false;
    private float suspensionTimer = 0f;
    public Transform pillarPosition;
    public bool isBeingPushed;
    public float pillarForce = 10000f;
    
    //audio
    //public AudioSource SkeletonAudio;
    //public AudioClip SkeletonAttack;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        skills = FindFirstObjectByType<SwitchSkills>();
       
    }
    private void Update()
    {
        CD_timer += Time.deltaTime;
       
        // attack when player gets detected
        if (PlayerDetected())
        {
            if (attack_CD <= CD_timer)
            {
                //attack
                CD_timer = 0;
                anim.SetBool("Walk", false);
                anim.SetTrigger("SkeletonAttack");
                Debug.Log("Skeleton Attack");
            }
        }
        if (isSuspended)
        {
            // Adjust gravity scale while suspended
            body.gravityScale = suspendedGravityScale;
            suspensionTimer += Time.deltaTime;
            body.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
            if (suspensionTimer >= suspensionDuration)
            {
                // Revert gravity scale after suspension duration
                body.gravityScale = 50f;
                isSuspended = false;
                suspensionTimer = 0f;
                body.constraints = RigidbodyConstraints2D.None | RigidbodyConstraints2D.FreezeRotation;
            }
        }
        if(skills.earthFunc == true)
        {
            Vector2 enemyDirection = (transform.localScale.x < 0) ? -transform.right : transform.right;
            StartCoroutine(MoveTowardsPlayer());
            
            skills.earthFunc = false;
            isBeingPushed = true;
        }
    }
    private IEnumerator MoveTowardsPlayer()
    {
        while (Vector2.Distance(transform.position, player.position) > stoppingDistance)
        {
            Vector3 direction = (player.position - transform.position).normalized;
            body.AddForce(direction * pillarForce, ForceMode2D.Force);


            Debug.Log(Vector2.Distance(transform.position, player.position));
            yield return null;

            EnemyHPSystem enemy_hp = GetComponent<EnemyHPSystem>();

            enemy_hp.EnemyTakeDamage(100);

        }
    }
    private bool PlayerDetected()
    {
         RaycastHit2D detect = Physics2D.BoxCast(BoxCollider.bounds.center + transform.right * range * transform.localScale.x * collider_distance, new Vector3(BoxCollider.bounds.size.x * range, BoxCollider.bounds.size.y, BoxCollider.bounds.size.z), 0, Vector2.left, 0, player_layer);

         if (detect.collider != null)
         {
             player_HP = detect.transform.GetComponent<GameController>();
         }

         return detect.collider != null;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(BoxCollider.bounds.center + transform.right * range * transform.localScale.x * collider_distance, new Vector3(BoxCollider.bounds.size.x * range, BoxCollider.bounds.size.y, BoxCollider.bounds.size.z));
    }
    private void DamagePlayer()
    {
        if (PlayerDetected() && player_HP.currHP != 0 && skills.barrierPrefabInstance == null) //Player still in range or still hit the box 
        {
            // Get the direction from the player to the skeleton
            Vector2 knockback_direction = (player_HP.transform.position - transform.position).normalized;

            // Apply knockback to the player
            player_HP.KnockBack(knockback_direction);
            player_HP.TakeDamage(damage);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("WindSkill"))
        {
            body.AddForce(Vector2.up * suspensionForce);
            isSuspended = true;
        }

        EnemyHPSystem enemy_hp = GetComponent<EnemyHPSystem>();

        enemy_hp.EnemyTakeDamage(50);
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
       Collider2D otherCollider = other.collider;
        EnemyHPSystem enemy_hp = otherCollider.GetComponent<EnemyHPSystem>();
       
            if(enemy_hp != null)
            {
                if (other.gameObject.CompareTag("FireSkill"))
                {
                    Debug.Log("ahhh fire");
                    enemy_hp.EnemyTakeDamage(100 * 1.5f);
                }
            }
    }
    
}
