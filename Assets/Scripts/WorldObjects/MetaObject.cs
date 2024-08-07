using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

// ONLY ONE ACTIVE SELF PER CHILD IN META OBJECTS!!!
public class MetaObject : WorldObject
{
    GameObject activeObject;

    private WorldObjectJSON internalJSON = new();
    public override WorldObjectJSON json
    {
        get
        {
            WorldObjectJSON activeJSON = GetActiveObject().GetComponent<WorldObject>().json;
            activeJSON.pos = internalJSON.pos;
            return activeJSON;
        }
        set
        {
            Debug.Log("Setting new JSON!");
            UpdateJSON(value);
        }
    }

    [SerializeField]
    GameObject flag;

    [SerializeField]
    GameObject coin;

    [SerializeField]
    GameObject button;

    [SerializeField]
    GameObject greenbutton;

    [SerializeField]
    GameObject bridge;

    [SerializeField]
    GameObject exit;

    bool lateStart = false;

    public GameObject GetActiveObject()
    {
        UpdateActiveObject();
        return activeObject;
    }

    public override void UpdateJSON(WorldObjectJSON newJSON)
    {
        Debug.Log("NEW JSON!");
        Console.WriteLine("Pos: {0}, Type: {1}, ID: {2}", newJSON.pos, newJSON.type, newJSON.ID);
        internalJSON.pos = newJSON.pos;
        internalJSON.ID = newJSON.ID;
        transform.position = newJSON.pos;
        // Type Check
        if (internalJSON.type != newJSON.type)
        {
            // NEW TYPE!
            Debug.Log("NEW TYPE!");
            SwitchObject(newJSON.type);
            internalJSON.type = newJSON.type;
            UpdateActiveObject();
        }
        else
        {
            internalJSON.type = newJSON.type;
        }
    }

    void SwitchObject(TypeEnum newType)
    {
        DisableAll();
        switch (newType)
        {
            case TypeEnum.flag:
                flag.SetActive(true);
                break;
            case TypeEnum.coin:
                coin.SetActive(true);
                break;
            case TypeEnum.redbutton:
                buttonObject = button.GetComponent<ButtonObject>();
                button.SetActive(true);
                break;
            case TypeEnum.greenbutton:
                buttonObject = greenbutton.GetComponent<ButtonObject>();
                greenbutton.SetActive(true);
                break;
            case TypeEnum.bridge:
                bridge.SetActive(true);
                break;
            case TypeEnum.exit:
                exit.SetActive(true);
                break;
            default:
                break;
        }
    }

    void DisableAll()
    {
        for (int x = 0; x < transform.childCount; x++)
        {
            transform.GetChild(x).gameObject.SetActive(false);
        }
    }

    void UpdateActiveObject()
    {
        for (int y = 0; y < transform.childCount; y++)
        {
            GameObject metaChild = transform.GetChild(y).gameObject;
            if (metaChild.activeSelf)
            {
                activeObject = metaChild;
                return;
            }
        }
    }

    void LateStart()
    {
        internalJSON.pos = transform.position;
        internalJSON.type = GetActiveObject().GetComponent<WorldObject>().json.type;
        internalJSON.ID = GetInstanceID();
    }

    // Update is called once per frame
    void Update()
    {
        if (!lateStart)
        {
            lateStart = true;
            LateStart();
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            Debug.Log(internalJSON.pos);
            Debug.Log(internalJSON.type);
            Debug.Log(internalJSON.ID);
        }
    }
}
