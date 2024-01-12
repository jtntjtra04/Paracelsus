using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

public class ElementSwitching : MonoBehaviour
{
    private int current_element;

    //pillars
    private bool fire_pillar = false; 
    private bool wind_pillar = false;
    private bool earth_pillar = false;
    private bool water_pillar = false;

    // fire element
    private bool fire_element = false;
    private float fire_timer = 0f;
    private float fire_duration = 60f;

    //wind element
    private bool wind_element = false;
    private float wind_timer = 0f;
    private float wind_duration = 60f;

    //earth element
    private bool earth_element = false;
    private float earth_timer = 0f;
    private float earth_duration = 60f;

    //water element
    private bool water_element = false;
    private float water_timer = 0f;
    private float water_duration = 60f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("FirePillar"))
        {
            fire_pillar = true; // Player interact with fire pillar
        }
        else if (collision.CompareTag("WindPillar"))
        {
            wind_pillar = true; // Player interact with wind pillar
        }
        else if (collision.CompareTag("EarthPillar"))
        {
            earth_pillar = true; // Player interact with earth pillar
        }
        else if (collision.CompareTag("WaterPillar"))
        {
            water_pillar = true; // Player interact with water pillar
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("FirePillar"))
        {
            fire_pillar = false; // Player not interact with fire pillar anymore
        }
        else if (collision.CompareTag("WindPillar"))
        {
            wind_pillar = false; // Player not interact with wind pillar anymore
        }
        else if (collision.CompareTag("EarthPillar"))
        {
            earth_pillar = false; // Player not interact with earth pillar anymore
        }
        else if (collision.CompareTag("WaterPillar"))
        {
            water_pillar = false; // Player not interact with water pillar anymore
        }
    }
    private void Update()
    {
        if (fire_pillar && Input.GetKeyDown(KeyCode.F)) // if interact with fire pillar
        {
            UnlockFire(); // enable fire element
        }
        else if (wind_pillar && Input.GetKeyDown(KeyCode.F)) // if interact with wind pillar
        {
            UnlockWind(); // enable wind element
        }
        else if (earth_pillar && Input.GetKeyDown(KeyCode.F)) // if interact with earth pillar
        {
            UnlockEarth(); // enable earth element
        }
        else if (water_pillar && Input.GetKeyDown(KeyCode.F)) // if interact with water pillar
        {
            UnlockWater(); // enable water element
        }

        if (fire_element) // check if element is active
        {
            fire_timer -= Time.deltaTime; // reduce the timer

            if (fire_timer <= 0)
            {
                SwitchElement(5); // switch back to neutral element
                fire_element = false; // set to false again so it doesn't infinite loop
            }
        }
        if (wind_element)
        {
            wind_timer -= Time.deltaTime;

            if(wind_timer <= 0)
            {
                SwitchElement(5);
                wind_element = false;
            }
        }
        if (earth_element)
        {
            earth_timer -= Time.deltaTime;

            if (earth_timer <= 0)
            {
                SwitchElement(5);
                earth_element = false;
            }    
        }
        if (water_element)
        {
            water_timer -= Time.deltaTime;

            if(water_timer <= 0)
            {
                SwitchElement(5);
                water_element = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha1) && fire_element) // element switcher from input and check if element active
        {
            SwitchElement(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && wind_element)
        {
            SwitchElement(2);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3) && earth_element)
        {
            SwitchElement(3);
        }
        else if(Input.GetKeyDown(KeyCode.Alpha4) && water_element)
        {
            SwitchElement(4);
        }
        else if(Input.GetKeyDown(KeyCode.Alpha5))
        {
            SwitchElement(5);
        }
    }
    private void SwitchElement(int current_index)
    {
        Debug.Log("Switch Element to Index : " + current_index);
        current_element = current_index;
    }
    public void UnlockFire() // unlock fire element
    {
        fire_element = true; // set element to active
        fire_timer = fire_duration; // store the duration to reduce 
        Debug.Log("Fire Element Unlocked!"); // print debugging
    }
    public void UnlockWind() // unlock wind element
    {
        wind_element = true;
        wind_timer = wind_duration;
        Debug.Log("Wind Element Unlocked!");
    }
    public void UnlockWater() // unlock water element
    {
        water_element = true;
        water_timer = water_duration;
        Debug.Log("Water Element Unlocked!");
    }
    public void UnlockEarth() // unlock earth element
    {
        earth_element = true;
        earth_timer = earth_duration;
        Debug.Log("Earth Element Unlocked!");
    }
    public int GetCurrentElement()
    {
        return current_element;
    }
}
