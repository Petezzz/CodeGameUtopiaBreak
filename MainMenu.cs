using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Cutscene()
    {
        SceneManager.LoadSceneAsync(0);
    }
    public void Menu()
    {
        SceneManager.LoadSceneAsync(1);
    }
    public void HowToPlay2()
    {
        SceneManager.LoadSceneAsync(2);
    }
    public void PlayGame()
    {
        SceneManager.LoadSceneAsync(3);
    }
    public void Win()
    {
        SceneManager.LoadSceneAsync(4);
    }
    public void Lose()
    {
        SceneManager.LoadSceneAsync(5);
    }
    public void Quit()
    {
        Application.Quit();
        //UnityEditor.EditorApplication.isPlaying = false;
    }
    public void ResetGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
