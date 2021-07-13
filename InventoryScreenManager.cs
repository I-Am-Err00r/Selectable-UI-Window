using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

//This script will manage turning on and off the UI with a button press and set up the UI initially when turned on
public class InventoryScreenManager : MonoBehaviour
{
    // A quick reference to the gameobject that will act as the parent display gameobject for the UI, usually first child of Canvas
    public GameObject contentWindow;

    private void Start()
    {
        //Set the contentWindow to false so it doesn't appear until button is pressed
        contentWindow.SetActive(false);
    }

    private void Update()
    {
        //Checks for input detection, for this example it is looking for the I key
        if (Input.GetKeyDown(KeyCode.I))
        {
            //Checks if this gameobject is active currently, if not runs logic in the if statement
            if (!contentWindow.activeInHierarchy)
            {
                //If the above if statement is true, then it turns on the contentWindow gameobject so we can see the UI
                contentWindow.SetActive(true);
                //Finds the gameobject that is acting as the selector and runs the method to initally select the correct item in the UI
                GetComponentInChildren<InventorySelectorManager>().SetSelection();
            }
            //If the above if statement is false, it runs this logic in the else statement
            else
            {
                //Gets a quick reference of the event system
                EventSystem events = FindObjectOfType<EventSystem>();
                //Moves the scrollbar back up top so it displays the upper part of the contentWindow
                GetComponentInChildren<Scrollbar>().value = 1;
                //Sets the content window to inactive so it is hidden again
                contentWindow.SetActive(false);
                //Makes sure there isn't a current selected gameobject in the event system
                events.SetSelectedGameObject(null);
            }
        }
    }
}
