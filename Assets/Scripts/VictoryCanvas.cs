
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class VictoryCanvas : MonoBehaviour
{
    public void RestartGame()
    {
        SceneManager.LoadScene("Level1");
    }

    public void BackToMain()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
