using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script will be attached to any content that will be added to UI as an Item; this variable is a scriptable object
public class InventoryItemSelection : MonoBehaviour
{
    //Reference to the exact scriptable object that contains the correct data to display this item within the UI
    public InventoryItem item;
}
