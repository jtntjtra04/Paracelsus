using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerOnTouch : MonoBehaviour
{

    [Header("Ink JSON")]
    [SerializeField] private TextAsset inkJSON;

    [Header("Background Animator")]
    [SerializeField] private Animator backgroundAnimator;

     [SerializeField] private Animator flashbackAnimator;
     [SerializeField] private Animator effectAnimator;




    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            DialogueManager.GetInstance().EnterDialogueMode(inkJSON, backgroundAnimator, flashbackAnimator,effectAnimator);
        }
    }


}