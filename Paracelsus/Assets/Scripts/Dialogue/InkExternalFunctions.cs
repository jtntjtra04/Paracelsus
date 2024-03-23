using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;

public class InkExternalFunctions 
{
    public void Bind(Story story, Animator backgroundAnimator, Animator FlashbackAnimator, Animator effectAnimator)
    {
          story.BindExternalFunction("Background", (string BgName) => Background(BgName, backgroundAnimator));
          story.BindExternalFunction("Music", (string MName) => ChangeMusic(MName));
          story.BindExternalFunction("Flashback", (string state) => Flashback(state, FlashbackAnimator));
          story.BindExternalFunction("SE", (string SEName) => PlaySE(SEName));
          story.BindExternalFunction("Effect", (string EName) => Effect(EName, effectAnimator));


    }

    public void Unbind(Story story)
    {
        story.UnbindExternalFunction("Background");
        story.UnbindExternalFunction("Music");
        story.UnbindExternalFunction("Flashback");
        story.UnbindExternalFunction("SE");
        story.UnbindExternalFunction("Effect");
    }



    public void Background(string BgName, Animator backgroundAnimator)
      {
        if( backgroundAnimator != null)
        {
          backgroundAnimator.Play(BgName);

        }
        else{
          Debug.LogWarning("Background Error lol");
        }
    
      }
    
    public void ChangeMusic(string MName)
    {
      if(MName == "stop")
      {
        AudioManager.instance.music_source.Stop();
      }
      else
      {
        AudioManager.instance.PlayMusic(MName);
      }
    }

     public void PlaySE(string SEName)
    {
     
        AudioManager.instance.PlaySFX(SEName);
      
    }

 



    public void Flashback(string state, Animator FlashbackAnimator)
    {
      FlashbackAnimator.Play(state);
    }

     public void Effect(string state, Animator effectAnimator)
    {
      effectAnimator.Play(state);
    }

}

