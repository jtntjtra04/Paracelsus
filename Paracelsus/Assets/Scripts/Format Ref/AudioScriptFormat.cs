using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioScriptFormat : MonoBehaviour
{
    //audio
    public AudioSource xAudio;
    public AudioClip x, y;


    void Update()
    {
        xAudio.clip = x;
        xAudio.Play(); 

        xAudio.clip = y;
        xAudio.Play(); 
    }
}

//silahkan ignore

//ini biar gw bisa copas script buat sound effects biar lebih gampang -jason