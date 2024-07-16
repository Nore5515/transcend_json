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

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha7))
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
                    Debug.LogException(e, this);
                }
            }
            panel.SetActive(!panel.activeSelf);
        }
    }
}
