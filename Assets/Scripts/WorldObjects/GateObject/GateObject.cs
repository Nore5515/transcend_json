using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateObject : MonoBehaviour
{
    [SerializeField]
    bool closed = true;

    [SerializeField]
    SpriteRenderer gateSprite;

    [SerializeField]
    public string gateGroup;

    // Start is called before the first frame update
    void Start()
    {
        UpdateSpriteState();
    }

    public void SetClosedState(bool newState)
    {
        closed = newState;
        UpdateSpriteState();
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
}
