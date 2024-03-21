using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHPSystem : MonoBehaviour
{
    public float health;
    public float curr_health;
    private bool isDefeat = false;

    // References
    private Rigidbody2D body;
    private BoxCollider2D box_collider;
    private SkeletonAI skeleton_attack; // skeleton
    private SkeletonPatrol skeleton_movement; // skeleton
    private SlimeAI slime_movement; // slime movement 1, 3
    private SlimeAI2wp slime_movement_2; // slime movement 2, 6
    private SlimeAIWS4 slime_movement_4; // slime movement 4
    private SlimeAIWS5 slime_movement_5; // slime movement 5 , 15
    private SlimeAIWS7 slime_movement_7; // slime movement 7
    private SlimeAIWS8 slime_movement_8; // slime movement 8
    private SlimeAIWF13 slime_movement_13; // slime movement 13
    private SlimeAIWrS16 slime_movement_16; // slime movement 16
    public EnemyHealthBar hp_bar;
    private Animator anim;
    public GameController player_hp;

    //audio
    //public AudioSource EnemyAudio;
    //public AudioClip EnemyDie, EnemyHurt;

    private void Start()
    {
        anim = GetComponent<Animator>();
        skeleton_attack = GetComponent<SkeletonAI>();
        skeleton_movement = GetComponentInParent<SkeletonPatrol>();
        slime_movement = GetComponent<SlimeAI>();
        slime_movement_2 = GetComponent<SlimeAI2wp>();
        slime_movement_4 = GetComponent<SlimeAIWS4>();
        slime_movement_5 = GetComponent<SlimeAIWS5>();
        slime_movement_7 = GetComponent<SlimeAIWS7>();
        slime_movement_8 = GetComponent<SlimeAIWS8>();
        slime_movement_13 = GetComponent<SlimeAIWF13>();
        slime_movement_16 = GetComponent<SlimeAIWrS16>();
        
        curr_health = health;
        hp_bar.SetHealth(curr_health, health);
        body = GetComponent<Rigidbody2D>();
        box_collider = GetComponent<BoxCollider2D>();
    }
    private void Update()
    {
        if(player_hp != null)
        {
            if(player_hp.currHP <= 0)
            {
                curr_health = health;
                hp_bar.SetHealth(curr_health, health);
            }
        }
    }
    public void EnemyTakeDamage(float damage)
    {
        AudioManager.instance.PlaySFX("EnemyHit");

        if (!isDefeat)
        {
            curr_health -= damage;
            hp_bar.SetHealth(curr_health, health);

            if (curr_health <= 0)
            {
                StartCoroutine(DefeatAnimation());
            }
        }
    }
    private IEnumerator DefeatAnimation()
    {
        isDefeat = true;
        anim.SetTrigger("Defeat");

        hp_bar.gameObject.SetActive(false);

        if (skeleton_movement != null)
        {
            Debug.Log("Disabled movement");
            skeleton_movement.enabled = false;
            body.constraints = RigidbodyConstraints2D.FreezePositionX;
        }
        if (skeleton_attack != null)
        {
            AudioManager.instance.PlaySFX("SkeletonDeath");
            Debug.Log("Disables attack");
            skeleton_attack.enabled = false;
        }
        if (slime_movement != null)
        {
            slime_movement.enabled = false;
            box_collider.enabled = false;
            AudioManager.instance.PlaySFX("SlimeDeath");
        }
        if (slime_movement_2 != null)
        {
            slime_movement_2.enabled = false;
            box_collider.enabled = false;
            AudioManager.instance.PlaySFX("SlimeDeath");
        }
        if (slime_movement_4 != null)
        {
            slime_movement_4.enabled = false;
            box_collider.enabled = false;
            AudioManager.instance.PlaySFX("SlimeDeath");
        }
        if (slime_movement_5 != null)
        {
            slime_movement_5.enabled = false;
            box_collider.enabled = false;
            AudioManager.instance.PlaySFX("SlimeDeath");
        }
        if (slime_movement_7 != null)
        {
            slime_movement_7.enabled = false;
            box_collider.enabled = false;
            AudioManager.instance.PlaySFX("SlimeDeath");
        }
        if (slime_movement_8 != null)
        {
            slime_movement_8.enabled = false;
            box_collider.enabled = false;
            AudioManager.instance.PlaySFX("SlimeDeath");
        }
        if (slime_movement_13 != null)
        {
            slime_movement_13.enabled = false;
            box_collider.enabled = false;
            AudioManager.instance.PlaySFX("SlimeDeath");
        }
        if (slime_movement_16 != null)
        {
            slime_movement_16.enabled = false;
            box_collider.enabled = false;
            AudioManager.instance.PlaySFX("SlimeDeath");
        }

        yield return new WaitForSeconds(1.5f);

        Die();
    }
    public void Die()
    {
        gameObject.SetActive(false);
    }
}
