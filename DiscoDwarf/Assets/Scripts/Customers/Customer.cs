using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Customer : InteractableObject
{
    public AudioClip teleportInClip = null;
    public AudioClip teleportOutClip = null;
    public AudioClip thirstyClip = null;

    [System.Serializable]
    public struct Body
    {
        public GameObject accesories;
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
    private GameObject drinkBackground;

    [SerializeField]
    private Sprite[] drinkBases;

    [SerializeField]
    private Sprite[] drinkFills;

    [SerializeField]
    private Image drinkBase;

    [SerializeField]
    private float waitingMinTime = 4.0f;

    [SerializeField]
    private float waitingMaxTime = 10.0f;

    private float waitingTime = 0.0f;
    private float currentWaitingTime = 0.0f;

    private bool isWaitingForOrderDrink = false;
    private bool isWaitingForGoHome = false;

    private AudioSource audioSource = null;


    [SerializeField]
    private bool isReal;

    [SerializeField]
    private GameObject canvas;

    private float happinessMultiplier = 1f;

    private Drink.DRINKTYPE desiredDrink;
    private EMOTION emotion;
    private float happinessBonus = 10f;
    private int pointBonus = 15;

    private float currentHappiness;
    private float maxHappiness = 100f;
    private float happinessSubstract;

    private HUDManager hudManager;

    private bool canBeUsed = true;

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
        if (!canBeUsed)
            return;

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
                    desiredDrinkImage.enabled = false;
                    drinkBase.enabled = false;
                    drinkBackground.SetActive(false);
                    emotionImage.gameObject.SetActive(false);
                    canBeUsed = false;

                    currentWaitingTime = 0.0f;
                    waitingTime = Random.Range(waitingMinTime, waitingMaxTime);
                    isWaitingForGoHome = true;

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
        audioSource = GetComponent<AudioSource>();

        hudManager = FindObjectOfType<HUDManager>();
        currentHappiness = maxHappiness;
        RandomAppearance();
        if (isReal)
        {
            StartCoroutine(GetComponentInChildren<ModelDisolver>().Undissolve(1f));
            audioSource.clip = teleportInClip;
            audioSource.Play();

            waitingTime = Random.Range(waitingMinTime, waitingMaxTime);
            currentWaitingTime = 0.0f;
            isWaitingForOrderDrink = true;
        }

        if (!isReal)
            canvas.SetActive(false);
    }

    private void RandomAppearance()
    {
        if (bodyParts.accesories)
            bodyParts.accesories.GetComponent<SpriteRenderer>().color = RandomColor();
        if (bodyParts.chest)
            bodyParts.chest.GetComponent<SpriteRenderer>().color = RandomColor();
        if (bodyParts.bottom)
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

        if (isWaitingForOrderDrink || isWaitingForGoHome)
        {
            currentWaitingTime += Time.deltaTime;

            if (currentWaitingTime >= waitingTime)
            {
                if (isWaitingForOrderDrink)
                {
                    DesireRandomDrink();
                    isWaitingForOrderDrink = false;
                }
                else if (isWaitingForGoHome)
                {
                    GoHome();
                    isWaitingForGoHome = false;
                }
            }
        }
    }

    private void UpdateHudManager()
    {
        if (isReal)
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
        canvas.SetActive(true);
        drinkBackground.SetActive(true);
        emotionImage.gameObject.SetActive(true);
        desiredDrink = (Drink.DRINKTYPE)Random.Range(0, 3);
        desiredDrinkImage.sprite = drinkFills[(int)desiredDrink];
        drinkBase.sprite = drinkBases[(int)desiredDrink];
        desiredDrinkImage.color = colors[(int)desiredDrink];

        hudManager.AddDesiredDrink(desiredDrink);

        if (isReal)
        {
            audioSource.clip = thirstyClip;
            audioSource.Play();
        }
    }

    private void AddHappiness(float value)
    {
        FindObjectOfType<HUDManager>().AddToHappyMeter(value);
    }

    private void GoHome()
    {
        CustomersManager.Instance.CustomerGone();
        SpriteLayerChanger.Instance.RemoveReference(GetComponentInChildren<SpritesContainer>());

        audioSource.clip = teleportOutClip;
        audioSource.Play();

        StartCoroutine(GetComponentInChildren<ModelDisolver>().Dissolve(1f));
        Destroy(this.gameObject, 1.05f);
    }

}


