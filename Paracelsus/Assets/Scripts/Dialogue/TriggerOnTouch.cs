using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerOnTouch : MonoBehaviour
{

    [Header("Ink JSON")]
    [SerializeField] private TextAsset inkJSON;


    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            DialogueManager.GetInstance().EnterDialogueMode(inkJSON);
        }
    }


}