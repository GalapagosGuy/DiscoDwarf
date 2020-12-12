using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrinkHUD : MonoBehaviour
{
    [SerializeField]
    private Image drinkImage;

    public void ChangeColor(Color color)
    {
        drinkImage.color = color;
    }
}
