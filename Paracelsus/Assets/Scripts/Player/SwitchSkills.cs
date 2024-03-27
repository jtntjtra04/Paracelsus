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
    public Transform pillarScale;
    

    public float pillarSpeed = 1000f;
    public GameObject Enemy;
    private int current_element = 0;
    public bool isBarrierActive = false;
    public float barrierTime = 5f;
    public float tornadoSpeed = 100f;
    public float tornadoTime = 1f;
    public bool earthFunc = false;
    [SerializeField] private Transform firepoint;
    [SerializeField] private Transform pillarFirepoint;
    public int amountOfBullets = 7;
    public float pelletSpeed = 100f;
    public float range = 10f;
    public float pelletTime = 0.5f;
    public SpriteRenderer pillarFlip;
    public float pillarDistance = 5f;
    private float windDuration = 5f;
    private float windTimer = 0f;
    private float fireDuration = 8f;
    private float fireTimer = 0f;
    private float waterDuration = 10f;
    private float waterTimer = 0f;
    private float earthDuration = 8f;
    private float earthTimer = 0f;
    private float pillarChargeUp = 0f;
    public bool waterReady =true;
    public bool fireReady = true;
    public bool windReady = true;
    public bool earthReady = true;
    public bool isCharging;
    private float chargeTimer = 0f;

    public Animator pillarAnimator;
    private bool isAnimationFrozen = false;

    
  
    void Update()
    {
        ElementSwitching switch_element = GetComponent<ElementSwitching>();

        if (switch_element != null)
        {
            current_element = switch_element.GetCurrentElement();
        }
        if(current_element == 2)
        {
            
            if(Input.GetMouseButtonDown(1) && waterReady)
            {
                CastBarrier();
                waterReady = false;
                
            }
            
        }else if(current_element == 1)
        {
           
           if(Input.GetMouseButtonDown(1) && windReady)
            {
                CastTornado();
                windReady = false;
                
            } 
        }else if(current_element == 4)
        {
            
            if (Input.GetMouseButtonDown(1) && earthReady)
            {
                isCharging = true;
                chargeTimer = 0f;
                CastPillar();
            }
            if(Input.GetMouseButton(1))
            {
                if(isCharging)
                {
                    chargeTimer += Time.deltaTime;
                    chargeTimer = Mathf.Clamp(chargeTimer, 0f, 3f);
                }
            }
            if(Input.GetMouseButtonUp(1))
            {
                if(isCharging)
                {
                    if(chargeTimer >= 3f)
                    {
                        isCharging = false;
                        LaunchPillar();
                        pillarAnimator.SetTrigger("Launched");
                        
                    }
                    else
                    {
                        Destroy(pillarPrefabInstance);
                        AudioManager.instance.sfx_source.Stop();
                    }
                }
                isCharging = false;
                chargeTimer = 0f;
                
                
                
            }
            
            

        }
        else if(current_element == 3)
        {
           if (Input.GetMouseButtonDown(1) && fireReady)
            {
                CastShotgun();
                fireReady = false;

            }
           
        }

        if(pillarPrefabInstance != null)
        {
            UpdateEarthPillarPosition();
        }
        if (barrierPrefabInstance != null)
        {
            UpdateWaterBarrierPosition();
        }

        //Cooldown Water Skill
        if (!waterReady)
            {
                waterTimer += Time.deltaTime;

            }
            if (waterTimer >= waterDuration)
            {
                waterReady = true;
                waterTimer = 0.0f;
            }
        //Cooldown Wind Skill
         if (!windReady)
            {
                windTimer += Time.deltaTime;

            }
            if (windTimer >= windDuration)
            {
                windReady = true;
                windTimer = 0.0f;
            }
        //Cooldown Earth Skill
         if (!earthReady)
            {
                earthTimer += Time.deltaTime;

            }
            if (earthTimer >= earthDuration)
            {
                earthReady = true;
                earthTimer = 0.0f;
            }
        //Cooldown Fire Skill
         if (!fireReady)
            {
                fireTimer += Time.deltaTime;

            }
            if (fireTimer >= fireDuration)
            {
                fireReady = true;
                fireTimer = 0.0f;
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
            AudioManager.instance.PlaySFX("WaterSkill");
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
            AudioManager.instance.PlaySFX("WindSkill");

             

        float playerScaleX = transform.localScale.x;
        Vector2 playerDirection = (playerScaleX < 0) ? -transform.right : transform.right; // Assuming the tornado should move to the right relative to the player's facing direction


        // Apply force to the tornado in the calculated direction
            tornadoRigidbody.AddForce(playerDirection * tornadoSpeed);

            // Destroy the tornado after 1 second
            Destroy(tornadoPrefabInstance, tornadoTime);
            
        }
       

 

    }

    public void CastPillar()
    {
        AudioManager.instance.PlaySFX("EarthCharge");
        if (pillarPrefabInstance == null)
        {
            pillarPrefabInstance = Instantiate(pillarPrefab, pillarFirepoint.position, Quaternion.identity);
             float playerScaleX = transform.localScale.x;
            Vector2 playerDirection = (playerScaleX < 0) ? -transform.right : transform.right; // Assuming the tornado should move to the right relative to the player's facing direction

            pillarPrefabInstance.transform.localScale = new Vector3(playerScaleX, transform.localScale.y, transform.localScale.z);
         
            
        }
       
    }

    private void LaunchPillar()
    {
        AudioManager.instance.sfx_source.loop = false;
        AudioManager.instance.PlaySFX("EarthLaunch");
        Rigidbody2D pillarRigidbody = pillarPrefabInstance.GetComponent<Rigidbody2D>();
        

        float playerScaleX = transform.localScale.x;
        Vector2 playerDirection = (playerScaleX < 0) ? -transform.right : transform.right; // Assuming the tornado should move to the right relative to the player's facing direction

        pillarPrefabInstance.transform.localScale = new Vector3(playerScaleX, transform.localScale.y, transform.localScale.z);

        // Apply force to the tornado in the calculated direction
            pillarRigidbody.AddForce(playerDirection * tornadoSpeed);
        earthReady = false;
        // Destroy the tornado after 1 second
        Destroy(pillarPrefabInstance, 5f);
    }

    public void UpdateEarthPillarPosition()
    {
        if (isCharging)
        {
            pillarPrefabInstance.transform.position = pillarFirepoint.position;
            float playerScaleX = transform.localScale.x;
            Vector2 playerDirection = (playerScaleX < 0) ? -transform.right : transform.right; // Assuming the tornado should move to the right relative to the player's facing direction

            pillarPrefabInstance.transform.localScale = new Vector3(playerScaleX, transform.localScale.y, transform.localScale.z);
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
            AudioManager.instance.PlaySFX("FireSkill");

            float playerScaleX = transform.localScale.x;
            
            Vector2 playerDirection = (playerScaleX < 0) ? -transform.right : transform.right;
            Vector2 dir = transform.rotation * playerDirection;
            Vector2 pdir = Vector2.Perpendicular(dir) * Random.Range(-range, range);
            pelletRb.velocity = (dir + pdir) * pelletSpeed;

            Destroy(pelletPrefabInstance, pelletTime);
        }
    }
}
