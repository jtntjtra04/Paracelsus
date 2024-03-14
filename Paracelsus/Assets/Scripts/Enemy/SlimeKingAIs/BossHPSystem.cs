using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHPSystem : MonoBehaviour
{
    public float health;
    public Slider boss_healthbar;
    private bool boss_defeat;

    // References
    private Rigidbody2D body;
    private BoxCollider2D box_collider;
    private Animator anim;
    private JumpEnemyAttack boss_movement;
    private void Start()
    {
        anim = GetComponent<Animator>();
        body = GetComponent<Rigidbody2D>();
        box_collider = GetComponent<BoxCollider2D>();
        boss_movement = GetComponent<JumpEnemyAttack>();
    }
    private void Update()
    {
        boss_healthbar.value = health;
    }
    public void BossTakeDamage(float damage)
    {
        AudioManager.instance.PlaySFX("EnemyHit");
        if (!boss_defeat)
        {
            health -= damage;
            if (health <= 0)
            {
                StartCoroutine(BossDefeat());
            }
        }
    }
    private IEnumerator BossDefeat()
    {
        boss_defeat = true;
        anim.SetTrigger("Defeat");
        boss_healthbar.gameObject.SetActive(false);
        AudioManager.instance.PlaySFX("BossKilled");

        if (boss_movement != null)
        {
            Debug.Log("Disable boss movement");
            boss_movement.enabled = false;
            body.constraints = RigidbodyConstraints2D.FreezePositionX;
        }
        yield return new WaitForSeconds(3f);

        BossDie();
    }
    public void BossDie()
    {
        gameObject.SetActive(false);
    }
}
