using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    public float maximum = 10f;  // ค่าสูงสุดที่ ProgressBar จะแสดง

    public float currentScore1;  // ค่าปัจจุบันของ ProgressBar สำหรับ Score1
    public float currentScore2;  // ค่าปัจจุบันของ ProgressBar สำหรับ Score2
    public float currentScore3;  // ค่าปัจจุบันของ ProgressBar สำหรับ Score3

    public Image maskScore1;     // Image ของ ProgressBar สำหรับ Score1
    public Image maskScore2;     // Image ของ ProgressBar สำหรับ Score2
    public Image maskScore3;     // Image ของ ProgressBar สำหรับ Score3

    private ScoreManager scoreManager; // อ้างอิงถึง ScoreManager

    void Start()
    {
        // หา ScoreManager ในเกม
        scoreManager = ScoreManager.instance;
    }

    void Update()
    {
        // ดึงค่า score1, score2 และ score3 จาก ScoreManager มาใช้
        int score1 = scoreManager.GetScore1();
        int score2 = scoreManager.GetScore2();
        int score3 = scoreManager.GetScore3();

        // ตรวจสอบ Score1 ถ้าครบ 30 หรือมากกว่า ให้ ProgressBar ค้างเต็มหลอด
        if (score1 >= 30)
        {
            currentScore1 = maximum; // ค้าง ProgressBar ไว้ที่ค่า maximum
        }
        else
        {
            currentScore1 = score1 % 10; // คำนวณค่าใหม่โดยใช้ score1 % 10
        }

        // ตรวจสอบ Score2 ถ้าครบ 30 หรือมากกว่า ให้ ProgressBar ค้างเต็มหลอด
        if (score2 >= 30)
        {
            currentScore2 = maximum; // ค้าง ProgressBar ไว้ที่ค่า maximum
        }
        else
        {
            currentScore2 = score2 % 10; // คำนวณค่าใหม่โดยใช้ score2 % 10
        }

        // ตรวจสอบ Score3 ถ้าครบ 30 หรือมากกว่า ให้ ProgressBar ค้างเต็มหลอด
        if (score3 >= 30)
        {
            currentScore3 = maximum; // ค้าง ProgressBar ไว้ที่ค่า maximum
        }
        else
        {
            currentScore3 = score3 % 10; // คำนวณค่าใหม่โดยใช้ score3 % 10
        }

        // อัปเดตการแสดงผล ProgressBar ของแต่ละ Score
        GetCurrentFill(maskScore1, currentScore1);
        GetCurrentFill(maskScore2, currentScore2);
        GetCurrentFill(maskScore3, currentScore3);
    }

    void GetCurrentFill(Image mask, float current)
    {
        // คำนวณเปอร์เซ็นต์ของ ProgressBar จาก current และ maximum
        float fillAmount = current / maximum;
        mask.fillAmount = fillAmount;
    }
}
