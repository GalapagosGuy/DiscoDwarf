using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tray : Item
{
    private GameObject[] drinks;
    private HUDManager HUDManager;
    [SerializeField]
    private int maxSize;

    [SerializeField]
    private GameObject[] drinkTransforms;

    private bool[] drinkPlace;

    public int MaxSize { get => maxSize; }
    private int drinkNumber;

    private void Awake()
    {
        HUDManager = FindObjectOfType<HUDManager>();

        drinkNumber = 0;
        drinks = new GameObject[maxSize];
        drinkPlace = new bool[drinkTransforms.Length];
        for (int i = 0; i < drinkPlace.Length; i++)
        {
            drinkPlace[i] = false;
        }
    }

    public bool HasFreeSpace()
    {
        if (drinkNumber + 1 <= maxSize)
            return true;
        else
            return false;
    }

    public bool SearchForDesireDrink(Drink.DRINKTYPE desiredDrink)
    {
        for(int i = 0; i < maxSize; i++)
        {
            if (drinks[i] && drinks[i].GetComponent<Drink>().DrinkType == desiredDrink)
            {
                GameObject currentDrink = drinks[i];
                RemoveDrink(drinks[i], i);
               
                Destroy(currentDrink);
                return true;
            }     
        }
        return false;
    }

    public void AddDrink(GameObject drink)
    {
        int place = FreeSpaceOnTray();
        drink.transform.SetParent(drinkTransforms[place].transform);
        drink.transform.localPosition = Vector3.zero;
        drink.transform.localRotation = Quaternion.identity;
        drink.transform.localScale = Vector3.one;
        drinks[place] = drink;
        drinkNumber++;
        HUDManager.AddDrinkToHud(place, colors[(int)drink.GetComponent<Drink>().DrinkType]);
        Debug.Log($"Added {drink.GetComponent<Drink>().DrinkType} to {place} place on tray");
    }

    public void RemoveDrink(GameObject drink, int place)
    {
        Debug.Log($"Removed {drink.GetComponent<Drink>().DrinkType} from {place} place on tray");
        drinks[place] = null;
        drinkPlace[place] = false;
        HUDManager.RemoveDrinkFromHud(place);
        drinkNumber--;
    }

    private int FreeSpaceOnTray()
    {
        for(int i = 0; i < drinkPlace.Length; i++)
        {
            if (!drinkPlace[i])
            {
                drinkPlace[i] = true;
                return i;
            }
                
        }
        return 0;
    }
    public void RemoveAllDrinks()
    {
        for (int i = 0; i < maxSize; i++)
        {
            if(drinks[i])
            {
                Destroy(drinks[i]);
                drinks[i] = null;  
            }
            drinkPlace[i] = false;
        }
        drinkNumber = 0;
        
        Debug.Log("Removed all drinks from tray");

    }


    public Color[] colors = new Color[]
{
        Color.red,
        Color.green,
        Color.cyan
};
}
