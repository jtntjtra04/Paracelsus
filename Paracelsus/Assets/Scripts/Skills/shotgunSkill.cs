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
        
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyHPSystem enemy_hp = collision.GetComponent<EnemyHPSystem>();
        BossHPSystem boss_hp = collision.GetComponent<BossHPSystem>();
            
        if (enemy_hp != null)
        {
            if (collision.CompareTag("FireEnemy"))
            {
                enemy_hp.EnemyTakeDamage(damage * 0.75f);
            }
            else if (collision.CompareTag("WindEnemy"))
            {
                enemy_hp.EnemyTakeDamage(damage * 1.5f);
            }
            else if (collision.CompareTag("WaterEnemy"))
            {
                enemy_hp.EnemyTakeDamage(damage);
            }
            else if (collision.CompareTag("EarthEnemy"))
            {
                enemy_hp.EnemyTakeDamage(damage);
            }
            else if (collision.CompareTag("WindSlime"))
            {
                enemy_hp.EnemyTakeDamage(damage * 1.5f);
            }
            else if (collision.CompareTag("FireSlime"))
            {
                enemy_hp.EnemyTakeDamage(damage * 0.75f);
            }
            else if (collision.CompareTag("WaterSlime"))
            {
                enemy_hp.EnemyTakeDamage(damage);
            }
            else if (collision.CompareTag("EarthSlime"))
            {
                enemy_hp.EnemyTakeDamage(damage);
            }
        }
        else if(boss_hp != null)
        {
            if(collision.CompareTag("SlimeKing"))
            {
                boss_hp.BossTakeDamage(damage);
            }
        }
    } 
}
