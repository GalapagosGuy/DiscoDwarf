using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BroomHolder : InteractableObject
{
    public GameObject broomToTake = null;

    public override void Use(ItemSlot playersItemSlot)
    {
        GameObject itemInHand = null;

        if (!playersItemSlot.Item)
            Debug.Log("No item in hands");
        else
            itemInHand = playersItemSlot.Item;

        //if player doesn't have broom in hands give it to him
        if (itemInHand == null || (itemInHand != null && itemInHand.GetComponent<Broom>() == null))
        {
            if (itemInHand.GetComponent<Tray>())
            {
                playersItemSlot.HideTray();
            }

            playersItemSlot.AddItemToSlot(broomToTake);
        }
        //if player has broom take it from him
        else
        {
            playersItemSlot.RemoveItemFromSlot();

            playersItemSlot.ShowTray();
        }
    }
}
