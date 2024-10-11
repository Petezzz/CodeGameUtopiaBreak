using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomBoxGround : MonoBehaviour
{
    public GameObject itemPrefab; // Assign your item prefab in the Inspector
    public float minRadius = 1.0f; // Minimum radius for spawning items
    public float maxRadius = 5.0f; // Maximum radius for spawning items
    public float spawnInterval = 6.0f; // Interval in seconds
    public int itemsPerSpawn = 5; // Number of items to spawn each interval

    void Start()
    {
        StartCoroutine(SpawnAndDestroyItems());
    }

    IEnumerator SpawnAndDestroyItems()
    {
        while (true)
        {
            // Wait for the specified interval
            yield return new WaitForSeconds(spawnInterval);

            for (int i = 0; i < itemsPerSpawn; i++)
            {
                // Randomly choose an angle and radius within the specified range
                float angle = Random.Range(0, 2 * Mathf.PI);
                float radius = Random.Range(minRadius, maxRadius);

                // Calculate the position based on angle and radius
                Vector3 spawnPosition = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * radius;

                // Calculate rotation based on radius
                float zRotation = radius * 10f; // Adjust the multiplier as needed

                // Instantiate the item at the calculated position with the calculated rotation
                GameObject item = Instantiate(itemPrefab, spawnPosition, Quaternion.Euler(0, 0, zRotation));

                // Destroy the item after the spawn interval
                Destroy(item, spawnInterval);
            }
        }
    }
}
