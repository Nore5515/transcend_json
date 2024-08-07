using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Tilemaps;
using Unity.Burst.CompilerServices;
using System;

public class SideBar : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI textField;

    [SerializeField]
    Tilemap worldTilemap;

    bool slidingLeft = false;
    bool slidingRight = false;

    // Start is called before the first frame update
    void Start()
    {
        worldTilemap = GameObject.Find("WorldTilemap").GetComponent<Tilemap>();
    }

    // Update is called once per frame
    void Update()
    {
        if (slidingLeft)
        {
            if (transform.position.x > -75.0f)
            {
                transform.Translate(new Vector3(-300.0f * Time.deltaTime, 0.0f));
            }
            else
            {
                slidingLeft = false;
            }
        }
        if (slidingRight)
        {
            if (transform.position.x < 0.0f)
            {
                transform.Translate(new Vector3(300.0f * Time.deltaTime, 0.0f));
            }
            else
            {
                slidingRight = false;
            }
        }


        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int tpos = worldTilemap.WorldToCell(mousePos);
        TileBase tile = worldTilemap.GetTile(tpos);
        if (tile != null)
        {
            tpos.x++;
            tpos.y++;
            textField.text = tpos.ToString();
        }
    }

    public void ButtonClick()
    {
        if (gameObject.transform.position.x < 0 && !slidingLeft)
        {
            slidingRight = true;
        }
        if (gameObject.transform.position.x > -75.0f && !slidingRight)
        {
            slidingLeft = true;
        }
    }
}
