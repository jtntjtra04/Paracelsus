using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private void Start()
    {
        /*AudioManager.instance.music_source.Stop();
        AudioManager.instance.PlayMusic("TitleMusic");*/
    }
    public void PlayGame()
    {
       if(pausetransition.ComefromPause == false)
       {
        MainMenuAudioManager.instance.PlaySFX("ClickSound");
        MainMenuAudioManager.instance.music_source.Stop();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
       }else if (pausetransition.ComefromPause == true) 
       {
        MainMenuAudioManager.instance.PlaySFX("ClickSound");
        MainMenuAudioManager.instance.music_source.Stop();
        SceneManager.LoadScene("GameScene");
       }
        
    }
    public void GoToSettingsMenu()
    {
        MainMenuAudioManager.instance.PlaySFX("ClickSound");
        SceneManager.LoadScene("SettingsMenu");
    }
    public void GoToMainMenu()
    {
        MainMenuAudioManager.instance.PlaySFX("ClickSound");
        SceneManager.LoadScene("MainMenu");
    }
    public void ExitGame()
    {
        MainMenuAudioManager.instance.PlaySFX("ClickSound");
        Application.Quit();
    }
}
