using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinGate : WorldObject, GateObjectInterface
{
    public bool closed { get; set; } = true;

    [SerializeField]
    public int coinsToOpen = 1;

    [SerializeField]
    List<Sprite> sprites = new();

    [SerializeField]
    SpriteRenderer gateSprite;

    public override WorldObjectJSON json { get; set; } = new();

    // Start is called before the first frame update
    void Start()
    {
        GameState.coins = 0;
        json.pos = transform.position;
        json.type = TypeEnum.coingate;
        json.ID = GetInstanceID();
        UpdateCoinSprite();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateCoinSprite()
    {
        if (coinsToOpen <= GameState.coins)
        {
            closed = false;
            gameObject.SetActive(false);
        }
        else
        {
            gateSprite.sprite = sprites[(coinsToOpen - GameState.coins) - 1];
        }
    }
}
