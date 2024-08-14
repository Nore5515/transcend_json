using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelSelectDropdown : MonoBehaviour
{
    Dictionary<string, string> userLevelToActualLevel = new();
    List<string> levels = new();

    // Start is called before the first frame update
    void Start()
    {
        // Level4.5.unity
        GetComponent<TMP_Dropdown>().ClearOptions();

        for (int x = 4; x < SceneManager.sceneCountInBuildSettings; x++)
        {
            string[] list = SceneUtility.GetScenePathByBuildIndex(x).Split('/');
            string levelDotUnity = list[list.Length - 1];
            string levelName = levelDotUnity.Substring(0, levelDotUnity.Length - 6);
            string userLevelName = "Level " + (x - 3).ToString();
            userLevelToActualLevel.Add(userLevelName, levelName);
            levels.Add(userLevelName);
            Debug.Log(string.Format("Key:{0}, Value:{1}", userLevelName, levelName));
        }
        GetComponent<TMP_Dropdown>().AddOptions(levels);
    }

    public void LoadLevel()
    {
        string key = levels[GetComponent<TMP_Dropdown>().value];
        Debug.Log(string.Format("{0}:{1}", key, userLevelToActualLevel[key]));
        SceneManager.LoadScene(userLevelToActualLevel[key]);
    }
}
