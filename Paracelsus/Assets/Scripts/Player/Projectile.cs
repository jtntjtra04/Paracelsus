using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private BoxCollider2D BoxCollider;
    private Animator anim;
    [SerializeField] private float speed;
    private bool hit;
    private float direction;
    private float life_time;
    private Rigidbody2D body;
    public float damage;


    private void Awake()
    {
        BoxCollider = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        body = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        if (hit)
        {
            return;
        }

        float movement_speed = speed * Time.deltaTime * direction;
        transform.Translate(movement_speed, 0, 0);

        // projectile lifetime
        life_time += Time.deltaTime;
        if (life_time > 5)
        {
            gameObject.SetActive(false);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
       

        EnemyHPSystem enemy_hp = collision.GetComponent<EnemyHPSystem>();

        if (enemy_hp != null)
        {
            string projectile_tag = gameObject.tag;

            if (collision.CompareTag("FireEnemy") && projectile_tag == "Water") // checking Skeleton
            {
                enemy_hp.EnemyTakeDamage(damage * 1.5f);
            }
            else if (collision.CompareTag("FireEnemy") && projectile_tag == "Fire")
            {
                enemy_hp.EnemyTakeDamage(damage * 0.75f);
            }
            else if (collision.CompareTag("WindEnemy") && projectile_tag == "Fire")
            {
                enemy_hp.EnemyTakeDamage(damage * 1.5f);
            }
            else if (collision.CompareTag("WindEnemy") && projectile_tag == "Wind")
            {
                enemy_hp.EnemyTakeDamage(damage * 0.75f);
            }
            else if (collision.CompareTag("WaterEnemy") && projectile_tag == "Wind")
            {
                enemy_hp.EnemyTakeDamage(damage * 1.5f);
            }
            else if (collision.CompareTag("WaterEnemy") && projectile_tag == "Water")
            {
                enemy_hp.EnemyTakeDamage(damage * 0.75f);
            }
            else if (collision.CompareTag("FireSlime") && projectile_tag == "Water")
            {
                enemy_hp.EnemyTakeDamage(damage * 1.5f);
            }
            else if (collision.CompareTag("FireSlime") && projectile_tag == "Fire")
            {
                enemy_hp.EnemyTakeDamage(damage * 0.75f);
            }
            else if (collision.CompareTag("WindSlime") && projectile_tag == "Fire")
            {
                enemy_hp.EnemyTakeDamage(damage * 1.5f);
            }
            else if (collision.CompareTag("WindSlime") && projectile_tag == "Wind")
            {
                enemy_hp.EnemyTakeDamage(damage * 0.75f);
            }
            else if (collision.CompareTag("WaterSlime") && projectile_tag == "Wind")
            {
                enemy_hp.EnemyTakeDamage(damage * 1.5f);
            }
            else if (collision.CompareTag("WaterSlime") && projectile_tag == "Water")
            {
                enemy_hp.EnemyTakeDamage(damage * 0.75f);
            }
            else if (projectile_tag == "Earth")
            {
                enemy_hp.EnemyTakeDamage(damage + 20f);
            }
            else
            {
                enemy_hp.EnemyTakeDamage(damage);
            } 
        }
        hit = true;
        BoxCollider.enabled = false;
        anim.SetTrigger("explode");
    }
    public void SetDirection(float _direction)
    {
        life_time = 0;
        gameObject.SetActive(true);
        hit = false;
        BoxCollider.enabled = true;
        direction = _direction;

        float localScaleX = transform.localScale.x;
        if (Mathf.Sign(localScaleX) != _direction) 
        {
            localScaleX = -localScaleX;
        }
        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
    }
    private void deactivate()
    {
        gameObject.SetActive(false);
    }

}
