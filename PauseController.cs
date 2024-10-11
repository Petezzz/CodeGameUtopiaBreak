using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseController : MonoBehaviour
{
    [Header("Pause")]
    private bool isPaused = false;
    public Button pauseButton;
    public Button resumeButton;
    public Button restartButton;

    public GameObject pauseUI;

    void Start()
    {
        pauseButton.onClick.AddListener(pauseGameClick);
        resumeButton.onClick.AddListener(resumeGameClick);
        restartButton.onClick.AddListener(resetGameClick);

        if(!isPaused)
        {
            pauseUI.SetActive(false);
        }
    }

    private void pauseGameClick()
    {
        if (!isPaused)
        {
            Time.timeScale = 0;
            isPaused = true;
            pauseUI.SetActive(true);
        }
    }

    private void resumeGameClick()
    {
        if (isPaused)
        {
            Time.timeScale = 1;
            isPaused = false;
            pauseUI.SetActive(false);
        }
    }

    private void resetGameClick()
    {
        Time.timeScale = 1;
        isPaused = false;
    }
}
