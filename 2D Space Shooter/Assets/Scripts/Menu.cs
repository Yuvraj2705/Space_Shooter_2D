using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] GameObject PauseButton;
    [SerializeField] GameObject PauseMenu;
    [SerializeField] GameObject GameOverMenu;

    void Start()
    {
        PauseButton.SetActive(true);
        PauseMenu.SetActive(false);
        GameOverMenu.SetActive(false);
    }

    public void activatingGameOverMenu()
    {
        Time.timeScale = 0;
        GameOverMenu.SetActive(true);
        PauseButton.SetActive(false);
        PauseMenu.SetActive(false);
    }

    public void OnPressPause()
    {
        Time.timeScale = 0;
        PauseMenu.SetActive(true);
        PauseButton.SetActive(false);
    }

    public void OnPressPlay()
    {
        Time.timeScale = 1;
        PauseMenu.SetActive(false);
        PauseButton.SetActive(true);
    }

    public void OnPressHome()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Menu");
    }

    public void OnPressRepeat()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Prototype");
    }
}
