using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Skill : MonoBehaviour
{
    [Header("Prefab")]
    public Camera mainCamera;
    public GameObject skill1Prefab;
    public GameObject skill2Prefab;
    public GameObject skill2OppositePrefab;
    public GameObject skill3Prefab;

    [Header("Game Control")]
    public Transform centerPoint;
    public float radius = 5f;
    public float radiusSkill2 = 6.3f;
    public float moveSpeed = 2f;

    [Header("Skill Cooldown Time")]
    public float skill1Cooldown = 3f;
    public float skill2Cooldown = 7f;
    public float skill3Cooldown = 30f;

    private bool canUseSkill1 = true;
    private bool canUseSkill2 = true;
    private bool canUseSkill3 = true;

    [Header("Point Text")]
    private int skillUsageCount = 0; 
    private int upgradePoints = 0;
    public Text pointsText;

    [Header("Skill Button")]
    public Button skill1Button;
    public Button skill2Button;
    public Button skill3Button;

    [Header("Upgrade Button")]
    public Button upgradeButton1;
    public Button upgradeButton2;
    public Button upgradeButton3;

    [Header("Skill Cooldown Text")]
    public Text skill1CooldownText;
    public Text skill2CooldownText;
    public Text skill3CooldownText;

    [Header("Cooldown Image")]
    public Image skill1CooldownImage;
    public Image skill2CooldownImage;
    public Image skill3CooldownImage;

    [Header("BarSkill1")]
    public GameObject bar1Skill1Black;
    public GameObject bar1Skill1White;
    public GameObject bar2Skill1Black;
    public GameObject bar2Skill1White;
    public GameObject bar3Skill1Black;
    public GameObject bar3Skill1White;
    [Header("BarSkill2")]
    public GameObject bar1Skill2Black;
    public GameObject bar1Skill2White;
    public GameObject bar2Skill2Black;
    public GameObject bar2Skill2White;
    public GameObject bar3Skill2Black;
    public GameObject bar3Skill2White;
    [Header("BarSkill3")]
    public GameObject bar1Skill3Black;
    public GameObject bar1Skill3White;
    public GameObject bar2Skill3Black;
    public GameObject bar2Skill3White;
    public GameObject bar3Skill3Black;
    public GameObject bar3Skill3White;

    [Header("Skill Window")]
    public GameObject skillWindow;
    public Button closeButton;
    public Button openButton;
    private bool isWindowVisible = true;

    [Header("Image Select Skill")]
    public Image skill1Image;
    public Image skill2Image;
    public Image skill3Image;
    private int selectedSkill = 0;

    void Start()
    {
        skill1Button.onClick.AddListener(() => SelectSkill(1));
        skill2Button.onClick.AddListener(() => SelectSkill(2));
        skill3Button.onClick.AddListener(() => SelectSkill(3));

        upgradeButton1.onClick.AddListener(UpgradeSkill1);
        upgradeButton2.onClick.AddListener(UpgradeSkill2);
        upgradeButton3.onClick.AddListener(UpgradeSkill3);

        skill1CooldownText.gameObject.SetActive(false);
        skill2CooldownText.gameObject.SetActive(false);
        skill3CooldownText.gameObject.SetActive(false);

        skill1CooldownImage.gameObject.SetActive(false);
        skill2CooldownImage.gameObject.SetActive(false);
        skill3CooldownImage.gameObject.SetActive(false);

        SetInitialBarStates();

        pointsText.text = "Points: 0";

        closeButton.onClick.AddListener(closeButtonClick);
        openButton.onClick.AddListener(openButtonClick);

        selectedSkill = 1;
        SetImageOpacity(skill1Image, 0);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && selectedSkill != 0)
        {
            if (selectedSkill == 1 && canUseSkill1)
            {
                UseSkill(1);
            }
            else if (selectedSkill == 2 && canUseSkill2)
            {
                UseSkill(2);
            }
            else if (selectedSkill == 3 && canUseSkill3)
            {
                UseSkill(3);
            }
        }
    }
    private void closeButtonClick()
    {
        if (isWindowVisible)
        {
            skillWindow.SetActive(false);
            isWindowVisible = false;
        }
    }
    private void openButtonClick()
    {
        if (!isWindowVisible)
        {
            skillWindow.SetActive(true);
            isWindowVisible = true;
        }
    }
    void UseSkill(int skillNumber)
    {
        Vector3 mousePosition = Input.mousePosition;
        Vector3 worldPosition = mainCamera.ScreenToWorldPoint(mousePosition);
        worldPosition.z = 0;

        float currentRadius = radius;
        if (skillNumber == 2)
        {
            currentRadius = radiusSkill2;
        }

        float distanceFromCenter = Vector3.Distance(worldPosition, centerPoint.position);

        if (distanceFromCenter <= currentRadius)
        {
            Vector3 direction = (worldPosition - centerPoint.position).normalized;
            Vector3 targetPosition = centerPoint.position + direction * currentRadius;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.Euler(new Vector3(0, 0, angle));

            GameObject spawnedObject = null;
            float destroyTime = 0f;

            switch (skillNumber)
            {
                case 1:
                    spawnedObject = Instantiate(skill1Prefab, worldPosition, rotation);
                    StartCoroutine(CooldownSkill(1, skill1Cooldown, skill1CooldownText, skill1CooldownImage));
                    destroyTime = 5f;
                    break;
                case 2:
                    spawnedObject = Instantiate(skill2Prefab, worldPosition, rotation);

                    Vector3 oppositeDirection = -direction; 
                    Vector3 oppositePosition = centerPoint.position + oppositeDirection * currentRadius;
                    float oppositeAngle = Mathf.Atan2(oppositeDirection.y, oppositeDirection.x) * Mathf.Rad2Deg;
                    Quaternion oppositeRotation = Quaternion.Euler(new Vector3(0, 0, oppositeAngle));

                    GameObject oppositeObject = Instantiate(skill2OppositePrefab, oppositePosition, oppositeRotation);

                    StartCoroutine(CooldownSkill(2, skill2Cooldown, skill2CooldownText, skill2CooldownImage));

                    destroyTime = 6f;

                    if (spawnedObject != null)
                    {
                        StartCoroutine(MoveToPosition(spawnedObject, targetPosition, direction));
                        Destroy(spawnedObject, destroyTime);
                    }
                    if (oppositeObject != null)
                    {
                        StartCoroutine(MoveToPosition(oppositeObject, oppositePosition, oppositeDirection));
                        Destroy(oppositeObject, destroyTime);
                    }
                    break;

                case 3:
                    spawnedObject = Instantiate(skill3Prefab, worldPosition, rotation);
                    StartCoroutine(CooldownSkill(3, skill3Cooldown, skill3CooldownText, skill3CooldownImage));
                    destroyTime = 5f;
                    break;
            }

            if (spawnedObject != null)
            {
                StartCoroutine(MoveToPosition(spawnedObject, targetPosition, direction));
                Destroy(spawnedObject, destroyTime);
            }

            skillUsageCount++; 

            if (skillUsageCount % 5 == 0)
            {
                upgradePoints++; 
                UpgradePointText();
                Debug.Log("Upgrade Points: " + upgradePoints);
            }
        }
    }

    private void UpgradePointText()
    {
        pointsText.text = "Points: " + upgradePoints;
    }

    IEnumerator CooldownSkill(int skillNumber, float cooldownTime, Text cooldownText, Image cooldownImage)
    {
        cooldownText.gameObject.SetActive(true);
        cooldownImage.gameObject.SetActive(true); 
        cooldownImage.fillAmount = 1;

        switch (skillNumber)
        {
            case 1:
                canUseSkill1 = false;
                break;
            case 2:
                canUseSkill2 = false;
                break;
            case 3:
                canUseSkill3 = false;
                break;
        }

        float timeRemaining = cooldownTime;

        while (timeRemaining > 0)
        {
            cooldownText.text = timeRemaining.ToString("F1");
            cooldownImage.fillAmount = timeRemaining / cooldownTime;
            yield return new WaitForSeconds(0.1f);
            timeRemaining -= 0.1f;
        }

        cooldownText.gameObject.SetActive(false);
        cooldownImage.gameObject.SetActive(false); 

        switch (skillNumber)
        {
            case 1:
                canUseSkill1 = true;
                break;
            case 2:
                canUseSkill2 = true;
                break;
            case 3:
                canUseSkill3 = true;
                break;
        }
    }

    void SelectSkill(int skillNumber)
    {
        selectedSkill = skillNumber;
        Debug.Log("Selected Skill: " + skillNumber);

        SetImageOpacity(skill1Image, 160); 
        SetImageOpacity(skill2Image, 160); 
        SetImageOpacity(skill3Image, 160); 

        switch (selectedSkill)
        {
            case 1:
                SetImageOpacity(skill1Image, 0); 
                break;
            case 2:
                SetImageOpacity(skill2Image, 0);
                break;
            case 3:
                SetImageOpacity(skill3Image, 0);
                break;
        }
    }

    void SetImageOpacity(Image image, float alpha)
    {
        Color tempColor = image.color;
        tempColor.a = alpha / 255f; 
        image.color = tempColor;
    }

    void SetInitialBarStates()
    {
        bar1Skill1Black.SetActive(true);
        bar1Skill1White.SetActive(false);
        bar2Skill1Black.SetActive(true);
        bar2Skill1White.SetActive(false);
        bar3Skill1Black.SetActive(true);
        bar3Skill1White.SetActive(false);

        bar1Skill2Black.SetActive(true);
        bar1Skill2White.SetActive(false);
        bar2Skill2Black.SetActive(true);
        bar2Skill2White.SetActive(false);
        bar3Skill2Black.SetActive(true);
        bar3Skill2White.SetActive(false);

        bar1Skill3Black.SetActive(true);
        bar1Skill3White.SetActive(false);
        bar2Skill3Black.SetActive(true);
        bar2Skill3White.SetActive(false);
        bar3Skill3Black.SetActive(true);
        bar3Skill3White.SetActive(false);
    }

    void UpgradeSkill1()
    {
        if (upgradePoints > 0 && skill1Cooldown == 3f)
        {
            skill1Cooldown -= 0.5f;
            bar1Skill1Black.SetActive(false);
            bar1Skill1White.SetActive(true);

            upgradePoints--;
            UpgradePointText();
        }
        else
        {
            Debug.Log("Not enough upgrade points!");
        }
        if (upgradePoints > 0 && skill1Cooldown == 2.5f)
        {
            skill1Cooldown -= 0.5f;
            bar2Skill1Black.SetActive(false);
            bar2Skill1White.SetActive(true);

            upgradePoints--;
            UpgradePointText();
        }
        else
        {
            Debug.Log("Not enough upgrade points!");
        }
        if (upgradePoints > 0 && skill1Cooldown == 2f)
        {
            skill1Cooldown-= 0.5f;
            bar3Skill1Black.SetActive(false);
            bar3Skill1White.SetActive(true);

            upgradePoints--;
            UpgradePointText();
        }
        else
        {
            Debug.Log("Not enough upgrade points!");
        }
    }
    void UpgradeSkill2()
    {
        if (upgradePoints > 0 && skill2Cooldown == 7)
        {
            skill2Cooldown--;

            bar1Skill2Black.SetActive(false);
            bar1Skill2White.SetActive(true);

            upgradePoints--;
            UpgradePointText();
        }
        else
        {
            Debug.Log("Not enough upgrade points!");
        }
        if (upgradePoints > 0 && skill2Cooldown == 6) 
        {
            skill2Cooldown--;

            bar2Skill2Black.SetActive(false);
            bar2Skill2White.SetActive(true);

            upgradePoints--;
            UpgradePointText();
        }
        else
        {
            Debug.Log("Not enough upgrade points!");
        }
        if (upgradePoints > 0 && skill2Cooldown == 5) 
        {
            skill2Cooldown--;

            bar3Skill2Black.SetActive(false);
            bar3Skill2White.SetActive(true);

            upgradePoints--;
            UpgradePointText();
        }
        else
        {
            Debug.Log("Not enough upgrade points!");
        }
    }
    void UpgradeSkill3()
    {
        if (upgradePoints > 0 && skill3Cooldown == 30) 
        {
            skill3Cooldown -= 5;

            bar1Skill3Black.SetActive(false);
            bar1Skill3White.SetActive(true);

            upgradePoints--;
            UpgradePointText();
        }
        else
        {
            Debug.Log("Not enough upgrade points!");
        }
        if (upgradePoints > 0 && skill3Cooldown == 25)
        {
            skill3Cooldown -= 5;

            bar2Skill3Black.SetActive(false);
            bar2Skill3White.SetActive(true);

            upgradePoints--;
            UpgradePointText();
        }
        else
        {
            Debug.Log("Not enough upgrade points!");
        }
        if (upgradePoints > 0 && skill3Cooldown == 20)
        {
            skill3Cooldown -= 5;

            bar3Skill3Black.SetActive(false);
            bar3Skill3White.SetActive(true);

            upgradePoints--;
            UpgradePointText();
        }
        else
        {
            Debug.Log("Not enough upgrade points!");
        }
    }

    IEnumerator MoveToPosition(GameObject spawnedObject, Vector3 targetPosition, Vector3 direction)
    {
        while (Vector3.Distance(spawnedObject.transform.position, targetPosition) > 0.1f)
        {
            spawnedObject.transform.position += direction * moveSpeed * Time.deltaTime;
            yield return null;
        }
        spawnedObject.transform.position = targetPosition;
    }
}