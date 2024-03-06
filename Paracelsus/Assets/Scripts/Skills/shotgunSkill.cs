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
            }
        } 
}
