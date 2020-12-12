using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class WaterSpawner : MonoBehaviour
{
    public GameObject waterToSpawn = null;

    [SerializeField]
    private float timeBetweenSpawns = 1.0f;
    [SerializeField]
    private float chancesToSpawn = 20.0f;

    private float currentSpawnTime = 0.0f;
    private float radius = 0.0f;

    private void Start()
    {
        radius = GetComponent<SphereCollider>().radius;
    }

    private void Update()
    {
        currentSpawnTime += Time.deltaTime;

        if (currentSpawnTime >= timeBetweenSpawns)
        {
            currentSpawnTime = 0.0f;

            if (Random.Range(0, 100) <= chancesToSpawn)
            {
                if (waterToSpawn)
                {
                    GameObject spawnedWater = Instantiate(waterToSpawn);

                    float x = Random.Range(-1.0f, 1.0f);
                    float z = Random.Range(-1.0f, 1.0f);

                    Vector3 position = this.transform.position + (new Vector3(x, 0.0f, z).normalized * radius);

                    spawnedWater.transform.position = position;
                }
            }
        }
    }
}
