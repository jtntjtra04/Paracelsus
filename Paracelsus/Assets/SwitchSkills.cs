using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchSkills : MonoBehaviour
{

    public GameObject barrierPrefab;
    public GameObject barrierPrefabInstance;
    public GameObject tornadoPrefab;
    public GameObject tornadoPrefabInstance;
    private int current_element = 0;
    public bool isBarrierActive = false;
    public float barrierTime = 5f;
    public float tornadoSpeed = 100f;
    public float tornadoTime = 1f;
    [SerializeField] private Transform firepoint;
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
        }else if(current_element == 1)
        {
           if(Input.GetMouseButtonDown(1))
            {
                CastTornado();
                
            } 
        }
        
         //Debug.Log(" " + IsBarrierActive());
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
            Destroy(barrierPrefabInstance, barrierTime);
            
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

    
    private void CastTornado()
    {
        if(tornadoPrefabInstance == null)
        {
            tornadoPrefabInstance = Instantiate(tornadoPrefab, firepoint.position, Quaternion.identity);
            Rigidbody2D tornadoRigidbody = tornadoPrefabInstance.GetComponent<Rigidbody2D>();

             

        float playerScaleX = transform.localScale.x;
        Vector2 playerDirection = (playerScaleX < 0) ? -transform.right : transform.right; // Assuming the tornado should move to the right relative to the player's facing direction


        // Apply force to the tornado in the calculated direction
            tornadoRigidbody.AddForce(playerDirection * tornadoSpeed);

            // Destroy the tornado after 1 second
            Destroy(tornadoPrefabInstance, tornadoTime);
            
        }
       

       

    }
}
