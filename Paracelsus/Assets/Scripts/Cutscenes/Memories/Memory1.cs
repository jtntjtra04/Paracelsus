using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//DialogueManager.GetInstance().EnterDialogueMode(scene1, backgroundAnimator);
//StartCoroutine(Flow());
//yield return new WaitForSeconds(5);
//FadeBox.Play("FadeOut");
//FadeBox.Play("FadeIn");
public class Memory1 : MonoBehaviour
{

    [Header("Background Animator")]
    [SerializeField] private Animator backgroundAnimator;

     [SerializeField] private GameObject visualCue;

    [SerializeField] private Animator flashbackAnimator;
    [SerializeField] private Animator effectAnimator;

    [SerializeField] private Animator FadeBox;

    [SerializeField] private TextAsset scene1;




private bool playerInRange;

    private void Awake()
    {
        playerInRange = false;
        visualCue.SetActive(false);
    }

    private void Update()
    {
        if (playerInRange && !DialogueManager.GetInstance().dialogueIsPlaying && !DialogueManager.CutscenePlay)
        {
            visualCue.SetActive(true);
            if (Input.GetKeyDown(KeyCode.F))
            {
                StartCoroutine(Flow());
            }
        }
        else
        {
            visualCue.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            playerInRange = false;
        }
    }


    IEnumerator Flow()
   {
        DialogueManager.CutscenePlay = true;
        AudioManager.instance.music_source.Stop();
        effectAnimator.Play("redflash");
        AudioManager.instance.PlaySFX("activate");
        yield return new WaitForSeconds(3);
        AudioManager.instance.PlaySFX("memory");
        effectAnimator.Play("memoryflash");
        yield return new WaitForSeconds(7);
        effectAnimator.Play("default");
        FadeBox.Play("defaultfadeout");
        yield return new WaitForSeconds(2);
        FadeBox.Play("FadeIn");
        flashbackAnimator.Play("Double");
        backgroundAnimator.Play("cityoutside");
        yield return new WaitForSeconds(5);
        DialogueManager.GetInstance().EnterDialogueMode(scene1, backgroundAnimator, flashbackAnimator, effectAnimator);
   
       
        DialogueManager.CutscenePlay = false;

    }



}
