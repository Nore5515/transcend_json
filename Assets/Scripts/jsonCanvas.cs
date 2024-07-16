using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class jsonCanvas : MonoBehaviour
{
    [SerializeField]
    GameObject panel;

    [SerializeField]
    PlayerController pc;

    [SerializeField]
    TMP_InputField inputField;

    [SerializeField]
    JSONGenerator generator;

    [SerializeField]
    GameObject parsingErrorText;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            panel.SetActive(true);
            generator.GenerateJSON();
            parsingErrorText.SetActive(false);
        }
    }

    public void SubmitPanel()
    {
        if (panel.activeSelf)
        {
            try
            {
                PlayerController.PlayerJSON playerJSON = JsonUtility.FromJson<PlayerController.PlayerJSON>(inputField.text);
                pc.UpdateJSON(playerJSON);
            }
            catch (Exception e)
            {
                Debug.Log(e, this);
                parsingErrorText.SetActive(true);
            }
        }
        panel.SetActive(false);
    }
}
