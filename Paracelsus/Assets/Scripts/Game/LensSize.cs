using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class LensSize : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera;
    public float newSizeOnTouch;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            virtualCamera.m_Lens.OrthographicSize = newSizeOnTouch;
            Debug.Log("Triggered");
        }
    }
}
