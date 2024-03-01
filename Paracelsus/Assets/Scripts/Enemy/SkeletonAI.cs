using System.Collections;
using System.Collections.Generic;
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
    //references
    private Animator anim;
    private GameController player_HP;
    private SwitchSkills skills;
    private float suspendedGravityScale = 0.5f;
    private float suspensionDuration = 1f;
    private bool isSuspended = false;
    private float suspensionTimer = 0f;
    public Transform pillarPosition;
    
    //audio
    public AudioSource SkeletonAudio;
    public AudioClip SkeletonAttack;

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
                SkeletonAudio.clip = SkeletonAttack;
                SkeletonAudio.Play();
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
            if(skills.pillarPrefabInstance == null)
            {
                skills.pillarPrefabInstance = Instantiate(skills.pillarPrefab, pillarPosition.position, Quaternion.identity);
                Destroy(skills.pillarPrefabInstance, 0.45f);
        //      skills.earthFunc = false;
            }
           
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
            player_HP.TakeDamage(damage);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.tag);
        if (other.gameObject.CompareTag("WindSkill"))
        {   
            body.AddForce(Vector2.up * suspensionForce);
            isSuspended = true;
        }
    }
    
}
