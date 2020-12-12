using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Customer : InteractableObject
{
    [SerializeField]
    private Image emotionImage;

    [SerializeField]
    private Sprite[] emotionSprites;

    [SerializeField]
    private Image desiredDrinkImage;

    private float happinessMultiplier = 1f;

    [SerializeField]
    private float maxWaitingTime;

    private Drink.DRINKTYPE desiredDrink;
    private EMOTION emotion;
    private float happinessBonus = 10f;

    private float currentHappiness;
    private float maxHappiness = 100f;
    private float happinessSubstract;

    private HUDManager hudManager;

    private Color[] colors = new Color[]
    {
        Color.red,
        Color.green,
        Color.cyan
    };

    private enum EMOTION
    {
        Happy,
        Irritated,
        Angry
    }

    public override void Use(ItemSlot playersItemSlot)
    {
        if (playersItemSlot.Item)
        {
            if (playersItemSlot.Item.GetComponent<Tray>())
            {
                if (playersItemSlot.Item.GetComponent<Tray>().SearchForDesireDrink(desiredDrink))
                {
                    AddHappiness(happinessBonus);
                    SpriteLayerChanger.Instance.RemoveReference(this.GetComponentInChildren<SpritesContainer>());

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
        hudManager = FindObjectOfType<HUDManager>();
        currentHappiness = maxHappiness;
        DesireRandomDrink();
    }

    private void Update()
    {
        if (currentHappiness > 0)
        {
            currentHappiness -= Time.deltaTime * happinessMultiplier;
        }
        ChangeEmotion();
        UpdateHudManager();
    }

    private void UpdateHudManager()
    {
        hudManager.SubstractFromHappyMeter(happinessSubstract * Time.deltaTime);
    }

    private void ChangeEmotion()
    {
        if (currentHappiness > 66)
        {
            emotion = EMOTION.Happy;
            happinessSubstract = 0f;
        }
        else if (currentHappiness > 33 && currentHappiness <= 66)
        {
            emotion = EMOTION.Irritated;
            happinessSubstract = 1f;

        }
        else if (currentHappiness < 33)
        {
            emotion = EMOTION.Angry;
            happinessSubstract = 2f;
        }

        emotionImage.sprite = emotionSprites[(int)emotion];
    }
    private void DesireRandomDrink()
    {
        desiredDrink = (Drink.DRINKTYPE)Random.Range(0, 3);
        desiredDrinkImage.color = colors[(int)desiredDrink];
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


