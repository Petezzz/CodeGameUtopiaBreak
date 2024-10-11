using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundtrackController : MonoBehaviour
{
    [Header("Audio Source")]
    public AudioSource spaceTrack;
    public AudioSource spaceRushTrack;
    [Header("Time")]
    public float maximumTime = 180f;
    private float currentTime;
    private bool hasSwitchedTrack = false; 

    void Start()
    {
        currentTime = maximumTime;
        spaceTrack.Play();
    }

    void Update()
    {
        if (currentTime > 0)
        {
            currentTime -= Time.deltaTime;
        }

        if (currentTime <= 30 && !hasSwitchedTrack) 
        {
            spaceRushTrack.Play();
            spaceTrack.Stop();
            hasSwitchedTrack = true; 
        }
    }
}
