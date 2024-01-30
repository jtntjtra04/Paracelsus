using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class GameController : MonoBehaviour
{
    // HP
    [SerializeField] private float startHP;
    [SerializeField] private float respawn_timer;
    public float currHP { get; private set; }

    // Spawn Positions
    private Vector2 startPosition;
    private Vector2 checkpointPosition;
    private bool canSetCheckpoint = false;

    // References
    private Animator anim;
    private PlayerMovement player_movement;
    private Rigidbody2D body;

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
        body = GetComponent<Rigidbody2D>();
    }

    public void TakeDamage(float damage)
    {
        // Damage calculations
        currHP = Mathf.Clamp(currHP - damage, 0, startHP);

        if (currHP == 0)
        {
            Respawn();
        }
    }

    // Hitting an Obstacle or Checkpoint or Pillar
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Obstacles"))
        {
            TakeDamage(1); // Player hitting obstacle
        }
        else if (collision.CompareTag("Checkpoint"))
        {
            canSetCheckpoint = true; // Allow setting checkpoint
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Checkpoint"))
        {
            canSetCheckpoint = false; // Stop allowing setting checkpoint
        }
    }

    private void Update()
    {
        if (canSetCheckpoint && Input.GetKeyDown(KeyCode.F))
        {
            SetCheckpoint();
            
            if(currHP < 3)
            {
                currHP = 3;
            }
        }
    }

    void SetCheckpoint()
    {
        checkpointPosition = transform.position;
    }

    void Respawn()
    {
        StartCoroutine(DeathAnimation(respawn_timer));
    }
    private IEnumerator DeathAnimation(float wait_time)
    {
        player_movement.enabled = false; //stop player for moving
        body.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation; // freeze player position x and rotation

        anim.SetTrigger("defeat"); // play defeat animation

        yield return new WaitForSeconds(respawn_timer); // death animation delay

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

        anim.Play("Idle"); // play and set to default animation

    }

}


