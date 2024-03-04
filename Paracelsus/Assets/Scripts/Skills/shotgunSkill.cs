using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shotgunSkill : MonoBehaviour
{
    
    // public GameObject player;
    public float damage;
    void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // float playerScaleX = player.transform.localScale.x;
        // if(playerScaleX < 0)
        // {
        //     Vector2 pelletScaleX = -transform.right;
        // }else
        // {
        //     Vector2 pelletScaleX = transform.right;
        // }
        void OnTriggerEnter2D(Collider2D collision)
        {
            Debug.Log(collision.tag);
        EnemyHPSystem enemy_hp = collision.GetComponent<EnemyHPSystem>();
        

         if (enemy_hp != null)
         {
            string projectile_tag = gameObject.tag;
         if (collision.CompareTag("FireEnemy"))
        {
            enemy_hp.EnemyTakeDamage(damage * 0.75f);
        }
        else if (collision.CompareTag("WindEnemy") )
        {
            enemy_hp.EnemyTakeDamage(damage * 1.5f);
        }
        else if (collision.CompareTag("FireSlime") )
        {
            enemy_hp.EnemyTakeDamage(damage * 0.75f);
        }
        else if (collision.CompareTag("WindSlime"))
        {
            enemy_hp.EnemyTakeDamage(damage * 1.5f);
        }
         }
        } 
    }
}
