using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicReplay1 : MonoBehaviour
{
    public AudioSource musicsource;
     public void OnTriggExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player" )
        {
            GameController.floor2_music = true;
        }
    }
  
}
