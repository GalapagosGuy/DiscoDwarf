using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicBeatDisplay : IMusicListener
{
    [SerializeField]
    private float speed = 15.0f;

    public Image mainBeatImage = null;

    private bool increaseAlpha = false;
    private bool decreaseAlpha = false;

    private void Update()
    {
        Color color = mainBeatImage.color;
        color.a -= Time.deltaTime * speed;
        mainBeatImage.color = color;
    }

    public override void OnBeatCenter()
    {
        Color color = mainBeatImage.color;
        color.a = 1.0f;
        mainBeatImage.color = color;
    }

    public override void OnBeatFinished()
    {

    }

    public override void OnBeatStart()
    {

    }
}
