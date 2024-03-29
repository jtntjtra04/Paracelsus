using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlideTuto : MonoBehaviour
{
     [Header("Ink JSON")]
    [SerializeField] private TextAsset inkJSON;

    [Header("Background Animator")]
    [SerializeField] private Animator backgroundAnimator;

     [SerializeField] private Animator flashbackAnimator;
     [SerializeField] private Animator effectAnimator;

     public GameController Gli;

     private bool Done = false;




    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player" && Gli.glide == true && Done == false)
        {
            DialogueManager.GetInstance().EnterDialogueMode(inkJSON, backgroundAnimator, flashbackAnimator,effectAnimator);
            Done = true;
        }
    }

}
