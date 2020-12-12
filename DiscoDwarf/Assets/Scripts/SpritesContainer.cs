using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpritesContainer : MonoBehaviour
{
    public GameObject distancePoint = null;

    [SerializeField]
    private int layerStep = 5;

    private SpriteRenderer[] renderers;
    private int[] layerDefaultPositions;

    private void Start()
    {
        renderers = GetComponentsInChildren<SpriteRenderer>();

        layerDefaultPositions = new int[renderers.Length];

        for (int i = 0; i < renderers.Length; i++)
            layerDefaultPositions[i] = renderers[i].sortingOrder;
    }

    public void RestoreLayersToDefault()
    {
        for (int i = 0; i < renderers.Length; i++)
            renderers[i].sortingOrder = layerDefaultPositions[i];

        isUp = false;
        isDown = false;
    }

    private bool isUp = false;
    private bool isDown = false;

    public void MoveLayersUp()
    {
        if (isUp)
            return;

        for (int i = 0; i < renderers.Length; i++)
            renderers[i].sortingOrder += layerDefaultPositions[i] + layerStep;

        if (isDown)
        {
            isDown = false;
        }
        else if (!isUp)
            isUp = true;
    }

    public void MoveLayersDown()
    {
        if (isDown)
            return;

        for (int i = 0; i < renderers.Length; i++)
            renderers[i].sortingOrder -= layerDefaultPositions[i] + layerStep;

        if (isUp)
        {
            isUp = false;
        }
        else if (!isDown)
            isDown = true;
    }
}
