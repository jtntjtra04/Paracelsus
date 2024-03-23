using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    // HP
    [SerializeField] private float startHP;
    [SerializeField] private float respawn_timer;
    public float currHP { get; private set; }

    // HP Potion
    public float maxPotion = 5f;
    public float hpPotion = 3f;

    // Spawn Positions
    private Vector2 startPosition;
    private Vector2 checkpointPosition;
    private bool canSetCheckpoint = false;

    // Spirit
    private bool wind_spirit = false; // for temporary element
    private bool fire_spirit = false;
    private bool water_spirit = false;
    private bool earth_spirit = false;
    public bool permanent_wind = false; // for permanent element
    public bool permanent_fire = false;
    public bool permanent_water = false;
    public bool permanent_earth = false;

    // Player Ability
    public bool double_jump = false; // double jump locked
    public bool glide = false; // glide locked
    public bool dash = false; // dash locked
    public bool stomp = false; // stomp locked

    // Knockback Effect
    public float knockback_force;
    public float knockback_counter = 0;
    public float knockback_time;
    public bool KnockFromRight;
    public bool KnockFromLeft;

    // UI
    public GameObject DeathUI;
    public GameObject EndGameUI;
    public Animator FadeBox;

    // References
    private Animator anim;
    private PlayerMovement player_movement;
    private PlayerAttack player_attack;
    private Rigidbody2D body;
    private SwitchSkills barrier;
    [SerializeField] private GameObject boss;
    public BossHPSystem boss_hp;
    public JumpEnemyAttack boss_movement;
    private ElementSwitching element;

    // Death System
    private bool isDeathInProgress = false;

    private void Start()
    {
        // Respawn point
        startPosition = transform.position;
        checkpointPosition = startPosition;
    }

    private void Awake()
    {
        // HP
        currHP = startHP;
        anim = GetComponent<Animator>();
        player_movement = GetComponent<PlayerMovement>();
        player_attack = GetComponent<PlayerAttack>();
        body = GetComponent<Rigidbody2D>();
        barrier = GetComponent<SwitchSkills>();
        element = GetComponent<ElementSwitching>();
        if (barrier == null)
        {
            Debug.Log("Barrier GameObject is null in GameController!");
        }

        //MusicPlayer.clip = WindMusic; //might need to place this somewhere else
        //MusicPlayer.Play();

    }
    public void TakeDamage(float damage)
    {
        // Damage calculations
        currHP = Mathf.Clamp(currHP - damage, 0, startHP);
        AudioManager.instance.PlaySFX("CelsusHurt");
        Debug.Log("Current HP: " + currHP); // Add this line

        if (currHP <= 0)
        {
            Death();
        }
    }

    // Hitting an Obstacle or Checkpoint or Pillar
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Obstacles"))
        {
            // Player hitting wind slime
            if(barrier.barrierPrefabInstance == null)
            {
                if (currHP > 0)
                {
                    knockback_counter = knockback_time; // set the counter to do decrement
                    if (collision.transform.position.x <= transform.position.x) // player got touch from left
                    {
                        KnockFromLeft = true;
                    }
                    else if (collision.transform.position.x > transform.position.x) // player got touch from right
                    {
                        KnockFromRight = true;
                    }
                    TakeDamage(1);
                }
            }
        }
        else if (collision.CompareTag("WindSlime") || collision.CompareTag("EarthSlime") || collision.CompareTag("WaterSlime") || collision.CompareTag("FireSlime"))
        {
            // Player hitting normal slime
            if(barrier.barrierPrefabInstance == null)
            {
                Debug.Log("Barrier is null");
                if(currHP > 0)
                {
                    knockback_counter = knockback_time; // set the counter to do decrement
                    if (collision.transform.position.x <= transform.position.x) // player got touch from left
                    {
                        KnockFromLeft = true;
                    }
                    else if (collision.transform.position.x > transform.position.x) // player got touch from right
                    {
                        KnockFromRight = true;
                    }
                    TakeDamage(1);
                }
            }
        }
        else if (collision.CompareTag("Checkpoint"))
        {
            canSetCheckpoint = true; // Allow setting checkpoint
        }
        else if (collision.CompareTag("WindSpirit"))
        {
            wind_spirit = true;
        }
        else if (collision.CompareTag("WaterSpirit"))
        {
            water_spirit = true;
        }
        else if (collision.CompareTag("FireSpirit"))
        {
            fire_spirit = true;
        }
        else if(collision.CompareTag("EarthSpirit"))
        {
            earth_spirit = true;
        }
        else if (collision.CompareTag("Ending"))
        {
            StartCoroutine(EndGame());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Checkpoint"))
        {
            canSetCheckpoint = false; // Stop allowing setting checkpoint
        }
        else if (collision.CompareTag("WindSpirit"))
        {
            wind_spirit = false;
        }
        else if (collision.CompareTag("WaterSpirit"))
        {
            water_spirit = false;
        }
        else if (collision.CompareTag("FireSpirit"))
        {
            fire_spirit = false;
        }
        else if (collision.CompareTag("EarthSpirit"))
        {
            earth_spirit = false;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "SlimeKing")
        {
            // Player hitting boss slime
            if (barrier.barrierPrefabInstance == null)
            {
                Debug.Log("Barrier is null");
                if(currHP > 0)
                {
                    knockback_counter = knockback_time; // set the counter to do decrement
                    if (collision.transform.position.x <= transform.position.x) // player got touch from left
                    {
                        KnockFromLeft = true;
                    }
                    if (collision.transform.position.x > transform.position.x) // player got touch from right
                    {
                        KnockFromRight = true;
                    }
                    TakeDamage(1);
                }
            }   
        }
    }
    private void Update()
    {
        if(knockback_counter > 0)
        {
            if (KnockFromRight)
            {
                body.velocity = new Vector2(-knockback_force, body.velocity.y);
            }
            if (KnockFromLeft)
            {
                body.velocity = new Vector2(knockback_force, body.velocity.y);
            }
            knockback_counter -= Time.deltaTime;
        }
        else
        {
            KnockFromRight = false;
            KnockFromLeft = false;
        }

        if (canSetCheckpoint && Input.GetKeyDown(KeyCode.F))
        {
            SetCheckpoint();
            AudioManager.instance.PlaySFX("Checkpoint");
            
            if(currHP < 3)
            {
                currHP = 3;
            }

            if(hpPotion < 3)
            {
                hpPotion = 3;
            }
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (currHP < 3)
            {
                Heal();
            }
        }
        // spirit mechanic
        if (wind_spirit && Input.GetKeyDown(KeyCode.F) && !double_jump)
        {
            double_jump = true;
            permanent_wind = true; // unlock permanent element
            element.wind_element = true;
            Debug.Log("Double Jump Ability Unlocked");
            AudioManager.instance.PlaySFX("AbilityUnlocked");
        }
        if (water_spirit && Input.GetKeyDown(KeyCode.F) && !glide)
        {
            glide = true;
            permanent_water = true;
            element.water_element = true;
            Debug.Log("Glide Ability Unlocked");
            AudioManager.instance.PlaySFX("AbilityUnlocked");
        }
        if (fire_spirit && Input.GetKeyDown(KeyCode.F) && !dash)
        {
            dash = true;
            permanent_fire = true;
            element.fire_element = true;
            Debug.Log("Dash Ability Unlocked");
            AudioManager.instance.PlaySFX("AbilityUnlocked");
        }
        if (earth_spirit && Input.GetKeyDown(KeyCode.F) && !stomp)
        {
            stomp = true;
            permanent_earth = true;
            element.earth_element = true;
            Debug.Log("Stomp Ability Unlocked");
            AudioManager.instance.PlaySFX("AbilityUnlocked");
        }
    }
    public void Heal()
    {
        if (hpPotion > 0)
        {
            currHP += 1;
            hpPotion -= 1;
            AudioManager.instance.PlaySFX("PotionDrink");
        }
    }


    void SetCheckpoint()
    {
        checkpointPosition = transform.position;
    }
    void Death()
    {
        if (!isDeathInProgress)
        {
            StartCoroutine(DeathAnimation(respawn_timer));
        }
    }
    void Respawn()
    {
        isDeathInProgress = false;
        boss_hp.boss_healthbar.gameObject.SetActive(false);

        if (boss != null)
        {
            boss.transform.position = boss_movement.original_position;
            //boss.transform.rotation = quaternion.Euler(0, 180, 0);
            boss_movement.speed = 0;
        }

        if (checkpointPosition != Vector2.zero) // Check if a checkpoint is set
        {
            transform.position = checkpointPosition; // Respawn at the checkpoint
        }
        else
        {
            transform.position = startPosition; // Respawn at the starting position if no checkpoint
        }
        currHP = startHP;

        body.constraints &= ~RigidbodyConstraints2D.FreezePositionX; // unfreeze player position x
        player_movement.enabled = true; // player can move again
        player_attack.enabled = true; // player can attack again

        anim.Play("Idle"); // play and set to default animation

        AudioManager.instance.PlayMusic("Theme");

        DeathUI.SetActive(false);
    }    
    private IEnumerator DeathAnimation(float wait_time)
    {
        isDeathInProgress = true; // Set the flag when the death animation starts

        player_movement.enabled = false; // player can't move
        player_attack.enabled = false; // player can't attack
        body.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
        anim.SetTrigger("defeat");

        AudioManager.instance.music_source.Stop();
        AudioManager.instance.PlaySFX("CelsusDeath");

        yield return new WaitForSeconds(respawn_timer);

        DeathUI.SetActive(true);

        while (!Input.GetMouseButtonDown(0))
        {
            yield return null;
        }

        Respawn();
    }
    private IEnumerator EndGame()
    {
        FadeBox.Play("FadeOut");
        yield return new WaitForSeconds(5);
        DialogueManager.CutscenePlay = true;
        AudioManager.instance.music_source.Stop();

        EndGameUI.SetActive(true);
        FadeBox.Play("FadeIn");
        AudioManager.instance.PlayMusic("EndingMusic");

        while (!Input.GetMouseButtonDown(0))
        {
            yield return null;
        }
        DialogueManager.CutscenePlay = false;
        AudioManager.instance.music_source.Stop();
        SceneManager.LoadScene("MainMenu");
    }
    public void KnockBack(Vector2 knockback_direction)
    {
        knockback_counter = knockback_time;

        if (knockback_direction.x < 0)
        {
            KnockFromRight = true;
        }
        else
        {
            KnockFromLeft = true;
        }
    }
}