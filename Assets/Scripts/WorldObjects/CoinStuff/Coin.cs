using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : WorldObject
{
    public override WorldObjectJSON json { get; set; } = new();

    [SerializeField]
    public bool lastCoin = false;

    // Start is called before the first frame update
    void Start()
    {
        json.pos = transform.position;
        json.type = TypeEnum.coin;
        json.ID = GetInstanceID();
    }
}
