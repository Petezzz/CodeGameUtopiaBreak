using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    private int score1;
    private int score2;
    private int score3;

    private int secretScore1 = 0;
    private int secretScore2 = 0;
    private int secretScore3 = 0;

    [Header("WarpPosition")]
    public Transform warpTargetPosition1;
    public Transform warpTargetPosition2;
    public Transform warpTargetPosition3;

    [Header("Character")]
    public CircularMovement characterToWarp1;
    public CircularMovement characterToWarp2;
    public CircularMovement characterToWarp3;

    [Header("Building1")]
    public GameObject building1Lv1;
    public GameObject building1Lv2;
    public GameObject building1LvMax;

    [Header("Building2")]
    public GameObject building2Lv1;
    public GameObject building2Lv2;
    public GameObject building2LvMax;

    [Header("Building3")]
    public GameObject building3Lv1;
    public GameObject building3Lv2;
    public GameObject building3LvMax;

    private bool isPlanet1Lv1Reached = false;
    private bool isPlanet1Lv2Reached = false;
    private bool isPlanet1LvMaxReached = false;

    private bool isPlanet2Lv1Reached = false;
    private bool isPlanet2Lv2Reached = false;
    private bool isPlanet2LvMaxReached = false;

    private bool isPlanet3Lv1Reached = false;
    private bool isPlanet3Lv2Reached = false;
    private bool isPlanet3LvMaxReached = false;

    [Header("Planet1HealthLess")]
    public GameObject Planet1HealthLess1;
    public GameObject Planet1HealthLess2;
    public GameObject Planet1HealthLess3;

    [Header("Planet2HealthLess")]
    public GameObject Planet2HealthLess1;
    public GameObject Planet2HealthLess2;
    public GameObject Planet2HealthLess3;

    [Header("Planet3HealthLess")]
    public GameObject Planet3HealthLess1;
    public GameObject Planet3HealthLess2;
    public GameObject Planet3HealthLess3;

    [Header("Planet1HealthFull")]
    public GameObject Planet1HealthFull1;
    public GameObject Planet1HealthFull2;
    public GameObject Planet1HealthFull3;

    [Header("Planet2HealthFull")]
    public GameObject Planet2HealthFull1;
    public GameObject Planet2HealthFull2;
    public GameObject Planet2HealthFull3;

    [Header("Planet3HealthFull")]
    public GameObject Planet3HealthFull1;
    public GameObject Planet3HealthFull2;
    public GameObject Planet3HealthFull3;

    private bool canAddScore1 = true;
    private bool canAddScore2 = true;
    private bool canAddScore3 = true;

    [Header("MaxScore")]
    private int maxScore1 = 10;
    private int maxScore2 = 10;
    private int maxScore3 = 10;

    [Header("Lighting")]
    public Animator[] lightingAnimators;
    private bool hasLighting = false;
    public AudioSource lightingSound;

    [Header("Scene")]
    public string sceneToLoad;
    public float endSceneTime = 0f;

    [Header("Score Text")]
    public Text scoreText;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        building1Lv1.SetActive(false);
        building1Lv2.SetActive(false);
        building1LvMax.SetActive(false);

        building2Lv1.SetActive(false);
        building2Lv2.SetActive(false);
        building2LvMax.SetActive(false);

        building3Lv1.SetActive(false);
        building3Lv2.SetActive(false);
        building3LvMax.SetActive(false);

        Planet1HealthLess1.SetActive(true);
        Planet1HealthLess2.SetActive(true);
        Planet1HealthLess3.SetActive(true);

        Planet2HealthLess1.SetActive(true);
        Planet2HealthLess2.SetActive(true);
        Planet2HealthLess3.SetActive(true);

        Planet3HealthLess1.SetActive(true);
        Planet3HealthLess2.SetActive(true);
        Planet3HealthLess3.SetActive(true);

        Planet1HealthFull1.SetActive(false);
        Planet1HealthFull2.SetActive(false);
        Planet1HealthFull3.SetActive(false);

        Planet2HealthFull1.SetActive(false);
        Planet2HealthFull2.SetActive(false);
        Planet2HealthFull3.SetActive(false);

        Planet3HealthFull1.SetActive(false);
        Planet3HealthFull2.SetActive(false);
        Planet3HealthFull3.SetActive(false);

        foreach (Animator animator in lightingAnimators)
        {
            animator.gameObject.SetActive(false);
        }
    }
    void Update()
    {
        UpdateScoreText();
    }
    void UpdateScoreText()
    {
        int totalScore = score1 + score2 + score3;
        scoreText.text = "Score: " + totalScore;
    }
    public void AddScore1(int amount)
    {
        if (!canAddScore1 || score1 >= maxScore1) return;
        int potentialNewScore = score1 + amount;

        if (potentialNewScore > maxScore1)
        {
            score1 = maxScore1;
        }
        else
        {
            score1 = potentialNewScore;
        }

        Debug.Log("Score1: " + score1);

        if (score1 % 10 == 0 && score1 > 0)
        {
            canAddScore1 = false;
            WarpDesignatedCharacter1();
            StartCoroutine(IncreaseSecretScoreAfterDelay1(4f));
            StartCoroutine(ResumeScore1AfterDelay(5f));
        }
    }

    private IEnumerator ResumeScore1AfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        canAddScore1 = true;
    }

    public void AddScore2(int amount)
    {
        if (!canAddScore2 || score2 >= maxScore2) return;

        int potentialNewScore = score2 + amount;

        if (potentialNewScore > maxScore2)
        {
            score2 = maxScore2;
        }
        else
        {
            score2 = potentialNewScore;
        }

        Debug.Log("Score2: " + score2);

        if (score2 % 10 == 0 && score2 > 0)
        {
            canAddScore2 = false;
            WarpDesignatedCharacter2();
            StartCoroutine(IncreaseSecretScoreAfterDelay2(4f));
            StartCoroutine(ResumeScore2AfterDelay(5f));
        }
    }

    private IEnumerator ResumeScore2AfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        canAddScore2 = true;
    }

    public void AddScore3(int amount)
    {
        if (!canAddScore3 || score3 >= maxScore3) return;

        int potentialNewScore = score3 + amount;

        if (potentialNewScore > maxScore3)
        {
            score3 = maxScore3;
        }
        else
        {
            score3 = potentialNewScore;
        }

        Debug.Log("Score3: " + score3);

        if (score3 % 10 == 0 && score3 > 0)
        {
            canAddScore3 = false;
            WarpDesignatedCharacter3();
            StartCoroutine(IncreaseSecretScoreAfterDelay3(4f));
            StartCoroutine(ResumeScore3AfterDelay(5f));
        }
    }

    private IEnumerator ResumeScore3AfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        canAddScore3 = true;
    }

    public int GetScore1()
    {
        return score1;
    }
    public int GetScore2()
    {
        return score2;
    }
    public int GetScore3()
    {
        return score3;
    }

    public int GetSecretScore1()
    {
        return secretScore1;
    }
    public int GetSecretScore2()
    {
        return secretScore2;
    }
    public int GetSecretScore3()
    {
        return secretScore3;
    }

    private void WarpDesignatedCharacter1()
    {
        if (characterToWarp1 != null)
        {
            characterToWarp1.WarpToPosition(warpTargetPosition1.position);
            characterToWarp1.FreezeMovement();
        }
        else
        {
            Debug.LogWarning("No character1 assigned for warping.");
        }
    }

    private void WarpDesignatedCharacter2()
    {
        if (characterToWarp2 != null)
        {
            characterToWarp2.WarpToPosition(warpTargetPosition2.position);
            characterToWarp2.FreezeMovement();
        }
        else
        {
            Debug.LogWarning("No character2 assigned for warping.");
        }
    }


    private void WarpDesignatedCharacter3()
    {
        if (characterToWarp3 != null)
        {
            characterToWarp3.WarpToPosition(warpTargetPosition3.position);
            characterToWarp3.FreezeMovement();
        }
        else
        {
            Debug.LogWarning("No character3 assigned for warping.");
        }
    }

    private IEnumerator IncreaseSecretScoreAfterDelay1(float delay)
    {
        yield return new WaitForSeconds(delay);
        secretScore1 += 1;
        Debug.Log("SecretScore1: " + secretScore1);

        if (secretScore1 == 1 && !isPlanet1Lv1Reached)
        {
            Debug.Log("Lv.1");
            building1Lv1.SetActive(true);
            isPlanet1Lv1Reached = true;
            maxScore1 = 20;
            Planet1HealthLess1.SetActive(false);
            Planet1HealthFull1.SetActive(true);
        }
        else if (secretScore1 == 2 && !isPlanet1Lv2Reached)
        {
            Debug.Log("Lv.2");
            building1Lv2.SetActive(true);
            building1Lv1.SetActive(false);
            isPlanet1Lv2Reached = true;
            maxScore1 = 30;
            Planet1HealthLess2.SetActive(false);
            Planet1HealthFull2.SetActive(true);
        }
        else if (secretScore1 == 3 && !isPlanet1LvMaxReached)
        {
            Debug.Log("Lv.Max");
            building1LvMax.SetActive(true);
            building1Lv2.SetActive(false);
            isPlanet1LvMaxReached = true;
            Planet1HealthLess3.SetActive(false);
            Planet1HealthFull3.SetActive(true);

            CheckIfAllBuildingsMaxLevel();
        }
    }
    private IEnumerator IncreaseSecretScoreAfterDelay2(float delay)
    {
        yield return new WaitForSeconds(delay);
        secretScore2 += 1;
        Debug.Log("SecretScore2: " + secretScore2);

        if (secretScore2 == 1 && !isPlanet2Lv1Reached)
        {
            Debug.Log("Lv.1");
            building2Lv1.SetActive(true);
            isPlanet2Lv1Reached = true;
            maxScore2 = 20;
            Planet2HealthLess1.SetActive(false);
            Planet2HealthFull1.SetActive(true);
        }
        else if (secretScore2 == 2 && !isPlanet2Lv2Reached)
        {
            Debug.Log("Lv.2");
            building2Lv2.SetActive(true);
            building2Lv1.SetActive(false);
            isPlanet2Lv2Reached = true;
            maxScore2 = 30;
            Planet2HealthLess2.SetActive(false);
            Planet2HealthFull2.SetActive(true);
        }
        else if (secretScore2 == 3 && !isPlanet2LvMaxReached)
        {
            Debug.Log("Lv.Max");
            building2LvMax.SetActive(true);
            building2Lv2.SetActive(false);
            isPlanet2LvMaxReached = true;
            Planet2HealthLess3.SetActive(false);
            Planet2HealthFull3.SetActive(true);

            CheckIfAllBuildingsMaxLevel();
        }
    }
    private IEnumerator IncreaseSecretScoreAfterDelay3(float delay)
    {
        yield return new WaitForSeconds(delay);
        secretScore3 += 1;
        Debug.Log("SecretScore3: " + secretScore3);

        if (secretScore3 == 1 && !isPlanet3Lv1Reached)
        {
            Debug.Log("Lv.1");
            building3Lv1.SetActive(true);
            isPlanet3Lv1Reached = true;
            maxScore3 = 20;
            Planet3HealthLess1.SetActive(false);
            Planet3HealthFull1.SetActive(true);
        }
        else if (secretScore3 == 2 && !isPlanet3Lv2Reached)
        {
            Debug.Log("Lv.2");
            building3Lv2.SetActive(true);
            building3Lv1.SetActive(false);
            isPlanet3Lv2Reached = true;
            maxScore3 = 30;
            Planet3HealthLess2.SetActive(false);
            Planet3HealthFull2.SetActive(true);
        }
        else if (secretScore3 == 3 && !isPlanet3LvMaxReached)
        {
            Debug.Log("Lv.Max");
            building3LvMax.SetActive(true);
            building3Lv2.SetActive(false);
            isPlanet3LvMaxReached = true;
            Planet3HealthLess3.SetActive(false);
            Planet3HealthFull3.SetActive(true);

            CheckIfAllBuildingsMaxLevel();
        }
    }
    private void CheckIfAllBuildingsMaxLevel()
    {
        if (isPlanet1LvMaxReached && isPlanet2LvMaxReached && isPlanet3LvMaxReached && !hasLighting)
        {
            Debug.Log("All Buildings are at Max Level. Changing Scene...");
            TriggerLighting();
        }
    }

    void TriggerLighting()
    {
        hasLighting = true;
        foreach (Animator animator in lightingAnimators)
        {
            animator.gameObject.SetActive(true);

            lightingSound.Play();
        }

        Invoke("ChangeScene", endSceneTime);
    }
    void ChangeScene()
    {
        SceneManager.LoadScene(sceneToLoad);

    }

}