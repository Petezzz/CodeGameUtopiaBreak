using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFOMovement : MonoBehaviour
{
    public Transform centerPoint;
    public float radius = 5.0f;
    public float minSpeed = 1.0f;
    public float maxSpeed = 5.0f;
    public float stopDuration = 5.0f;

    [HideInInspector]
    public float angularSpeed;
    [HideInInspector]
    public float angle;

    private float originalRadius;
    private bool isStopped = false;
    private bool isFrozen = false;
    private bool isRadiusChanging = false;
    private Vector2 originalPosition;
    internal float speed;
    public GameObject objectToSpawn1;
    public GameObject objectToSpawn2;
    public GameObject objectToSpawn3;
    public Vector3 centerPointDrop;
    public float timeDestroy = 10f;
    public float moveSpeed = 3f;
    public Camera mainCamera;
    public float radius1 = 8.0f;
    private bool itemSpawned = false;

    public float timeRush = 180f;
    private float currentTime;
    void Start()
    {
        if (angularSpeed == 0)
        {
            angularSpeed = Random.Range(minSpeed, maxSpeed);
        }
        originalRadius = radius;

        StartCoroutine(RandomStopCoroutine());

        currentTime = timeRush;
    }

    void Update()
    {
        if (!isStopped && !isFrozen)
        {
            float x = centerPoint.position.x + radius * Mathf.Cos(angle);
            float y = centerPoint.position.y + radius * Mathf.Sin(angle);

            transform.position = new Vector2(x, y);

            Vector2 direction = new Vector2(-Mathf.Sin(angle), Mathf.Cos(angle));
            float angleDegrees = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angleDegrees));

            angle += angularSpeed * Time.deltaTime;

            if (angle > Mathf.PI * 2)
            {
                angle -= Mathf.PI * 2;
            }
            else if (angle < 0)
            {
                angle += Mathf.PI * 2;
            }
            itemSpawned = false;
        }
        else
        {
            if (!itemSpawned)
            {
                Vector3 worldPosition = this.gameObject.transform.position;
                worldPosition.z = 0;
                float distanceFromCenter = Vector3.Distance(worldPosition, centerPointDrop);
                Vector3 direction = (worldPosition - centerPointDrop).normalized;
                Vector3 targetPosition = centerPointDrop + direction * radius1;

                GameObject[] objectsToSpawn = { objectToSpawn1, objectToSpawn2, objectToSpawn3 };
                GameObject objectToSpawn = objectsToSpawn[Random.Range(0, objectsToSpawn.Length)];

                GameObject spawnedObject = Instantiate(objectToSpawn, worldPosition, Quaternion.identity);
                StartCoroutine(MoveToPosition(spawnedObject, targetPosition, direction));
                Destroy(spawnedObject, timeDestroy);

                itemSpawned = true; // Set the flag to true to prevent further spawning
            }
        }

        if(currentTime > 0)
        {
            currentTime -= Time.deltaTime;
        }
    }

    IEnumerator MoveToPosition(GameObject obj, Vector3 targetPosition, Vector3 direction)
    {
        Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();
        if (rb == null || obj == null)
        {
            yield break;
        }

        rb.velocity = direction * moveSpeed;

        while (obj != null && Vector3.Distance(obj.transform.position, targetPosition) > 0.1f)
        {
            if (obj == null) yield break;

            // Calculate the rotation
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            obj.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

            yield return null;
        }

        if (obj != null)
        {
            rb.velocity = Vector2.zero;
            obj.transform.position = targetPosition;
        }
    }

    public void FreezeMovement()
    {
        isFrozen = true;
    }
    private IEnumerator RandomStopCoroutine()
    {
        while (true)
        {
            float waitTime;
            if (currentTime <= 30)
            {
                // Reduce the wait time when time is <= 30 seconds to make stops more frequent
                waitTime = Random.Range(5.0f, 10.0f);
            }
            else
            {
                // Normal stop frequency
                waitTime = Random.Range(7.0f, 20.0f);
            }

            yield return new WaitForSeconds(waitTime);

            // Stop the UFO only if it is not in the process of radius change
            if (!isRadiusChanging)
            {
                isStopped = true;
                yield return new WaitForSeconds(stopDuration);
                isStopped = false;
            }
        }
    }
}
