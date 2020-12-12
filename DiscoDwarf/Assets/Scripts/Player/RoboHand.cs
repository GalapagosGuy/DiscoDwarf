using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoboHand : MonoBehaviour
{
    public ItemSlot itemSlot;
    public Hand[] hands;

    private int currentIndex = 0;

    public void SwapHand()
    {
        GameObject currentObject = hands[currentIndex].handObject;

        if (currentIndex + 1 >= hands.Length)
            currentIndex = 0;
        else
            currentIndex++;

        if (currentObject.GetComponent<Tray>())
            itemSlot?.HideTray();
        else
            itemSlot?.RemoveItemFromSlot();

        if (hands[currentIndex].handObject.GetComponent<Tray>())
            itemSlot?.ShowTray();
        else
            itemSlot.AddItemToSlot(Instantiate(hands[currentIndex].handObject));

        ChangeHand(hands[currentIndex].type);
    }

    public void ChangeHand(ITEMTYPE itemType)
    {
        foreach (Hand hs in hands)
        {
            hs.objectWithHandSprite.SetActive(itemType == hs.type);
        }
    }
}

[System.Serializable]
public struct Hand
{
    public ITEMTYPE type;
    public GameObject objectWithHandSprite;
    public GameObject handObject;
}
