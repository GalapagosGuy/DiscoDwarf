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

    private float happyMeter;
    private float maxHappyMeter = 100f;

    private void Start()
    {
        happyMeter = maxHappyMeter;
    }

    private void Update()
    {
        UpdateHappyMeter();
    }

    private void UpdateHappyMeter()
    {
        if(happyMeter > 0)
        {
            happyMeter -= Time.deltaTime * happinessMultiplier;
        }

        happyMeterImage.fillAmount = happyMeter / maxHappyMeter;
        happyMeterImage.color = Color.Lerp(Color.red, Color.green, happyMeter/maxHappyMeter);
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
