using UnityEngine;
using UnityEngine.SceneManagement;

public class ReloadSceneOnStart : MonoBehaviour
{
    private static bool hasReloaded = false;  // ����ù��������͵�Ǩ�ͺ���������Ŵ����������ѧ

    void Start()
    {
        if (!hasReloaded)
        {
            hasReloaded = true;  // ��駤��������������Ŵ����
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);  // ����Ŵ Scene �Ѩ�غѹ
        }
        else
        {
            // ���������ѧ�ҡ����Ŵ�����
            Debug.Log("Starting game...");
        }
    }
}
