using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    Tilemap worldTilemap;

    [SerializeField]
    GameObject playerPrefab;

    Vector3 pos;

    private void Start()
    {
        pos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (IsTileAtVectorWalkable(pos + Vector3.right))
            {
                pos.x += 1;
                transform.position = pos;
            }
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (IsTileAtVectorWalkable(pos - Vector3.right))
            {
                pos.x -= 1;
                transform.position = pos;
            }
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (IsTileAtVectorWalkable(pos + Vector3.up))
            {
                pos.y += 1;
                transform.position = pos;
            }
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (IsTileAtVectorWalkable(pos - Vector3.up))
            {
                pos.y -= 1;
                transform.position = pos;
            }
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            LogTileStandingOn();
        }
    }

    bool IsTileAtVectorWalkable(Vector3 worldCoord)
    {
        string tileName = worldTilemap.GetTile(worldTilemap.WorldToCell(worldCoord)).name;

        switch (tileName)
        {
            case "RedTile":
                return false;
            case "GreenTile":
                return false;
            default:
                return true;
        }
    }

    void LogTileStandingOn()
    {
        Debug.Log(worldTilemap.GetTile(worldTilemap.WorldToCell(pos)));
    }
}
