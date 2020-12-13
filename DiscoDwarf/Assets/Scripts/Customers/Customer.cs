using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Customer : InteractableObject
{
    [System.Serializable]
    public struct Body
    {
        public GameObject arm;
        public GameObject skin;
        public GameObject chest;
        public GameObject bottom;
        public GameObject hair;
    }

    [SerializeField]
    private Color[] hairColors;

    [SerializeField]
    private Body bodyParts;

    [SerializeField]
    private Image emotionImage;

    [SerializeField]
    private Sprite[] emotionSprites;

    [SerializeField]
    private Image desiredDrinkImage;

    [SerializeField]
    private bool isReal;

    [SerializeField]
    private GameObject canvas;

    private float happinessMultiplier = 1f;

    [SerializeField]
    private float maxWaitingTime;

    private Drink.DRINKTYPE desiredDrink;
    private EMOTION emotion;
    private float happinessBonus = 10f;
    private int pointBonus = 15;

    private float currentHappiness;
    private float maxHappiness = 100f;
    private float happinessSubstract;

    private HUDManager hudManager;

    private Color[] colors = new Color[]
    {
        Color.red,
        Color.yellow,
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
        if (isReal && playersItemSlot.Item)
        {
            if (playersItemSlot.Item.GetComponent<Tray>())
            {
                if (playersItemSlot.Item.GetComponent<Tray>().SearchForDesireDrink(desiredDrink))
                {
                    AddHappiness(happinessBonus);
                    SpriteLayerChanger.Instance.RemoveReference(this.GetComponentInChildren<SpritesContainer>());
                    hudManager.RemoveDesiredDrink(desiredDrink);
                    hudManager.AddPoints(pointBonus);
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
        RandomAppearance();
        DesireRandomDrink();
        hudManager.AddDesiredDrink(desiredDrink);
        if (!isReal)
            canvas.SetActive(false);
    }

    private void RandomAppearance()
    {
        //bodyParts.arm.GetComponent<SpriteRenderer>().color = RandomColor();
        //bodyParts.skin.GetComponent<SpriteRenderer>().color = Color.green * 0.75f;
        if (bodyParts.chest)
            bodyParts.chest.GetComponent<SpriteRenderer>().color = RandomColor();
        if(bodyParts.bottom)
            bodyParts.bottom.GetComponent<SpriteRenderer>().color = RandomColor();
        if (bodyParts.hair)
            bodyParts.hair.GetComponent<SpriteRenderer>().color = RandomHairColor();
    }
    private Color RandomHairColor()
    {
        return hairColors[Random.Range(0, hairColors.Length)];
    }
    private Color RandomColor()
    {
        return new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
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
        if(isReal)
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
            happinessSubstract = 0.5f;

        }
        else if (currentHappiness < 33)
        {
            emotion = EMOTION.Angry;
            happinessSubstract = 1.25f;
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


