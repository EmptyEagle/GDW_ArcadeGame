using UnityEngine;

public class ButtonInteractableObjects : MonoBehaviour
{
    public static bool ButtonCheck(GameObject[] requiredButtons, GameObject[] overrideButtons, bool onlyOneRequired)
    {
        // Start with a success for the sake of returning it later if nothing changes
        bool buttonRequirementMet = true;
        
        // Loop through the override buttons to check if all are pressed
        for (int i = 0; i < overrideButtons.Length; i++)
        {
            Button curButtonCheck = overrideButtons[i].GetComponent<Button>();
            // If the the button requirement isn't met at this point, make sure all the override buttons are pressed
            if (!curButtonCheck.isPressed)
            {
                //Debug.Log("Override button [" + overrideButtons[i].name + "] not pressed.");
                buttonRequirementMet = false;
                break;
            }
        }
        
        // Loop through the typically required buttons to check if all are pressed
        for (int i = 0; i < requiredButtons.Length; i++)
        {
            Button curButtonCheck = requiredButtons[i].GetComponent<Button>();
            // There's no need to check for required buttons if the button requirements have been met by the override
            if (i == 0 && buttonRequirementMet && overrideButtons.Length > 0)
            {
                //Debug.Log("Override button(s) pressed. Skipping required button check.");
                break;
            }
            // If the button requirements haven't been met by the override, restart the sequence by setting the requirement to met and looping through the required buttons
            else if (i == 0)
            {
                buttonRequirementMet = true;
                //Debug.Log("Override button(s) not pressed or none are present. Starting required button check.");
            }
            // If only one button is required to be pressed, loop through all the buttons to see if one is pressed
            if (onlyOneRequired && curButtonCheck.isPressed)
            {
                //Debug.Log("Required button [" + requiredButtons[i].name + "] pressed. ONLY ONE REQUIRED.");
                buttonRequirementMet = true;
                break;
            }
            else if (onlyOneRequired)
            {
                //Debug.Log("Required button [" + requiredButtons[i].name + "] not pressed. ONLY ONE REQUIRED.");
                buttonRequirementMet = false;
            }
            // Else, if a required button is not pressed, break out of the loop with a failure
            else if (!curButtonCheck.isPressed)
            {
                //Debug.Log("Required button [" + requiredButtons[i].name + "] not pressed.");
                buttonRequirementMet = false;
                break;
            }
        }
        
        return buttonRequirementMet;
    }
}
