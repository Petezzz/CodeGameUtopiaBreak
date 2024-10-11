using UnityEngine;
using UnityEngine.SceneManagement;

public class ReloadSceneOnStart : MonoBehaviour
{
    private static bool hasReloaded = false;  // ตัวแปรนี้จะใช้เพื่อตรวจสอบว่าได้รีโหลดไปแล้วหรือยัง

    void Start()
    {
        if (!hasReloaded)
        {
            hasReloaded = true;  // ตั้งค่าว่าเราได้รีโหลดแล้ว
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);  // รีโหลด Scene ปัจจุบัน
        }
        else
        {
            // เริ่มเกมหลังจากรีโหลดไปแล้ว
            Debug.Log("Starting game...");
        }
    }
}
