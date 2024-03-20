using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrentElement : MonoBehaviour
{
    
    public Image[] Icons;
    public Sprite[] OriginalIcons;
    public Sprite[] ElementIcons;

    private int currentElement = 0; // Assuming 0 is the default element
    private int previousElement = -1; // Initialize to an invalid element index

    [SerializeField] private ElementSwitching Element;

    void Update()
    {
        if (Element != null)
        {
            currentElement = Element.GetCurrentElement();
        }

        // Reset the previous element's icon
        if (previousElement != -1 && previousElement != currentElement - 1)
        {
            Icons[previousElement].sprite = OriginalIcons[previousElement];
        }

        // Set the current element's icon
        if (currentElement > 0 && currentElement <= ElementIcons.Length)
        {
            Icons[currentElement - 1].sprite = ElementIcons[currentElement - 1];
            previousElement = currentElement - 1;
        }
    }  
}
