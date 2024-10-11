using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharacterManager : MonoBehaviour
{
    [Header("CharacterPrefab")]
    public GameObject characterPrefab1; 
    public GameObject characterPrefab2; 
    public GameObject characterPrefab3;

    [Header("CenterPoint")]
    public Transform centerPoint; 
    public float radius1 = 5.0f;

    [Header("MaxAllCharacter")]
    public int initialCharacterCount = 1;

    [Header("MaxCharacter1")]
    public int maxCharacterCount1 = 5;

    [Header("MaxCharacter2")]
    public int maxCharacterCount2 = 5;

    [Header("MaxCharacter3")]
    public int maxCharacterCount3 = 5;

    [Header("RespawnTime")]
    public float spawnInterval = 2.0f; 

    private int currentCharacterCount1;
    private int currentCharacterCount2;
    private int currentCharacterCount3;
    
    private HashSet<float> usedAngles = new HashSet<float>();

    void Start()
    {
        for (int i = 0; i < initialCharacterCount; i++)
        {
            SpawnCharacter1();
        }
        for (int j = 0; j < initialCharacterCount; j++)
        {
            SpawnCharacter2();
        }
        for (int k = 0; k < initialCharacterCount; k++)
        {
            SpawnCharacter3();
        }
        

        StartCoroutine(SpawnCharacterCoroutine1());
        StartCoroutine(SpawnCharacterCoroutine2());
        StartCoroutine(SpawnCharacterCoroutine3());
        
    }

    void SpawnCharacter1()
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

        GameObject character = Instantiate(characterPrefab1, spawnPosition, Quaternion.identity);

        character.transform.parent = transform;

        CircularMovement movement = character.GetComponent<CircularMovement>();
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

        currentCharacterCount1++;
    }
    void SpawnCharacter2()
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

        GameObject character = Instantiate(characterPrefab2, spawnPosition, Quaternion.identity);

        character.transform.parent = transform;

        CircularMovement movement = character.GetComponent<CircularMovement>();
        if (movement != null)
        {
            movement.centerPoint = centerPoint;
            movement.radius = radius1;
            movement.angularSpeed = Random.Range(movement.minSpeed, movement.maxSpeed);
            movement.angle = angleInRadians; // Set the initial angle for circular movement

            if (Random.value > 0.5f)
            {
                Vector3 scale = character.transform.localScale;
                scale.x *= -1;
                character.transform.localScale = scale;

                movement.angularSpeed = -movement.angularSpeed;
            }
        }

        currentCharacterCount2++;
    }
    void SpawnCharacter3()
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

        GameObject character = Instantiate(characterPrefab3, spawnPosition, Quaternion.identity);

        character.transform.parent = transform;

        CircularMovement movement = character.GetComponent<CircularMovement>();
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

        currentCharacterCount3++;
    }

    public void OnCharacterDestroyed1()
    {
        currentCharacterCount1--;
    }
    public void OnCharacterDestroyed2()
    {
        currentCharacterCount2--;
    }
    public void OnCharacterDestroyed3()
    {
        currentCharacterCount3--;
    }
    

    private IEnumerator SpawnCharacterCoroutine1()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);

            if (currentCharacterCount1 < maxCharacterCount1)
            {
                SpawnCharacter1();
            }
        }
    }
    private IEnumerator SpawnCharacterCoroutine2()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);

            if (currentCharacterCount2 < maxCharacterCount2)
            {
                SpawnCharacter2();
            }
        }
    }
    private IEnumerator SpawnCharacterCoroutine3()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);

            if (currentCharacterCount3 < maxCharacterCount3)
            {
                SpawnCharacter3();
            }
        }
    }
}
