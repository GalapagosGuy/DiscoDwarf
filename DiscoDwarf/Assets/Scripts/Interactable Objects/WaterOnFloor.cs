using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterOnFloor : MonoBehaviour
{
    public GameObject particleEffect = null;

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
        if(!hudManager.timeStopped)
            hudManager.SubstractFromHappyMeter(substractValue * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponentInChildren<ItemSlot>() && other.GetComponentInChildren<ItemSlot>().Item != null && other.GetComponentInChildren<ItemSlot>().Item.GetComponent<Broom>())
        {
            other.GetComponentInChildren<ItemSlot>().Item.GetComponent<Broom>().PlayBroomingSound();
            hudManager.AddPoints(pointBonus);

            if (particleEffect)
            {
                GameObject ps = Instantiate(particleEffect, this.transform.position, Quaternion.identity);
                Destroy(ps, 3.0f);
            }

            Destroy(this.gameObject, 0.1f);
        }
    }
}
