using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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

    private bool GotoGame;


    private void Start()
    {
        AudioManager.instance.music_source.Stop();
        flashbackAnimator.Play("Double");
        backgroundAnimator.Play("burn");
        StartCoroutine(Flow());
    }

    private void Update()
    {
        if(GotoGame == true && !DialogueManager.GetInstance().dialogueIsPlaying)
        {
            StartCoroutine(GameGo());
        }

    }



    IEnumerator Flow()
   {
        DialogueManager.CutscenePlay = true;

        yield return new WaitForSeconds(7);
        AudioManager.instance.PlayMusic("fire");
        FadeBox.Play("FadeIn");
        yield return new WaitForSeconds(3);
        DialogueManager.GetInstance().EnterDialogueMode(scene1, backgroundAnimator, flashbackAnimator, effectAnimator);
        GotoGame = true;

   
       
        DialogueManager.CutscenePlay = false;

    }

    IEnumerator GameGo()
    {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("GameScene");

    }



}
