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
    private SkeletonAI skeleton_attack;
    private SkeletonPatrol skeleton_movement;
    private SlimeAI slime_movement;
    public EnemyHealthBar hp_bar;
    private Animator anim;

    //audio
    public AudioSource EnemyAudio;
    public AudioClip EnemyDie, EnemyHurt;

    private void Start()
    {
        anim = GetComponent<Animator>();
        skeleton_attack = GetComponent<SkeletonAI>();
        skeleton_movement = GetComponentInParent<SkeletonPatrol>();
        slime_movement = GetComponent<SlimeAI>();
        curr_health = health;
        hp_bar.SetHealth(curr_health, health);
        body = GetComponent<Rigidbody2D>();
    }
    public void EnemyTakeDamage(float damage)
    {
        EnemyAudio.clip = EnemyHurt;
        EnemyAudio.Play();

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

        EnemyAudio.clip = EnemyDie;
        EnemyAudio.Play();

        hp_bar.gameObject.SetActive(false);

        if (skeleton_movement != null)
        {
            Debug.Log("Disabled movement");
            skeleton_movement.enabled = false;
            body.constraints = RigidbodyConstraints2D.FreezePositionX;
        }
        if (skeleton_attack != null)
        {
            Debug.Log("Disables attack");
            skeleton_attack.enabled = false;
        }
        if (slime_movement != null)
        {
            Debug.Log("Disabled slime movement");
            slime_movement.enabled = false;
        }
        
        yield return new WaitForSeconds(1.5f);

        Die();
    }
    public void Die()
    {
        gameObject.SetActive(false);
    }
}
