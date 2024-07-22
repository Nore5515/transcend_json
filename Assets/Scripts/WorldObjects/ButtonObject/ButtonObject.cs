using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonObject : MonoBehaviour
{
    [SerializeField]
    GameObject buttonUp;

    [SerializeField]
    GameObject buttonDown;

    [SerializeField]
    bool pressed = false;

    [SerializeField]
    string wallGroup = "1";

    // Start is called before the first frame update
    void Start()
    {
        UpdateButton();
    }

    public void SetButtonPressed(bool state)
    {
        pressed = state;
        UpdateButton();
    }

    void UpdateButton()
    {
        GameState.ToggleButtonFlag(wallGroup, pressed);
        if (pressed)
        {
            buttonUp.SetActive(false);
            buttonDown.SetActive(true);
        }
        else
        {
            buttonUp.SetActive(true);
            buttonDown.SetActive(false);
        }
    }
}
