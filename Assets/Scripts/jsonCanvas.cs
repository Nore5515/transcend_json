using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class jsonCanvas : MonoBehaviour
{
    [SerializeField]
    GameObject panel;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            panel.SetActive(!panel.activeSelf);
        }
    }
}
