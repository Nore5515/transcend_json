using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class BridgeObject : WorldObject
{
    public override WorldObjectJSON json { get; set; } = new();

    // Start is called before the first frame update
    void Start()
    {
        json.pos = transform.position;
        json.type = TypeEnum.bridge;
        json.ID = GetInstanceID();
    }

}
