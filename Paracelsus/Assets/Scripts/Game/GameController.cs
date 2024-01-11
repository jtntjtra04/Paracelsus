using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    // HP
    [SerializeField] private float startHP;
    public float currHP { get; private set; }

    // Spawn Positions
    private Vector2 startPosition;
    private Vector2 checkpointPosition;
    private bool canSetCheckpoint = false;

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
        if (checkpointPosition != Vector2.zero) // Check if a checkpoint is set
        {
            transform.position = checkpointPosition; // Respawn at the checkpoint
        }
        else
        {
            transform.position = startPosition; // Respawn at the starting position if no checkpoint
        }
        currHP = startHP;
    }
}


