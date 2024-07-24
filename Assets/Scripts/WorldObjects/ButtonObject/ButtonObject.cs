using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public class ButtonObjectJSON : WorldObjectJSON
//{
//    bool pressed;
//}

public class ButtonObject : WorldObject
{
    [SerializeField]
    GameObject buttonUp;

    [SerializeField]
    GameObject buttonDown;

    [SerializeField]
    bool pressed = false;

    [SerializeField]
    string wallGroup = "1";

    //ButtonObjectJSON buttonJSON = new();

    public override WorldObjectJSON json { get; set; } = new();

    // Start is called before the first frame update
    void Start()
    {
        json.pos = transform.position;
        json.type = "Button";
        json.ID = GetInstanceID();
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
