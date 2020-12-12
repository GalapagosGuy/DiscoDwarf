using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimeCounter : MonoBehaviour
{
    private static float time = 0;
    private static TextMeshProUGUI text;

    public static float CTime() { return time; }

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        time += Time.deltaTime;
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

    public static string GetTime() { return text.text; }
    public static void ResetTime() { time = 0; }
}
