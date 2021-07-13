using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

//This script will manage the gameobject that will act as a selection visual cue
public class InventorySelectorManager : MonoBehaviour
{
    //The gameobject that is an empty parent gameobject of the inventoryContent
    public GameObject inventoryViewport;
    //The gameobject that is the parent of all the different items; this gameobject will also contain the grid layout group
    public GameObject inventoryContent;
    //The gameobject that is acting as the scrollBar
    public Scrollbar scrollBar;

    //A reference to all the different items in the UI so we can get exact position coordinates to move the selection gameobject when that item is selected
    private InventoryItemSelection[] items;
    //The amount of time it takes the inventory display gameobject to move to it's new position when the scroll bar moves
    private float lerpDuration = .25f;
    //A bool that stops things from moving additionally if they are already moving; basically reduces jitter and twitching while things are being positioned correctly
    private bool lerping;


    private void Update()
    {
        //If there are some sort of items within with the UI display
        if (items != null)
        {
            //Makes a quick reference to the rectTransform on the current selected gameobject based on the Unity Event system
            RectTransform rect = EventSystem.current.currentSelectedGameObject.GetComponent<RectTransform>();
            //This if statement will check to see if the scrollbar needs to move down to show the items below what is currently visible
            if ((rect.transform.position.y + rect.rect.yMin) < (inventoryViewport.transform.position.y + inventoryViewport.GetComponent<RectTransform>().rect.yMin) && !lerping)
            {
                //Handles positioning the window correctly based on the bottom of the selected item
                Vector3 newPosition = new Vector3(inventoryContent.transform.position.x, inventoryContent.transform.position.y + (rect.rect.size.y + inventoryContent.GetComponentInChildren<GridLayoutGroup>().spacing.y), transform.position.z);
                //Moves the window smoothly so it doesn't appear so jerky when moving
                StartCoroutine(Lerp(newPosition));
            }
            //This if statement and all the logic inside it do the same thing as the if statement above, but shows the items above, not below
            if ((rect.transform.position.y + rect.rect.yMax) > (inventoryViewport.transform.position.y + inventoryViewport.GetComponent<RectTransform>().rect.yMax) && !lerping)
            {
                Vector3 newPosition = new Vector3(inventoryContent.transform.position.x, inventoryContent.transform.position.y - (rect.rect.size.y + inventoryContent.GetComponentInChildren<GridLayoutGroup>().spacing.y), transform.position.z);
                StartCoroutine(Lerp(newPosition));
            }
            //Gets the current position of the current selected gameobject in the Event system
            Vector2 itemPosition = new Vector2(rect.transform.position.x, rect.transform.position.y);
            //Snaps the position of the selection gameobject to the current position of the selected gameobject in the Event System
            GetComponent<RectTransform>().transform.position = itemPosition;
        }
    }

    //This is called from the InventoryScreenManager script; sets up the initial selected gameobject
    public void SetSelection()
    {
        //Checks to see if the items array has been loaded
        if (items == null)
        {
            //If it is currently blank, it fills up the items array with the children gameobjects that contain the InventoryItemSelection script
            items = inventoryContent.GetComponentsInChildren<InventoryItemSelection>();
        }
        //Sets the first item in this area to the current selected button
        items[0].GetComponent<Button>().Select();
        //Creates a temporary rectTransform variable of the current selected gameobject's rectTransform
        RectTransform rect = items[0].GetComponent<RectTransform>();
        //Creates a temporary vector 2 variable of the current position of the rect
        Vector2 firstItemPosition = new Vector2(rect.transform.position.x, rect.transform.position.y);
        //Moves the selection gameobject to the current temporary position of the rect
        GetComponent<RectTransform>().transform.position = firstItemPosition;
        //This foreach loop adds custom data to the prefabs we have loaded into the UI based on the scriptable object data loaded into those prefabs
        foreach (InventoryItemSelection item in items)
        {
            //Changes the Sprite of the item in the UI to the custom image provided by scriptable object
            item.GetComponentInChildren<Image>().sprite = item.item.spriteImage;
            //Changes the Sprite of the item name in the UI to the custom string name provided by scriptable object
            item.GetComponentInChildren<Text>().text = item.item.itemName;
        }
    }

    //Handles smoothly moving the display window when it needs to; rather hard to explain how this works if you're not familiar with programming
    IEnumerator Lerp(Vector3 newPosition)
    {
        lerping = true;
        float timeElapsed = 0;
        Vector3 startPosition = inventoryContent.transform.position;
        while(timeElapsed < lerpDuration)
        {
            inventoryContent.transform.position = Vector3.Lerp(startPosition, newPosition, timeElapsed / lerpDuration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        lerping = false;
        inventoryContent.transform.position = newPosition;
    }
}
