using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Suspend : MonoBehaviour
{
    public float suspensionForce = 10000f; // Adjust as needed

    private void OnTriggerEnter2D(Collider2D collision)
    {
       if (collision.CompareTag("FireEnemy"))
        {
          Debug.Log("enemy hit");
            Rigidbody2D enemyRigidbody = collision.GetComponent<Rigidbody2D>();

            if (enemyRigidbody != null)
            {
               enemyRigidbody.AddForce(Vector2.up * suspensionForce, ForceMode2D.Impulse);
            }
        }
    }

    
  
  
}
