using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicBeatDisplay : MonoBehaviour, IMusicListener
{
    [SerializeField]
    private float speed = 15.0f;

    public Image mainBeatImage = null;
    public Image smallInfoImage = null;

    private bool increaseAlpha = false;
    private bool decreaseAlpha = false;

    private void Update()
    {
        /*if (mainBeatImage)
        {
            Color color = mainBeatImage.color;

            if (increaseAlpha)
                color.a += Time.deltaTime * speed * 2;
            else if (decreaseAlpha)
                color.a -= Time.deltaTime * speed;

            mainBeatImage.color = color;
        }*/

        Color color = mainBeatImage.color;
        color.a -= Time.deltaTime * speed;
        mainBeatImage.color = color;
    }

    public void OnBeatCenter()
    {
        //increaseAlpha = false;
        //decreaseAlpha = true;
        smallInfoImage.color = Color.green;

        Color color = mainBeatImage.color;
        color.a = 1.0f;
        mainBeatImage.color = color;
    }

    public void OnBeatFinished()
    {
        //decreaseAlpha = false;
        //mainBeatImage.gameObject.SetActive(false);

        smallInfoImage.color = Color.black;
    }

    public void OnBeatStart()
    {
        //mainBeatImage.gameObject.SetActive(true);
        //increaseAlpha = true;
        smallInfoImage.color = Color.green;
    }
}
