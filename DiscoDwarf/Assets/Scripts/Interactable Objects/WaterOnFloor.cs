using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterOnFloor : MonoBehaviour
{
    private HUDManager hudManager;
    [SerializeField]
    private float substractValue = 0.2f;
    private void Awake()
    {
        hudManager = FindObjectOfType<HUDManager>();
    }
    private void Update()
    {
        hudManager.SubstractFromHappyMeter(substractValue * Time.deltaTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponentInChildren<ItemSlot>() && other.GetComponentInChildren<ItemSlot>().Item != null && other.GetComponentInChildren<ItemSlot>().Item.GetComponent<Broom>())
        {
            Destroy(this.gameObject);
        }
    }
}
