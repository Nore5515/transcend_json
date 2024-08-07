using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        json.type = GetEnumFromString(wallGroup + "Button");
        SwitchSprites(wallGroup);
    }

    void SwitchSprites(string colorSprites)
    {
        ToggleOffAllSprites();
        switch (colorSprites)
        {
            case "Red":
                buttonUp = buttonUp_red;
                buttonDown = buttonDown_red;
                break;
            case "Blue":
                buttonUp = buttonUp_blue;
                buttonDown = buttonDown_blue;
                break;
            case "Green":
                buttonUp = buttonUp_green;
                buttonDown = buttonDown_green;
                break;
            default:
                break;
        }
        UpdateButton();
    }

    TypeEnum GetEnumFromString(string s)
    {
        switch (s)
        {
            case "RedButton":
                return TypeEnum.redbutton;
            case "BlueButton":
                return TypeEnum.bluebutton;
            case "GreenButton":
                return TypeEnum.greenbutton;
            default:
                break;
        }
        throw new System.Exception();
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
        GameState.ToggleButtonFlag(wallGroup, pressed);
        UpdateButton();
    }

    void UpdateButton()
    {
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
        string newColor = GetColorFromEnum(newJSON.type);
        //Debug.Log(newColor);
        UpdateWallGroup(newColor);
    }

    string GetColorFromEnum(TypeEnum num)
    {
        Debug.Log("Switching on " + num);
        switch (num)
        {
            case TypeEnum.redbutton:
                return "Red";
            case TypeEnum.bluebutton:
                return "Blue";
            case TypeEnum.greenbutton:
                return "Green";
            default:
                break;
        }
        throw new System.Exception();
    }

}
