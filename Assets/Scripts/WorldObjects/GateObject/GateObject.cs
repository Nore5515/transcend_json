using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public interface GateObjectInterface
{
    bool closed { get; set; }
}

public class GateObject : WorldObject, GateObjectInterface
{
    public bool closed { get; set; } = true;

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
        Debug.Log(string.Format("Setting {0} colored gate to {1}", color, newState ? "Closed" : "Open"));
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
