using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    [SerializeField]
    private Image happyMeterImage;

    [SerializeField]
    private float happinessMultiplier;

    [SerializeField]
    private GameObject drinksHolder;

    private GameObject[] currentDrinks;

    [SerializeField]
    private GameObject drinkPrefab;

    private float happyMeter;
    private float maxHappyMeter = 100f;

    private void Start()
    {
        happyMeter = maxHappyMeter;
        currentDrinks = new GameObject[3];
    }

    private void Update()
    {
        UpdateHappyMeter();
    }

    private void UpdateHappyMeter()
    {
        happyMeterImage.fillAmount = happyMeter / maxHappyMeter;
        happyMeterImage.color = Color.Lerp(Color.red, Color.green, happyMeter/maxHappyMeter);
    }

    public void AddDrinkToHud(int place, Color color)
    {
        GameObject drink = Instantiate(drinkPrefab, drinksHolder.transform);
        currentDrinks[place] = drink;
        drink.GetComponent<DrinkHUD>().ChangeColor(color);
       
    }

    public void RemoveDrinkFromHud(int place)
    {
        Destroy(currentDrinks[place]);
        currentDrinks[place] = null;
    }

    public void AddToHappyMeter(float value)
    {
        happyMeter += value;
        if (happyMeter > maxHappyMeter)
            happyMeter = maxHappyMeter;
    }

    public void SubstractFromHappyMeter(float value)
    {
        happyMeter -= value;
        if (happyMeter < 0)
            happyMeter = 0;
    }
}
