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

    public void AddDrink(GameObject drink)
    {
        drinks.Add(drink);
        drink.transform.SetParent(this.transform);
        drink.transform.position = drinkTransforms[drinks.Count - 1].transform.position;
    }

    public void RemoveDrink(GameObject drink)
    {
        drinks.Remove(drink);
    }
}
