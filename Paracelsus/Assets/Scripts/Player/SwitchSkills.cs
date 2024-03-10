using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SwitchSkills : MonoBehaviour
{

    public GameObject barrierPrefab;
    public GameObject barrierPrefabInstance;
    public GameObject tornadoPrefab;
    public GameObject tornadoPrefabInstance;
    public GameObject pillarPrefab;
    public GameObject pillarPrefabInstance;
    public GameObject pelletPrefab;
    public GameObject pelletPrefabInstance;

    public float pillarSpeed = 1000f;
    public GameObject Enemy;
    private int current_element = 0;
    public bool isBarrierActive = false;
    public float barrierTime = 5f;
    public float tornadoSpeed = 100f;
    public float tornadoTime = 1f;
    public bool earthFunc = false;
    [SerializeField] private Transform firepoint;
    public int amountOfBullets = 7;
    public float pelletSpeed = 100f;
    public float range = 10f;
    public float pelletTime = 0.5f;
    public SpriteRenderer pillarFlip;
    public float pillarDistance = 5f;
  
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
        }else if(current_element == 4)
        {
            if (Input.GetMouseButtonDown(1))
            {
                CastPillar();

            }
        }else if(current_element == 3)
        {
            if (Input.GetMouseButtonDown(1))
            {
                CastShotgun();

            }
            if (pillarPrefabInstance != null)
            {
                UpdatePillarDirection();
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

    private void CastPillar()
    {
        pillarFlip.flipX = false;
        earthFunc = true;
        GameObject FireEnemy = GameObject.FindWithTag("FireEnemy");
        Vector2 EnemyScale = FireEnemy.transform.localScale;

        // Vector2 pillarprefabInstance.localScale = (EnemyScale < 0) ? -transform.right : transform.right;

        if(pillarPrefabInstance == null)
        {
            Vector2 enemyPosition = FireEnemy.transform.position - FireEnemy.transform.forward * pillarDistance;
            pillarPrefabInstance = Instantiate(pillarPrefab, enemyPosition, Quaternion.identity);
            Destroy(pillarPrefabInstance, 1f);
            Rigidbody2D enemyRb= FireEnemy.GetComponent<Rigidbody2D>();
            enemyRb.AddForce(FireEnemy.transform.forward * pillarSpeed);
           
        
        }

       
    }

    private void UpdatePillarDirection()
    {
        GameObject FireEnemy = GameObject.FindWithTag("FireEnemy");
        Vector2 EnemyScale = FireEnemy.transform.localScale;
        if(FireEnemy.transform.localScale.x < 0)
        {
            Debug.Log(EnemyScale);
            pillarFlip.flipX = true;
        }
        else
        {
            Debug.Log(EnemyScale);
            pillarFlip.flipX = false;
        }
    }
   

    private void CastShotgun()
    {
        for(int i = 0; i < amountOfBullets; i++)
        {
            float localScaleX = transform.localScale.x;
           
            transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
            pelletPrefabInstance = Instantiate(pelletPrefab, firepoint.position, Quaternion.identity);
            Rigidbody2D pelletRb = pelletPrefabInstance.GetComponent<Rigidbody2D>();

            float playerScaleX = transform.localScale.x;
            
            Vector2 playerDirection = (playerScaleX < 0) ? -transform.right : transform.right;
            Vector2 dir = transform.rotation * playerDirection;
            Vector2 pdir = Vector2.Perpendicular(dir) * Random.Range(-range, range);
            pelletRb.velocity = (dir + pdir) * pelletSpeed;

            Destroy(pelletPrefabInstance, pelletTime);
        }
    }
}
