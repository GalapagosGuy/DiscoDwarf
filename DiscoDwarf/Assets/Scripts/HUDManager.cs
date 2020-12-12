using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class HUDManager : MonoBehaviour
{
    [Header("Happiness meter parameters")]
    [SerializeField]
    private Image happyMeterImage;
    [SerializeField]
    private Image happyMeterEmotion;
    [SerializeField]
    private Sprite[] emotions;
    [SerializeField]
    private float happinessMultiplier;

    [Header("Drinks parameters")]
    [SerializeField]
    private GameObject[] desiredDrinksHolder;
    [SerializeField]
    private GameObject drinksHolder;
    private GameObject[] currentDrinks;
    [SerializeField]
    private GameObject drinkPrefab;

    [Header("Points")]
    [SerializeField]
    private TextMeshProUGUI pointsText;
    [SerializeField]
    private GameObject pointsMultiplierObject;

    [Header("End game canvas")]
    [SerializeField]
    private GameObject EndGameCanvas;
    [SerializeField]
    private TextMeshProUGUI endGameResult;
    [SerializeField]
    private TextMeshProUGUI endGamePoints;

    private int points;
    private float pointsMultiplier;
    private ComboCounter comboCounter;

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
        comboCounter = FindObjectOfType<ComboCounter>();
        points = 0;
        AddPoints(0);
        happyMeter = maxHappyMeter;
        currentDrinks = new GameObject[3];
    }

    private void Update()
    {
        UpdateHappyMeter();
        CalculatePointsMultiplier();
    }

    private void UpdateHappyMeter()
    {
        happyMeterImage.fillAmount = happyMeter / maxHappyMeter;
        if (happyMeter > 66)
            happyMeterEmotion.sprite = emotions[0];
        else if(happyMeter > 33 && happyMeter <= 66)
            happyMeterEmotion.sprite = emotions[1];
        else if (happyMeter <= 33)
            happyMeterEmotion.sprite = emotions[2];

        happyMeterImage.color = Color.Lerp(Color.red, Color.green, happyMeter/maxHappyMeter);
        if (happyMeter <= 0)
            EndGame("You funked up");
    }

    private void EndGame(string result)
    {
        EndGameCanvas.SetActive(true);
        endGameResult.text = result;
        endGamePoints.text = points.ToString();
    }
    public void PlayAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void AddPoints(int value)
    {
       
        points += (int)(value * pointsMultiplier);
        pointsText.text = points.ToString();
    }

    public void CalculatePointsMultiplier()
    {
        bool turnOn = false;
        if (comboCounter.Combo >= 10)
        {
            turnOn = true;
            pointsMultiplier = 1.1f;
        }
        if (comboCounter.Combo >= 25)
            pointsMultiplier = 1.25f;
        if (comboCounter.Combo >= 50)
            pointsMultiplier = 1.35f;
        if (comboCounter.Combo >= 100)
            pointsMultiplier = 1.5f;
        if (comboCounter.Combo >= 200)
            pointsMultiplier = 2f;

        if (turnOn)
        {
            pointsMultiplierObject.SetActive(true);
            pointsMultiplierObject.GetComponent<TextMeshProUGUI>().text = "x" + pointsMultiplier;
        }
        else
        {
            pointsMultiplierObject.SetActive(false);
        }
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
