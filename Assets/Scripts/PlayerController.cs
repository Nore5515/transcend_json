using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;


public class PlayerController : MonoBehaviour
{
    [Serializable]
    public class PlayerJSON
    {
        public Vector3 pos;
    }

    [SerializeField]
    Tilemap worldTilemap;

    [SerializeField]
    GameObject playerPrefab;

    [SerializeField]
    GameObject objectPool;

    Dictionary<Vector3, string> objectDict = new();

    public PlayerJSON json = new PlayerJSON();

    public void UpdateJSON(PlayerJSON newJSON)
    {
        json = newJSON;
        transform.position = json.pos;
    }

    private void Start()
    {
        json.pos = transform.position;
        int objectCount = objectPool.transform.childCount;
        for (int x = 0; x < objectCount; x++)
        {
            objectDict.Add(objectPool.transform.GetChild(x).transform.position, objectPool.transform.GetChild(x).name);
        }
        foreach (KeyValuePair<Vector3, string> obj in objectDict)
        {
            Debug.Log(obj);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (ProcessFutureMovementTile(json.pos + Vector3.right))
            {
                json.pos.x += 1;
                transform.position = json.pos;
            }
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (ProcessFutureMovementTile(json.pos - Vector3.right))
            {
                json.pos.x -= 1;
                transform.position = json.pos;
            }
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (ProcessFutureMovementTile(json.pos + Vector3.up))
            {
                json.pos.y += 1;
                transform.position = json.pos;
            }
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (ProcessFutureMovementTile(json.pos - Vector3.up))
            {
                json.pos.y -= 1;
                transform.position = json.pos;
            }
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            LogTileStandingOn();
        }
    }

    bool ProcessFutureMovementTile(Vector3 worldCoords)
    {
        if (IsTileAtVectorWalkable(worldCoords))
        {
            if (IsTileAtVectorObject(worldCoords))
            {
                ProcessObjectCollision(worldCoords);
            }
            return true;
        }
        return false;
    }

    void ProcessObjectCollision(Vector3 worldCoord)
    {
        switch (objectDict[worldCoord])
        {
            case "Flag":
                SceneManager.LoadScene(0);
                break;
            default:
                break;
        }
    }

    bool IsTileAtVectorObject(Vector3 worldCoord)
    {
        return objectDict.ContainsKey(worldCoord);
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
        Debug.Log(worldTilemap.GetTile(worldTilemap.WorldToCell(json.pos)));
    }
}
