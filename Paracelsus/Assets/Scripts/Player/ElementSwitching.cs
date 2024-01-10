using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

public class ElementSwitching : MonoBehaviour
{
    private int current_element;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SwitchElement(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SwitchElement(2);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SwitchElement(3);
        }
        else if(Input.GetKeyDown(KeyCode.Alpha4))
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

    public int GetCurrentElement()
    {
        return current_element;
    }
}
