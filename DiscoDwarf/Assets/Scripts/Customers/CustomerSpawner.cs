using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] customerPrefabs;

    [SerializeField]
    private float spawnTime;

    private GameObject currentCustomer;
    private float currentTime;

    private void Awake()
    {
        currentTime = Random.Range(0f, spawnTime);
    }

    void Update()
    {
        if(!currentCustomer && currentTime < spawnTime)
        {
            currentTime += Time.deltaTime;
        }
        else
        {
            currentTime = 0f;
            if(!currentCustomer)
                SpawnCustomer();
        }
    }

    private void SpawnCustomer()
    {
        currentCustomer = Instantiate(customerPrefabs[Random.Range(0,customerPrefabs.Length)], transform.position, Quaternion.identity);
    }
}
