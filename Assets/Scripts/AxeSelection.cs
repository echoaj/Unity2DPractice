using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AxeSelection : MonoBehaviour
{
    
    public void HandleAxeSelection()
    {
        GameObject axeButton = EventSystem.current.currentSelectedGameObject;
        if (axeButton != null)
        {
            Debug.Log("Clicked on : " + axeButton.name);
            switch (axeButton.name)
            {
                case "axe1_Wood":
                    Mine.axeStrength = 1;
                    break;

                case "axe2_Steel":
                    Mine.axeStrength = 2;
                    break;

                case "axe3_Gold":
                    Mine.axeStrength = 4;
                    break;

                default:
                    break;
            }
        }
        
    }

}
