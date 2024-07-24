using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public class FlagObjectJSON : WorldObjectJSON
//{
//    public string type { get; set; }
//    public int ID { get; set; }
//    public Vector3 pos { get; set; }
//}

public class FlagObject : WorldObject
{
    public override WorldObjectJSON json { get; set; } = new();

    // Start is called before the first frame update
    void Start()
    {
        json.pos = transform.position;
        json.type = "Flag";
        json.ID = GetInstanceID();
    }
}
