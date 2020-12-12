using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drink : MonoBehaviour
{
    [SerializeField]
    private DRINKTYPE drinkType;

    public DRINKTYPE DrinkType { get => drinkType; }

    public enum DRINKTYPE
    {
        Drink1,
        Drink2,
        Drink3,
        Drink4
    }
}
