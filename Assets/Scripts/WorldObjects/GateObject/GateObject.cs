using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

//class GateObjectJSON : WorldObjectJSON
//{
//    public bool isClosed;
//}

public class GateObject : WorldObject
{
    [SerializeField]
    public bool closed = true;

    [SerializeField]
    SpriteRenderer gateSprite;

    [SerializeField]
    public string gateGroup;

    //GateObjectJSON gateJSON = new();
    public override WorldObjectJSON json { get; set; } = new();

    [SerializeField]
    public string color;

    public void SetClosedState(bool newState)
    {
        closed = newState;
        UpdateSpriteState();
    }

    // Start is called before the first frame update
    void Start()
    {
        UpdateSpriteState();
        json.pos = transform.position;
        json.type = GetEnumFromString(color);
        gateGroup = color;
        json.ID = GetInstanceID();
        //gateJSON.isClosed = closed;
    }

    void UpdateSpriteState()
    {
        if (closed)
        {
            Color opaque = gateSprite.material.color;
            opaque.a = 1.0f;
            gateSprite.material.color = opaque;
        }
        else
        {
            Color transparent = gateSprite.material.color;
            transparent.a = 0.0f;
            gateSprite.material.color = transparent;
        }
    }

    TypeEnum GetEnumFromString(string s)
    {
        switch (s)
        {
            case "Red":
                return TypeEnum.redgate;
            case "Blue":
                return TypeEnum.bluegate;
            case "Green":
                return TypeEnum.greengate;
            default:
                break;
        }
        throw new System.Exception();
    }


}
