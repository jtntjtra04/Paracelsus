using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHPSystem : MonoBehaviour
{
    public float health;
    public Slider boss_healthbar;
    public bool boss_defeat;
    public Color low_hp;
    public Color high_hp;

    // References
    private Rigidbody2D body;
    private BoxCollider2D box_collider;
    private Animator anim;
    private JumpEnemyAttack boss_movement;

    // Boss Gate
    [SerializeField] private GameObject EntryBossGate;
    [SerializeField] private GameObject ExitBossGate;
    [SerializeField] private Animator EntryBossGate_anim;
    [SerializeField] private Animator ExitBossGate_anim;
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
            boss_healthbar.fillRect.GetComponent<Image>().color = Color.Lerp(low_hp, high_hp, boss_healthbar.normalizedValue);
            if (health <= 0)
            {
                StartCoroutine(BossDefeat());
            }
        }
    }
    private IEnumerator BossDefeat()
    {
        boss_defeat = true;
        EntryBossGate_anim.SetBool("BossDefeat", true);
        ExitBossGate_anim.SetBool("BossDefeat", true);
        //Destroy(EntryBossGate);
        //Destroy(ExitBossGate_anim);
        
        anim.SetTrigger("Defeat");
        boss_healthbar.gameObject.SetActive(false);
        AudioManager.instance.PlaySFX("BossKilled");

        if (boss_movement != null)
        {
            Debug.Log("Disable boss movement");
            boss_movement.enabled = false;
            body.isKinematic = true;
            body.constraints = RigidbodyConstraints2D.FreezeAll;
            box_collider.enabled = false;
        }
        yield return new WaitForSeconds(7.2f);

        AudioManager.instance.music_source.Stop();
        AudioManager.instance.PlayMusic("Theme");

        BossDie();
    }
    public void BossDie()
    {
        gameObject.SetActive(false);
    }
}
