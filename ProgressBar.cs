using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    public float maximum = 10f;  // ����٧�ش��� ProgressBar ���ʴ�

    public float currentScore1;  // ��һѨ�غѹ�ͧ ProgressBar ����Ѻ Score1
    public float currentScore2;  // ��һѨ�غѹ�ͧ ProgressBar ����Ѻ Score2
    public float currentScore3;  // ��һѨ�غѹ�ͧ ProgressBar ����Ѻ Score3

    public Image maskScore1;     // Image �ͧ ProgressBar ����Ѻ Score1
    public Image maskScore2;     // Image �ͧ ProgressBar ����Ѻ Score2
    public Image maskScore3;     // Image �ͧ ProgressBar ����Ѻ Score3

    private ScoreManager scoreManager; // ��ҧ�ԧ�֧ ScoreManager

    void Start()
    {
        // �� ScoreManager ���
        scoreManager = ScoreManager.instance;
    }

    void Update()
    {
        // �֧��� score1, score2 ��� score3 �ҡ ScoreManager ����
        int score1 = scoreManager.GetScore1();
        int score2 = scoreManager.GetScore2();
        int score3 = scoreManager.GetScore3();

        // ��Ǩ�ͺ Score1 ��Ҥú 30 �����ҡ���� ��� ProgressBar ��ҧ�����ʹ
        if (score1 >= 30)
        {
            currentScore1 = maximum; // ��ҧ ProgressBar ������� maximum
        }
        else
        {
            currentScore1 = score1 % 10; // �ӹǳ����������� score1 % 10
        }

        // ��Ǩ�ͺ Score2 ��Ҥú 30 �����ҡ���� ��� ProgressBar ��ҧ�����ʹ
        if (score2 >= 30)
        {
            currentScore2 = maximum; // ��ҧ ProgressBar ������� maximum
        }
        else
        {
            currentScore2 = score2 % 10; // �ӹǳ����������� score2 % 10
        }

        // ��Ǩ�ͺ Score3 ��Ҥú 30 �����ҡ���� ��� ProgressBar ��ҧ�����ʹ
        if (score3 >= 30)
        {
            currentScore3 = maximum; // ��ҧ ProgressBar ������� maximum
        }
        else
        {
            currentScore3 = score3 % 10; // �ӹǳ����������� score3 % 10
        }

        // �ѻവ����ʴ��� ProgressBar �ͧ���� Score
        GetCurrentFill(maskScore1, currentScore1);
        GetCurrentFill(maskScore2, currentScore2);
        GetCurrentFill(maskScore3, currentScore3);
    }

    void GetCurrentFill(Image mask, float current)
    {
        // �ӹǳ�����繵�ͧ ProgressBar �ҡ current ��� maximum
        float fillAmount = current / maximum;
        mask.fillAmount = fillAmount;
    }
}
