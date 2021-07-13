using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script can make the scriptable object in the Unity Editor which will contain serialized data to plug into the appicable UI components to give this item the unique properties such as name and image
[CreateAssetMenu(fileName = "Inventory Object", menuName = "ScriptableObjects/Inventory/Inventory Objects", order = 1)]
public class InventoryItem : ScriptableObject
{
    //A string that will act as the name for this item when displayed in the UI
    public string itemName;
    //A Sprite that will act as the image for this item when displayed in the UI
    public Sprite spriteImage;
}
