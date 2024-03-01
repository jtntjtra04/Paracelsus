using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTestting : MonoBehaviour
{

    [Header("waittest1")]
    [SerializeField] private TextAsset waittest1;

    [Header("waittest2")]
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
       DialogueManager.GetInstance().EnterDialogueMode(waittest1);

        
        yield return new WaitForSeconds(5);
        

        DialogueManager.GetInstance().EnterDialogueMode(waittest2);
        
    }



}
