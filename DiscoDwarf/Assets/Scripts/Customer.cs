using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Customer : InteractableObject
{
    private Drink.DRINKTYPE desiredDrink;
    private Image drinkImage;

    private Color[] colors = new Color[]
    {
        Color.red,
        Color.green,
        Color.cyan
    };

    public override void Use(ItemSlot playersItemSlot)
    {
        if (playersItemSlot.Item)
        {
            if (playersItemSlot.Item.GetComponent<Tray>())
            {
                if(playersItemSlot.Item.GetComponent<Tray>().SearchForDesireDrink(desiredDrink))
                {
                    GoHome();
                    Debug.Log("Customer got desired drunk");
                }
                Debug.Log("No desired drink on tray");
            }
            else
                Debug.Log("No tray in hands");
        }
        else
            Debug.Log("No item in hands");
    }

    private void Awake()
    {
        drinkImage = GetComponentInChildren<Image>();
        DesireRandomDrink();
    }

    private void DesireRandomDrink()
    {
        desiredDrink = (Drink.DRINKTYPE)Random.Range(0, 3);
        drinkImage.color = colors[(int)desiredDrink];
    }

    private void GoHome()
    {
        Destroy(this.gameObject);
    }

}


