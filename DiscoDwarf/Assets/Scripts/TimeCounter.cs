using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimeCounter : MonoBehaviour
{
    private float time = 300;
    private TextMeshProUGUI text;
    private HUDManager HUDManager;

    private void Awake()
    {
        text = GetComponentInChildren<TextMeshProUGUI>();
        HUDManager = FindObjectOfType<HUDManager>();
    }

    void Update()
    {
        if (time <= 0)
            HUDManager.EndGame("Funktastic!");
        else
            time -= Time.deltaTime;
        
        UpdateDisplay();
    }

    private void UpdateDisplay()
    {
        string _text = "";
        int minutes = (int)time / 60;
        int seconds = (int)time % 60;
        if (minutes < 10)
        {
            _text = "0";
        }
        _text += minutes.ToString() + ":";
        if (seconds < 10)
        {
            _text += "0";
        }
        _text += seconds.ToString();
        text.text = _text;
    }

}
