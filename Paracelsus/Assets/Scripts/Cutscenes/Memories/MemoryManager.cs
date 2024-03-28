using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//DialogueManager.GetInstance().EnterDialogueMode(scene1, backgroundAnimator);
//StartCoroutine(Flow());
//yield return new WaitForSeconds(5);
//FadeBox.Play("FadeOut");
//FadeBox.Play("FadeIn");
public class MemoryManager : MonoBehaviour
{

    [Header("Background Animator")]
    [SerializeField] private Animator backgroundAnimator;

     [SerializeField] private GameObject visualCue;

    [SerializeField] private Animator flashbackAnimator;
    [SerializeField] private Animator effectAnimator;

    [SerializeField] private Animator FadeBox;

    [SerializeField] private TextAsset memory1;
    [SerializeField] private TextAsset memory2;
        [SerializeField] private TextAsset activated;


   // public bool mem1 = true;
    //public bool mem2,allcompleted = false;

    public GameController me1,me2,done;

    public GameObject shard;
    




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
                if(gameObject.tag =="MemoryDone")
                {
                DialogueManager.GetInstance().EnterDialogueMode(activated, backgroundAnimator, flashbackAnimator, effectAnimator);
                }
                else if(me1.mem1 == true)
                {
                  StartCoroutine(Mem1());
                  me1.mem1= false;
                  me2.mem2 = true;
                  gameObject.tag="MemoryDone";
                  
                }
                else if(me2.mem2 == true)
                {
                    StartCoroutine(Mem2());
                    me2.mem2 = false;
                    done.allcompleted = true;
                    gameObject.tag="MemoryDone";
                    
                }
               
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


    IEnumerator Mem1()
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
        DialogueManager.GetInstance().EnterDialogueMode(memory1, backgroundAnimator, flashbackAnimator, effectAnimator);
   
       
        DialogueManager.CutscenePlay = false;

    }

     IEnumerator Mem2()
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
        backgroundAnimator.Play("home");
        yield return new WaitForSeconds(5);
        DialogueManager.GetInstance().EnterDialogueMode(memory2, backgroundAnimator, flashbackAnimator, effectAnimator);
   
       
        DialogueManager.CutscenePlay = false;

    }



}
