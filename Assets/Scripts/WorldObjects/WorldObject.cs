using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class WorldObject : MonoBehaviour
{
    [SerializeField]
    public ButtonObject buttonObject;

    public virtual WorldObjectJSON json { get; set; }

    public virtual void UpdateJSON(WorldObjectJSON newJSON)
    {
        json = newJSON;
        transform.position = json.pos;
    }
}