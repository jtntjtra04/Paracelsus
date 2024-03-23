using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//DialogueManager.GetInstance().EnterDialogueMode(scene1, backgroundAnimator);
//StartCoroutine(Flow());
//yield return new WaitForSeconds(5);
//FadeBox.Play("FadeOut");
//FadeBox.Play("FadeIn");
public class Template : MonoBehaviour
{

    [Header("Background Animator")]
    [SerializeField] private Animator backgroundAnimator;

    [SerializeField] private Animator FadeBox;

    [SerializeField] private TextAsset scene1;

    private void Start()
    {
        StartCoroutine(Flow());
    }



    IEnumerator Flow()
   {
        DialogueManager.CutscenePlay = true;

        yield return new WaitForSeconds(5);
       


        DialogueManager.CutscenePlay = false;

    }



}
