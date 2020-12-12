using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tray : Item
{
    private List<GameObject> drinks;

    [SerializeField]
    private int maxSize;

    [SerializeField]
    private GameObject[] drinkTransforms;

    public int MaxSize { get => maxSize; }

    private void Awake()
    {
        drinks = new List<GameObject>();
    }

    public bool HasFreeSpace()
    {
        if (drinks.Count + 1 <= maxSize)
            return true;
        else
            return false;
    }

    public bool SearchForDesireDrink(Drink.DRINKTYPE desiredDrink)
    {
        for(int i = 0; i < drinks.Count; i++)
        {
            if (drinks[i].GetComponent<Drink>().DrinkType == desiredDrink)
            {
                Destroy(drinks[i]);
                drinks.Remove(drinks[i]);
                return true;
            }     
        }
        return false;
    }

    public void AddDrink(GameObject drink)
    {
        Debug.Log($"Added {drink.GetComponent<Drink>().DrinkType} to tray");
        drinks.Add(drink);
        drink.transform.SetParent(this.transform);
        drink.transform.position = drinkTransforms[drinks.Count - 1].transform.position;
    }

    public void RemoveDrink(GameObject drink)
    {
        Debug.Log($"Removed {drink.GetComponent<Drink>().DrinkType} from tray");
        drinks.Remove(drink);
    }

    public void RemoveAllDrinks()
    {
        for (int i = 0; i < drinks.Count; i++)
        {
            Destroy(drinks[i]);

        }
        drinks.Clear();
        Debug.Log("Removed all drinks from tray");

    }
}
