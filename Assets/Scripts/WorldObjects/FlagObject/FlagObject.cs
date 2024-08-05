using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagObject : WorldObject
{
    public override WorldObjectJSON json { get; set; } = new();

    // Start is called before the first frame update
    void Start()
    {
        json.pos = transform.position;
        json.type = TypeEnum.flag;
        json.ID = GetInstanceID();
    }
}
