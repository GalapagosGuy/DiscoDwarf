using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoboHand : MonoBehaviour
{
    public HandSprite[] hands;

    public void ChangeHand(ITEMTYPE itemType)
    {
        foreach (HandSprite hs in hands)
        {
            hs.objectWithHand.SetActive(itemType == hs.type);
        }
    }
}

[System.Serializable]
public struct HandSprite
{
    public ITEMTYPE type;
    public GameObject objectWithHand;
}
