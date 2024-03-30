using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicReplay1 : MonoBehaviour
{
    public void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player" )
        {
            AudioManager.instance.ChangeMusic("Theme");
        }
    }
  
}
