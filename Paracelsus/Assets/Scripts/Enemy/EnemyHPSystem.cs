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
    public EnemyHealthBar hp_bar;
    private Animator anim;

    //audio
    public AudioSource SkeletonAudio;
    public AudioClip SkeletonDie, SkeletonHurt;

    private void Start()
    {
        anim = GetComponent<Animator>();
        skeleton_attack = GetComponent<SkeletonAI>();
        skeleton_movement = GetComponentInParent<SkeletonPatrol>();
        curr_health = health;
        hp_bar.SetHealth(curr_health, health);
        body = GetComponent<Rigidbody2D>();
        if(skeleton_movement == null)
        {
            Debug.Log("Movement null raaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa");
        }
        if(skeleton_attack == null) 
        {
            Debug.Log("Attack null raaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa");
        }
    }
    public void EnemyTakeDamage(float damage)
    {
        SkeletonAudio.clip = SkeletonHurt;
        SkeletonAudio.Play();

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

        SkeletonAudio.clip = SkeletonDie;
        SkeletonAudio.Play();

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
        
        yield return new WaitForSeconds(1.5f);

        Die();
    }
    public void Die()
    {
        gameObject.SetActive(false);
    }
}
