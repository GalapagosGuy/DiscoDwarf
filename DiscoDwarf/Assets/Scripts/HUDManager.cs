using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUDManager : MonoBehaviour
{
    [SerializeField]
    private Image happyMeterImage;

    [SerializeField]
    private float happinessMultiplier;

    [SerializeField]
    private GameObject[] desiredDrinksHolder;

    [SerializeField]
    private GameObject drinksHolder;

    private GameObject[] currentDrinks;

    [SerializeField]
    private GameObject drinkPrefab;

    private float happyMeter;
    private float maxHappyMeter = 100f;

    private int[] desiredDrinks = new int[3];

    private struct DesiredDrinks
    {
        private int redDrinks;
        private int greenDrinks;
    }

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

    public void AddDesiredDrink(Drink.DRINKTYPE drinkType)
    {
        desiredDrinks[(int)drinkType] += 1;
        desiredDrinksHolder[(int)drinkType].GetComponent<TextMeshProUGUI>().text = desiredDrinks[(int)drinkType].ToString();
    }

    public void RemoveDesiredDrink(Drink.DRINKTYPE drinkType)
    {
        desiredDrinks[(int)drinkType] -= 1;
        desiredDrinksHolder[(int)drinkType].GetComponent<TextMeshProUGUI>().text = desiredDrinks[(int)drinkType].ToString();

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
