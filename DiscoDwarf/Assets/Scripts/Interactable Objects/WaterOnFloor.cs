using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterOnFloor : MonoBehaviour
{
    private HUDManager hudManager;
    [SerializeField]
    private float substractValue = 0.2f;

    private int pointBonus = 5;

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
            other.GetComponentInChildren<ItemSlot>().Item.GetComponent<Broom>().PlayBroomingSound();
            hudManager.AddPoints(pointBonus);
            Destroy(this.gameObject, 0.1f);
        }
    }
}
