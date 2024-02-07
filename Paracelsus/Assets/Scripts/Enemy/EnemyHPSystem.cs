using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHPSystem : MonoBehaviour
{
    public float health;
    public float curr_health;
    public EnemyHealthBar hp_bar;
    private void Start()
    {
        curr_health = health;
        hp_bar.SetHealth(curr_health, health);
    }
    public void EnemyTakeDamage(float damage)
    {
        curr_health -= damage;
        hp_bar.SetHealth(curr_health, health);

        if (curr_health <= 0)
        {
            Die();
        }
    }
    public void Die()
    {
        gameObject.SetActive(false);
    }
}
