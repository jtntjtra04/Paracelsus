using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchSkills : MonoBehaviour
{

    public GameObject barrierPrefab;
    public GameObject barrierPrefabInstance;
    private int current_element = 0;
    public bool isBarrierActive = false;
    private float barrierTime = 0f;
    void Update()
    {
        ElementSwitching switch_element = GetComponent<ElementSwitching>();

        if (switch_element != null)
        {
            current_element = switch_element.GetCurrentElement();
        }
        if(current_element == 2)
        {
            if(Input.GetMouseButtonDown(1))
            {
                CastBarrier();
                
            }
            if (barrierPrefabInstance != null)
            {
                UpdateWaterBarrierPosition();
            }
        }
        
     //    Debug.Log(" " + IsBarrierActive());
    }

    private void CastBarrier()
    {
         if (barrierPrefabInstance == null)
        {
            // Instantiate water barrier prefab
            barrierPrefabInstance = Instantiate(barrierPrefab, transform.position, Quaternion.identity);
            barrierTime += Time.deltaTime;
            // Adjust the position so it's in front of the player
            Vector3 offset = new Vector3(0f, 0f, 1f);
            barrierPrefabInstance.transform.position += offset;

            isBarrierActive = true;
            // Destroy the water barrier after 5 seconds
            Destroy(barrierPrefabInstance, 5f);
            
            if(barrierTime == 5f)
            {
                isBarrierActive = false;
            }
        }
    }

     private void UpdateWaterBarrierPosition()
    {
        // Update the position of the water barrier to follow the player
        Vector3 offset = new Vector3(0f, 0f, 1f);
        barrierPrefabInstance.transform.position = transform.position + offset;
    }

    public bool IsBarrierActive()
    {
         // Check if the barrier prefab instance exists and the barrier time is less than 5 seconds
    if (barrierPrefabInstance != null && barrierTime < 5f)
    {
        return true;
    }

    // Otherwise, return false
    return false;
    }
}
