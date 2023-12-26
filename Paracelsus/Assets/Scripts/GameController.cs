using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private float startHP;
    Vector2 startPosition;
    public float currHP {  get; private set; }

    private void Start()
    {
        // Respawn point
        startPosition = transform.position;
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
    }

    // Hitting an Obstacle
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Obstacles"))
        {
            TakeDamage(1);
        }

        if (currHP == 0)
        {
            Respawn();
        }
    }
    void Respawn()
    {
        transform.position = startPosition;
        currHP = startHP;
    }
}
