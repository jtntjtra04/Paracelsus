using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//DialogueManager.GetInstance().EnterDialogueMode(scene1, backgroundAnimator);
//StartCoroutine(Flow());
//yield return new WaitForSeconds(5);
//FadeBox.Play("FadeOut");
//FadeBox.Play("FadeIn");
public class Prologue : MonoBehaviour
{

    [Header("Background Animator")]
    [SerializeField] private Animator backgroundAnimator;

    [SerializeField] private Animator flashbackAnimator;
    [SerializeField] private Animator effectAnimator;

    [SerializeField] private Animator FadeBox;

    [SerializeField] private TextAsset scene1;


    private void Start()
    {
        AudioManager.instance.music_source.Stop();
        flashbackAnimator.Play("Double");
        backgroundAnimator.Play("burn");
        StartCoroutine(Flow());
    }



    IEnumerator Flow()
   {
        DialogueManager.CutscenePlay = true;

        yield return new WaitForSeconds(7);
        AudioManager.instance.PlayMusic("fire");
        FadeBox.Play("FadeIn");
        yield return new WaitForSeconds(3);
        DialogueManager.GetInstance().EnterDialogueMode(scene1, backgroundAnimator, flashbackAnimator, effectAnimator);
   
       



       


        DialogueManager.CutscenePlay = false;

    }



}
