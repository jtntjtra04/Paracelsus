using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Push : MonoBehaviour
{
    private BoxCollider2D BoxCollider;
    private Animator anim;
    private Rigidbody2D body;
    public float damage;
    private bool hit;
    public SwitchSkills earthskill;
    private void Awake()
    {
        BoxCollider = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        body = GetComponent<Rigidbody2D>();
        earthskill = FindFirstObjectByType<SwitchSkills>();
    }
    // Update is called once per frame
    void Update()
    {
      if(hit)
      {
        return;
      }
    }
     private void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyHPSystem enemy_hp = collision.GetComponent<EnemyHPSystem>();
        BossHPSystem boss_hp = collision.GetComponent<BossHPSystem>();

        if (enemy_hp != null)
        {
            string projectile_tag = gameObject.tag;

            enemy_hp.EnemyTakeDamage(damage + 20f);
            
        }
        else if(boss_hp != null)
        {
            string projectile_tag = gameObject.tag;

            if(collision.CompareTag("SlimeKing"))
            {
                boss_hp.BossTakeDamage(damage);
            }
        }
        if (!earthskill.isCharging)
        {
            hit = true;
            BoxCollider.enabled = false;
            anim.SetTrigger("Explode");
        }
    }

     private void deactivate()
    {
      Destroy(gameObject);
    }

   
}
