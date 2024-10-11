using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircularMovement : MonoBehaviour
{
    [Header("Position")]
    public Transform centerPoint; 
    public float radius = 5.0f;
    public float minSpeed = 1.0f;
    public float maxSpeed = 5.0f;
    public float radiusChangeDuration = 3.0f;
    public float stopDuration = 5.0f;

    [HideInInspector]
    public float angularSpeed;
    [HideInInspector]
    public float angle;

    private float originalRadius;

    [Header("Condition")]
    private bool isRadiusChanging = false;
    private bool isStopped = false;
    private bool isFrozen = false;

    private Vector2 originalPosition;
    [Header("Death Duration")]
    public float deathAnimationDuration = 1f;
    private Animator animator;
    internal float speed;

    [Header("Sound Effect")]
    public AudioSource jumpSound;
    public AudioSource warpSound;
    public AudioSource dieSound;

    void Start()
    {
        if (angularSpeed == 0)
        {
            angularSpeed = Random.Range(minSpeed, maxSpeed);
        }
        originalRadius = radius;

        StartCoroutine(RandomStopCoroutine());

        animator = GetComponent<Animator>();
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
        }

        animator.SetBool("run", isStopped == false);

        
        
    }

    public void WarpToPosition(Vector2 newPosition)
    {
        originalPosition = transform.position;
        transform.position = newPosition;
        StartCoroutine(WarpReturnCoroutine()); 
    }

    private IEnumerator WarpReturnCoroutine()
    {
        isFrozen = true; 
        yield return new WaitForSeconds(5.0f); 
        transform.position = originalPosition;
        isFrozen = false; 
    }

    public void FreezeMovement()
    {
        isFrozen = true;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Box"))
        {
            angularSpeed = -angularSpeed;

            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }
        if (collision.gameObject.CompareTag("Daimond"))
        {
            WarpToRandomPosition();
            warpSound.Play();
            isStopped = false;
        }

        if (collision.gameObject.CompareTag("JumpTag") && !isRadiusChanging && !isStopped)
        {
            StartCoroutine(ChangeRadiusTemporarily());
            jumpSound.Play();
        }

        if (collision.gameObject.CompareTag("Galaxy"))
        {
            StartCoroutine(Die());
            dieSound.Play();
        }

    }

    

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.gameObject.tag == "Box")
        {
            angularSpeed = -angularSpeed;

            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }

        if (other.gameObject.tag == "JumpTag" && !isRadiusChanging && !isStopped)
        {
            StartCoroutine(ChangeRadiusTemporarily());
            jumpSound.Play();
        }

        if(other.gameObject.tag == "Daimond")
        {
            WarpToRandomPosition();
            warpSound.Play();
            isStopped = false;
        }

        if(other.gameObject.tag == "Galaxy")
        {
            StartCoroutine(Die());
            dieSound.Play();
        }

    }

    IEnumerator Die()
    {
        animator.SetTrigger("die");

        yield return new WaitForSeconds(deathAnimationDuration);

        Destroy(gameObject);
    }

    private IEnumerator RotateAndFreezeForDuration(float duration)
    {
        isFrozen = true;
        isStopped = true;
        float timeElapsed = 0f;

        while (timeElapsed < duration)
        {
            Vector3 scale = transform.localScale;
            scale.x = -scale.x;
            transform.localScale = scale;

            yield return new WaitForSeconds(0.5f); 

            timeElapsed += 0.5f; 
        }

        isFrozen = false; 
        isStopped = false;
    }

    private void WarpToRandomPosition()
    {
        float oppositeAngle = angle + Mathf.PI;

        if (oppositeAngle > Mathf.PI * 2)
        {
            oppositeAngle -= Mathf.PI * 2;
        }

        float newX = centerPoint.position.x + radius * Mathf.Cos(oppositeAngle);
        float newY = centerPoint.position.y + radius * Mathf.Sin(oppositeAngle);

        transform.position = new Vector2(newX, newY);

        angle = oppositeAngle;
        if (!isRadiusChanging)
        {
            StartCoroutine(WarpAndRotate());
        }
    }
    private IEnumerator WarpAndRotate()
    {
        yield return new WaitForSeconds(1.5f); 
        StartCoroutine(RotateAndFreezeForDuration(3.0f));
    }

    private IEnumerator ChangeRadiusTemporarily()
    {
        isRadiusChanging = true;
        float newRadius = originalRadius - 3;
        float elapsedTime = 0f;

        while (elapsedTime < radiusChangeDuration / 2)
        {
            radius = Mathf.Lerp(originalRadius, newRadius, elapsedTime / (radiusChangeDuration / 2));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        radius = newRadius;
        elapsedTime = 0f;

        while (elapsedTime < radiusChangeDuration / 2)
        {
            radius = Mathf.Lerp(newRadius, originalRadius, elapsedTime / (radiusChangeDuration / 2));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        radius = originalRadius;
        isRadiusChanging = false;
    }

    private IEnumerator RandomStopCoroutine()
    {
        while (true)
        {
            float waitTime = Random.Range(5.0f, 20.0f);
            yield return new WaitForSeconds(waitTime);

            if (!isRadiusChanging)
            {
                isStopped = true;
                yield return new WaitForSeconds(stopDuration);
                isStopped = false;
            }
        }
    }
}

