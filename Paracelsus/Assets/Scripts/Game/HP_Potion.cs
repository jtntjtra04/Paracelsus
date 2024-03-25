using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HP_Potion : MonoBehaviour
{
    private float maxPot = 5f;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameController gameController = GameObject.FindFirstObjectByType<GameController>();

            if (gameController.hpPotion < maxPot)
            {
                gameController.hpPotion++;
                Destroy(gameObject);
                AudioManager.instance.PlaySFX("PotionGet");
                Debug.Log("PotionGet");
            }
            else
            {
                Debug.Log("Player already has the maximum number of potions!");
            }
        }
    }
}
