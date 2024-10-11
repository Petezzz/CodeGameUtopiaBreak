using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFOManager : MonoBehaviour
{
    public GameObject characterPrefab;
    public Transform centerPoint;
    public float radius1 = 3.0f;
    public int initialCharacterCount = 1;
    public int maxCharacterCount = 5;
    public float spawnInterval = 2.0f;
    private int currentCharacterCount;

    private HashSet<float> usedAngles = new HashSet<float>();

    void Start()
    {
        for (int i = 0; i < initialCharacterCount; i++)
        {
            SpawnCharacter();
        }
        StartCoroutine(SpawnCharacterCoroutine());

    }

    void SpawnCharacter()
    {
        float angle;
        do
        {
            angle = Random.Range(0f, 360f);
        } while (usedAngles.Contains(angle));

        usedAngles.Add(angle);

        float angleInRadians = angle * Mathf.Deg2Rad;

        Vector2 spawnPosition = new Vector2(
            centerPoint.position.x + radius1 * Mathf.Cos(angleInRadians),
            centerPoint.position.y + radius1 * Mathf.Sin(angleInRadians)
        );

        GameObject character = Instantiate(characterPrefab, spawnPosition, Quaternion.identity);

        character.transform.parent = transform;

        UFOMovement movement = character.GetComponent<UFOMovement>();
        if (movement != null)
        {
            movement.centerPoint = centerPoint;
            movement.radius = radius1;
            movement.angularSpeed = Random.Range(movement.minSpeed, movement.maxSpeed);
            movement.angle = angleInRadians;

            if (Random.value > 0.5f)
            {
                Vector3 scale = character.transform.localScale;
                scale.x *= -1;
                character.transform.localScale = scale;

                movement.angularSpeed = -movement.angularSpeed;
            }
        }

        currentCharacterCount++;
    }

        public void OnCharacterDestroyed1()
    {
        currentCharacterCount--;
    }

    private IEnumerator SpawnCharacterCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);

            if (currentCharacterCount < maxCharacterCount)
            {
                SpawnCharacter();
            }
        }
    }
}
