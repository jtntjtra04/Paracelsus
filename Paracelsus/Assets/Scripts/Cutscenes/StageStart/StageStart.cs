using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
//DialogueManager.GetInstance().EnterDialogueMode(scene1, backgroundAnimator);
//StartCoroutine(Flow());
//yield return new WaitForSeconds(5);
//FadeBox.Play("FadeOut");
//FadeBox.Play("FadeIn");
public class StageOpening : MonoBehaviour
{


    [SerializeField] private Animator backgroundAnimator;

    [SerializeField] private Animator flashbackAnimator;
    [SerializeField] private Animator effectAnimator;

    [SerializeField] private Animator FadeBox;

    [SerializeField] private TextAsset stage1;


    private void Start()
    {
       AudioManager.instance.music_source.Stop();
        StartCoroutine(StageStart());
    }



    IEnumerator StageStart()
   {
        DialogueManager.CutscenePlay = true;
       

        yield return new WaitForSeconds(2);
        AudioManager.instance.PlayMusic("Theme");
        FadeBox.Play("FadeIn");
        yield return new WaitForSeconds(2);
        DialogueManager.GetInstance().EnterDialogueMode(stage1, backgroundAnimator, flashbackAnimator, effectAnimator);
       


        DialogueManager.CutscenePlay = false;

    }



}
