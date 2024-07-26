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
    GameObject buttonUp_red;
    [SerializeField]
    GameObject buttonDown_red;
    [SerializeField]
    GameObject buttonUp_blue;
    [SerializeField]
    GameObject buttonDown_blue;
    [SerializeField]
    GameObject buttonUp_green;
    [SerializeField]
    GameObject buttonDown_green;

    [SerializeField]
    bool pressed = false;

    [SerializeField]
    string wallGroup = "Red";

    //ButtonObjectJSON buttonJSON = new();

    public override WorldObjectJSON json { get; set; } = new();

    // Start is called before the first frame update
    void Start()
    {
        json.pos = transform.position;
        UpdateWallGroup(wallGroup);
        json.ID = GetInstanceID();
    }

    public void UpdateWallGroup(string newWallGroup)
    {
        wallGroup = newWallGroup;
        SwitchSprites(wallGroup);
        json.type = wallGroup + "Button";
    }

    void SwitchSprites(string colorSprites)
    {
        ToggleOffAllSprites();
        switch (colorSprites)
        {
            case "Red":
                buttonUp = buttonUp_red;
                buttonDown = buttonDown_red;
                //buttonUp_red.SetActive(true);
                //buttonDown_red.SetActive(true);
                break;
            case "Blue":
                buttonUp = buttonUp_blue;
                buttonDown = buttonDown_blue;
                //buttonUp_blue.SetActive(true);
                //buttonDown_blue.SetActive(true);
                break;
            case "Green":
                buttonUp = buttonUp_green;
                buttonDown = buttonDown_green;
                //buttonUp_green.SetActive(true);
                //buttonDown_green.SetActive(true);
                break;
            default:
                break;
        }
        UpdateButton();
    }

    void ToggleOffAllSprites()
    {
        buttonUp_red.SetActive(false);
        buttonDown_red.SetActive(false);
        buttonUp_blue.SetActive(false);
        buttonDown_blue.SetActive(false);
        buttonUp_green.SetActive(false);
        buttonDown_green.SetActive(false);
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

    public override void UpdateJSON(WorldObjectJSON newJSON)
    {
        json = newJSON;
        transform.position = json.pos;
        string newColor = newJSON.type.Substring(0, newJSON.type.IndexOf("Button"));
        Debug.Log(newColor);
        UpdateWallGroup(newColor);
    }
}
