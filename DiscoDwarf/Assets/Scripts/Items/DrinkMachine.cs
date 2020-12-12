using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrinkMachine : InteractableObject
{ 
    [SerializeField]
    private GameObject servedDrink;

    public override void Use(ItemSlot playersItemSlot)
    {
        GameObject itemInHand = null;

        if (!playersItemSlot.Item)
            Debug.Log("No item in hands");
        else
            itemInHand = playersItemSlot.Item;

        if(itemInHand.GetComponent<Tray>())
        {
            if (itemInHand.GetComponent<Tray>().HasFreeSpace())
            {
                itemInHand.GetComponent<Tray>().AddDrink(Instantiate(servedDrink, transform.position, Quaternion.identity));
            }
            else
                Debug.Log("No more space left for drinks");
        }
        else
            Debug.Log($"No Tray in hands - itemslot occupied by {itemInHand.name}");
    }

}
