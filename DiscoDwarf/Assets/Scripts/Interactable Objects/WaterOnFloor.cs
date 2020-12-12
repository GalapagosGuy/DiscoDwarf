﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterOnFloor : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject);

        if (other.GetComponentInChildren<ItemSlot>().Item.GetComponent<Broom>())
        {
            Destroy(this.gameObject);
        }
    }
}