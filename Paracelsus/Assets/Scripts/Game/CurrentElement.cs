using UnityEngine; 
using UnityEngine.UI; 
public class CurrentElement : MonoBehaviour 
{ 
    public Image[] Icons; 
    public Sprite[] OriginalIcons; 
    public Sprite[] ElementIcons; 
    public Sprite[] BWIcons;
    public Sprite[] CDIcons; 
    private Sprite[] PreviousIcon;
    private int currentElement = 0; // Assuming 0 is the default element 
    
    [SerializeField] private ElementSwitching Element; 
    [SerializeField] private SwitchSkills Skills;

    void Update() 
    { 
        if (Element != null) 
        { 
            currentElement = Element.GetCurrentElement(); 

            if(Skills.windReady)
            {
                if (Element.wind_element) // check if element is active
                {
                
                if(currentElement == 1)
                {
                    /*Icons[4].sprite = BWIcons[4];*/
                    Icons[0].sprite = ElementIcons[0];
                    
                }else
                {
                    Icons[0].sprite = BWIcons[0];
                }

                }
                else
                {
                    Icons[0].sprite  = OriginalIcons[0];
                    /*Icons[4].sprite = ElementIcons[4];*/
                
                }
            }else
            {
                if(Element.wind_element)
                {
                    Icons[0].sprite = CDIcons[0];
                }else
                {
                    Icons[0].sprite  = OriginalIcons[0];
                }
                
            }
           
            //---------------------------------------------------------------
            if(Skills.waterReady)
            {
                 if (Element.water_element) // check if element is active
                {   
                
                if(currentElement == 2)
                {
                    /*Icons[4].sprite = BWIcons[4];*/
                    Icons[1].sprite = ElementIcons[1];
                }else
                {
                    Icons[1].sprite = BWIcons[1];
                }
                }
                else
                {
                    Icons[1].sprite  = OriginalIcons[1];
                    /*Icons[4].sprite = ElementIcons[4];*/
                }
            }else
            {
                if(Element.water_element)
                {
                    Icons[1].sprite = CDIcons[1];
                }else
                {
                    Icons[1].sprite  = OriginalIcons[1];
                }
            }
           
            //---------------------------------------------------------------
            if(Skills.fireReady)
            {
                if (Element.fire_element) // check if element is active
                {
                
                    if(currentElement == 3)
                    {
                        /*Icons[4].sprite = ElementIcons[4];*/
                        Icons[2].sprite = ElementIcons[2];
                    }else
                    {
                        Icons[2].sprite = BWIcons[2];
                    }
                
                }
                else
                {
                    Icons[2].sprite  = OriginalIcons[2];
                    
                }
            }else
            {
                if (Element.fire_element)
                {
                    Icons[2].sprite = CDIcons[2];
                }
                else
                {
                    Icons[2].sprite = OriginalIcons[2];
                }
            }
           
            //---------------------------------------------------------------
            if(Skills.earthReady)
            {
                if (Element.earth_element) // check if element is active
                {
                    if(currentElement == 4)
                    {
                        /*Icons[4].sprite = ElementIcons[4];*/
                        Icons[3].sprite = ElementIcons[3];
                    }
                    else
                    {
                        Icons[3].sprite = BWIcons[3];
                    }
                }
                else
                {
                    Icons[3].sprite  = OriginalIcons[3];
                }
            }

            else
            {
                if(Element.earth_element)
                {
                    Icons[3].sprite = CDIcons[3];
                }else
                {
                    Icons[3].sprite  = OriginalIcons[3];
                }
            }
            if(currentElement == 5)
            {
                /*Icons[4].sprite = ElementIcons[4];*/
                 Icons[4].sprite = ElementIcons[4];
                
            }
            else
            {
                Icons[4].sprite = BWIcons[4];
            }

        } 
    

    } 
}
