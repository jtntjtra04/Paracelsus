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

    private float CD_timer = 100;
    
    //references
    private Animator anim;
    // private HealthUI player_HP;
    private GameController player_HP;

    private void Awake()
    {
        anim = GetComponent<Animator>();
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
                anim.SetTrigger("SkeletonAttack");
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
        if (PlayerDetected()) //Player still in range or still hit the box 
        {
            player_HP.TakeDamage(damage);
        }
    }
}
