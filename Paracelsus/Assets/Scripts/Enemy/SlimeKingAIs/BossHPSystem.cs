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
    private void Start()
    {
        anim = GetComponent<Animator>();
        body = GetComponent<Rigidbody2D>();
        box_collider = GetComponent<BoxCollider2D>();
    }
    private void Update()
    {
        boss_healthbar.value = health;
    }
    public void BossTakeDamage(float damage)
    {
        AudioManager.instance.PlaySFX("Enemyhit");
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
        boss_healthbar.gameObject.SetActive(false); 
        yield return new WaitForSeconds(3f);

        BossDie();
    }
    public void BossDie()
    {
        gameObject.SetActive(false);
    }
}
