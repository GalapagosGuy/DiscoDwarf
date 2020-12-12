using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Customer : InteractableObject
{
    [SerializeField]
    private float happinessMultiplier = 2f;

    private Drink.DRINKTYPE desiredDrink;
    private Image drinkImage;
    private float happinessBonus = 10f;

    private float currentHappiness;
    private float maxHappiness;

    private Color[] colors = new Color[]
    {
        Color.red,
        Color.green,
        Color.cyan
    };

    public override void Use(ItemSlot playersItemSlot)
    {
        if (playersItemSlot.Item)
        {
            if (playersItemSlot.Item.GetComponent<Tray>())
            {
                if(playersItemSlot.Item.GetComponent<Tray>().SearchForDesireDrink(desiredDrink))
                {
                    AddHappiness(happinessBonus);
                    GoHome();
                    Debug.Log($"Customer got desired drink - {desiredDrink}");
                }
                else
                    Debug.Log("No desired drink on tray");
            }
            else
                Debug.Log("No tray in hands");
        }
        else
            Debug.Log("No item in hands");
    }

    private void Awake()
    {
        drinkImage = GetComponentInChildren<Image>();
        DesireRandomDrink();
    }

    private void Update()
    {
        if(currentHappiness > 0)
        {
            currentHappiness -= Time.deltaTime * happinessMultiplier;
        }
    }
    private void DesireRandomDrink()
    {
        desiredDrink = (Drink.DRINKTYPE)Random.Range(0, 3);
        drinkImage.color = colors[(int)desiredDrink];
    }

    private void AddHappiness(float value)
    {
        FindObjectOfType<HUDManager>().AddToHappyMeter(value);
    }
    private void GoHome()
    {
        Destroy(this.gameObject);
    }

}


