using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TimerProgressBar : MonoBehaviour
{
    [Header("Time")]
    public float maximumTime = 10f;
    private float currentTime;

    [Header("UI")]
    public Image mask;
    public string sceneToLoad;
    public Text timeText;

    public Animator bulletExplosive;
    public Animator[] explosionAnimators;
    private bool hasExploded = false;
    public float endSceneTime = 0f;

    [Header("Sound Effect")]
    public AudioSource explosionSound;

    void Start()
    {
        currentTime = maximumTime;
        foreach (Animator animator in explosionAnimators)
        {
            animator.gameObject.SetActive(false);
        }
        bulletExplosive.gameObject.SetActive(false);
    }

    void Update()
    {
        if (currentTime > 0)
        {
            currentTime -= Time.deltaTime;
            UpdateProgressBar();
            UpdateTimeText(); 
        }
        if (currentTime <= 0 && !hasExploded)
        {
            TriggerExplosions();
        }
        if(currentTime <= 3 && !hasExploded)
        {
            bulletExplosive.gameObject.SetActive(true);
        }
    }

    void UpdateProgressBar()
    {
        float fillAmount = currentTime / maximumTime;
        mask.fillAmount = fillAmount;
    }

    void UpdateTimeText()
    {
        timeText.text = Mathf.Ceil(currentTime).ToString(); 
    }

    void TriggerExplosions()
    {
        hasExploded = true;
        foreach (Animator animator in explosionAnimators)
        {
            animator.gameObject.SetActive(true);
            animator.SetTrigger("Explode");
            explosionSound.Play();
        }

        Invoke("ChangeScene", endSceneTime);
    }

    void ChangeScene()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}
