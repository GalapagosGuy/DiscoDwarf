using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterOnFloor : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponentInChildren<ItemSlot>() && other.GetComponentInChildren<ItemSlot>().Item != null && other.GetComponentInChildren<ItemSlot>().Item.GetComponent<Broom>())
        {
            Destroy(this.gameObject);
        }
    }
}
