using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DialogueTestting : MonoBehaviour
{

     [Header("Background Animator")]
    [SerializeField] private Animator backgroundAnimator;

    [SerializeField] private Animator FlashbackAnimator;
    [SerializeField] private Animator effectAnimator;


    [SerializeField] private TextAsset waittest1;

  
    [SerializeField] private TextAsset waittest2;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
             StartCoroutine(WaitaBit());
        
        }

    }
   IEnumerator WaitaBit()
   {
        DialogueManager.CutscenePlay = true;

       DialogueManager.GetInstance().EnterDialogueMode(waittest1, backgroundAnimator,FlashbackAnimator,effectAnimator);

        yield return new WaitForSeconds(5);

        DialogueManager.GetInstance().EnterDialogueMode(waittest2, backgroundAnimator,FlashbackAnimator,effectAnimator);

        DialogueManager.CutscenePlay = false;

    }



}
