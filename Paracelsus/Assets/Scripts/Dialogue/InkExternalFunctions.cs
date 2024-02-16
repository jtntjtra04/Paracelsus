using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;

public class InkExternalFunctions
{
    public void Bind(Story story, Animator flashScreen)
    {
        story.BindExternalFunction("delay", (int seconds) =>
        {
            Debug.Log(seconds);
        });
    }

    public void Unbind(Story story)
    {
        
    }
}

