using Ink.Runtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SearchService;

public class DialogueManager : MonoBehaviour
{

    [Header("Dialogue Ui")]

    [SerializeField] private float typingSpeed = 0.04f;

    [SerializeField] private GameObject continueIcon;

    [SerializeField] private GameObject dialoguePanel;

    [SerializeField] private TextMeshProUGUI dialogueText;

    [SerializeField] private TextMeshProUGUI displayNameText;

    [SerializeField] private Animator portraitAnimator;

    private Coroutine displayLineCoroutine;

    private Animator layoutAnimator;

    private Story currentStory;

    private bool canContinueToNextLine = false;

    private bool canSkip = false;

    private bool submitSkip;

    public bool dialogueIsPlaying { get; private set; }


    private static DialogueManager instance;

    private const string SPEAKER_TAG = "speaker";

    private const string PORTRAIT_TAG = "portrait";

    private const string LAYOUT_TAG = "layout";



    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogWarning("Found more than one Dialogue Manager in the scene");
        }
           instance = this;
    }

    public static DialogueManager GetInstance()
    {
        return instance;
    }

    private void Start()
    {
        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);

        layoutAnimator = dialoguePanel.GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            submitSkip = true;
        }

        if (!dialogueIsPlaying)
        {
            return;
        }

        // user left mouse click to proceed the dialogue
        if (canContinueToNextLine && Input.GetMouseButtonDown(0))
        {
            ContinueStory();
        }

    }

    public void EnterDialogueMode(TextAsset inkJSON)
    {
        currentStory = new Story(inkJSON.text);
        dialogueIsPlaying = true;
        dialoguePanel.SetActive(true);

        displayNameText.text = "???";
        portraitAnimator.Play("default");
        layoutAnimator.Play("blank");

        ContinueStory();

    }

    private IEnumerator CanSkip()
    {
        canSkip = false; //Making sure the variable is false.
        yield return new WaitForSeconds(0.05f);
        canSkip = true;
    }


    private IEnumerator ExitDialogueMode()
    {
        yield return new WaitForSeconds(0.2f);

        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);
        dialogueText.text = "";
    }

    private void ContinueStory()
    {
        if (currentStory.canContinue)
        {
            if (displayLineCoroutine != null)
            {
                StopCoroutine(displayLineCoroutine);
            }

            displayLineCoroutine =  StartCoroutine(DisplayLine(currentStory.Continue()));

            HandleTags(currentStory.currentTags);
        }
        else
        {
            StartCoroutine(ExitDialogueMode());
        }
    }

    private IEnumerator DisplayLine(string line)
    {
        dialogueText.text = "";
        continueIcon.SetActive(false); 

        submitSkip = false;
        canContinueToNextLine = false;
        
        StartCoroutine(CanSkip());

        foreach (char letter in line.ToCharArray())
        {
            if (canSkip && submitSkip)
            {
                submitSkip = false;
                dialogueText.text = line;
                break;
            }

            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        continueIcon.SetActive(true); 
        canContinueToNextLine = true;
        canSkip = false;
    }


    /* private IEnumerator DisplayLine(string line)
     {
         dialogueText.text = "";

         continueIcon.SetActive(false);

         submitSkip = false;
         canContinueToNextLine = false;

         StartCoroutine(CanSkip());

         bool isAddingRichTextTag = false;

         foreach (char letter in line.ToCharArray())
         {
             if (canSkip && submitSkip)
             {
                 submitSkip = false;
                 dialogueText.text = line;
                 break;
             }

             if (Input.GetMouseButtonDown(0))
             {
                 dialogueText.text = line; 
                 break;
             }

             if (letter == '<' || isAddingRichTextTag)
             {
                 isAddingRichTextTag = true;
                 dialogueText.text += letter;
                 if( letter == '>' )
                 {
                     isAddingRichTextTag = false;
                 }
             }
             else
             {
                 dialogueText.text += letter;
                 yield return new WaitForSeconds(typingSpeed);
             }

         }
         continueIcon.SetActive(true);
         canContinueToNextLine = true;
         canSkip = false;
     }
 */
    private void HandleTags(List<string> currentTags)
    {
        foreach (string tag in currentTags)
        {
            string[] splitTag = tag.Split(':');
            if(splitTag.Length != 2)
            {
                Debug.LogError("Tag could not be appropriately parsed: " + tag);
            }
            string tagKey = splitTag[0].Trim();
            string tagValue = splitTag[1].Trim();

            switch (tagKey)
            {
                case SPEAKER_TAG:
                    displayNameText.text = tagValue;
                    break;
                case PORTRAIT_TAG:
                    portraitAnimator.Play(tagValue);
                    break;
                case LAYOUT_TAG:
                    layoutAnimator.Play(tagValue);
                    break;
                default:
                    Debug.LogWarning("Tag came in but is not currently handled: " + tag);
                    break;


            }



        }
    }


}
