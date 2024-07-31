using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

// ONLY ONE ACTIVE SELF PER CHILD IN META OBJECTS!!!
public class MetaObject : WorldObject
{
    GameObject activeObject;
    public override WorldObjectJSON json { get; set; } = new();

    bool lateStart = false;

    public GameObject GetActiveObject()
    {
        UpdateActiveObject();
        return activeObject;
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

    // Start is called before the first frame update
    void Start()
    {
    }

    void LateStart()
    {
        json.pos = transform.position;
        json.type = GetActiveObject().GetComponent<WorldObject>().json.type;
        json.ID = GetInstanceID();
        Debug.Log(json.pos);
        Debug.Log(json.type);
        Debug.Log(json.ID);
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
            Debug.Log(json.pos);
            Debug.Log(json.type);
            Debug.Log(json.ID);
        }
    }
}
