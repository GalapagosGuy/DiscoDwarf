using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ComboCounter : IMusicListener
{
    public static ComboCounter Instance = null;

    public TextMeshProUGUI counter;

    private void Awake()
    {
        if (ComboCounter.Instance == null)
            ComboCounter.Instance = this;
        else
            Destroy(this);
    }

    private void Start()
    {
        if (counter)
        {
            combo = 0;
            counter.text = combo + "";
        }
    }

    private bool anyInputProcessed = false;
    private int combo = 0;

    public int Combo { get => combo; }

    public override void OnBeatCenter()
    {

    }

    public override void OnBeatFinished()
    {
        CheckIfAnyInputPressed();
    }

    public override void OnBeatStart()
    {
        anyInputProcessed = false;
    }

    public void InputPressed()
    {
        if (!anyInputProcessed && counter)
        {
            combo++;
            GetComponent<Animator>().SetTrigger("Bump");
            counter.text = combo + "";
        }

        anyInputProcessed = true;
    }

    public void BreakCombo()
    {
        combo = 0;
        counter.text = combo + "";
    }

    private void CheckIfAnyInputPressed()
    {
        if (!anyInputProcessed && counter)
        {
            combo = 0;
            counter.text = combo + "";
        }
    }
}
