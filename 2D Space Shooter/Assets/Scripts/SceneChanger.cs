using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("Prototype");
    }

    public void QuitGAme()
    {
        Application.Quit();
    }

    public void Controls()
    {
        SceneManager.LoadScene("Controls");
    }

    public void MEnu()
    {
        SceneManager.LoadScene("Menu");
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Menu");
        }
    }
}
