using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectScore2 : MonoBehaviour
{
    public AudioSource pickSound;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Pick1"))
        {
            ScoreManager.instance.AddScore2(1);
            Destroy(collision.gameObject);
            pickSound.Play();
        }
        else if (collision.gameObject.CompareTag("Pick2"))
        {
            ScoreManager.instance.AddScore2(2);
            Destroy(collision.gameObject);
            pickSound.Play();
        }
        else if (collision.gameObject.CompareTag("Pick3"))
        {
            ScoreManager.instance.AddScore2(1);
            Destroy(collision.gameObject);
            pickSound.Play();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Pick1"))
        {
            ScoreManager.instance.AddScore2(1);
            Destroy(other.gameObject);
            pickSound.Play();
        }
        else if (other.gameObject.CompareTag("Pick2"))
        {
            ScoreManager.instance.AddScore2(2);
            Destroy(other.gameObject);
            pickSound.Play();
        }
        else if (other.gameObject.CompareTag("Pick3"))
        {
            ScoreManager.instance.AddScore2(1);
            Destroy(other.gameObject);
            pickSound.Play();
        }
    }
}
