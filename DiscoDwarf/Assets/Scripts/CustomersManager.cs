using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomersManager : MonoBehaviour
{
    public static CustomersManager Instance = null;

    private void Awake()
    {
        if (CustomersManager.Instance == null)
            CustomersManager.Instance = this;
        else
            Destroy(this);
    }

    [SerializeField]
    private int maxCustomersAtTheSameTime = 8;

    private int spawnedCustomers = 0;

    public bool CanSpawnCustomer()
    {
        return spawnedCustomers < maxCustomersAtTheSameTime;
    }

    public void CustomerSpawned()
    {
        spawnedCustomers++;
    }

    public void CustomerGone()
    {
        spawnedCustomers--;
    }
}
