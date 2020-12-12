using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bin : InteractableObject
{
    public override void Use(ItemSlot playersItemSlot)
    {
        if(playersItemSlot.Item.GetComponent<Tray>())
        {
            playersItemSlot.Item.GetComponent<Tray>().RemoveAllDrinks();
        }
    }
}
