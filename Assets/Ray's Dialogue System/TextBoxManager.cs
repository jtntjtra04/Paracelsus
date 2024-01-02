using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextBoxManager : MonoBehaviour
{
    public GameObject textBox;

    public Text theText;
    public TextAsset textFile;
    public string[] textLines;

    public int currentLine;
    public int endAtLine;

    public PlayerMovement player;

    void Start()
    {
        player = FindObjectOfType<PlayerMovement>();

        if(textFile != null)
        {
            textLines = (textFile.text.Split('\n'));

        
        }

        if(endAtLine == 0)
        {
            endAtLine = textLines.Length - 1;
        }
    }

    void Update()
    {
        theText.text = textLines[currentLine];


        if(Input.GetKeyDown(KeyCode.Return))
        {
            currentLine += 1;
        }
        
        if(currentLine > endAtLine)
        {
            textBox.SetActive(false); 
        }
    }



}
