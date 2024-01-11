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
    private bool fire_pillar = false;
    private bool wind_pillar = false;
    private bool earth_pillar = false;
    private bool water_pillar = false;

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
            TakeDamage(1);
        }
        else if (collision.CompareTag("Checkpoint"))
        {
            canSetCheckpoint = true; // Allow setting checkpoint
        }
        else if (collision.CompareTag("FirePillar"))
        {
            fire_pillar = true; // Player interact with fire pillar
        }
        else if (collision.CompareTag("WindPillar"))
        {
            wind_pillar = true; // Player interact with wind pillar
        }
        else if (collision.CompareTag("EarthPillar"))
        {
            earth_pillar = true; // Player interact with earth pillar
        }
        else if (collision.CompareTag("WaterPillar"))
        {
            water_pillar = true; // Player interact with water pillar
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Checkpoint"))
        {
            canSetCheckpoint = false; // Stop allowing setting checkpoint
        }
        else if (collision.CompareTag("FirePillar"))
        {
            fire_pillar = false; // Player not interact with fire pillar anymore
        }
        else if (collision.CompareTag("WindPillar"))
        {
            wind_pillar = false; // Player not interact with wind pillar anymore
        }
        else if (collision.CompareTag("EarthPillar"))
        {
            earth_pillar = false; // Player not interact with earth pillar anymore
        }
        else if (collision.CompareTag("WaterPillar"))
        {
            water_pillar = false; // Player not interact with water pillar anymore
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
        else if (fire_pillar && Input.GetKeyDown(KeyCode.F))
        {
            
        }
        else if (wind_pillar && Input.GetKeyDown(KeyCode.F))
        {

        }
        else if (earth_pillar && Input.GetKeyDown(KeyCode.F))
        {

        }
        else if (water_pillar && Input.GetKeyDown(KeyCode.F))
        {

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


