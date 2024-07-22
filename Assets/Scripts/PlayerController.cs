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

    [Serializable]
    public class ObjectPoolJSON
    {

    }

    [SerializeField]
    Tilemap worldTilemap;

    [SerializeField]
    GameObject objectPool;

    [SerializeField]
    string nextLevel;


    Dictionary<Vector3, GameObject> objectDict = new();

    public PlayerJSON json = new PlayerJSON();

    public void UpdateJSON(PlayerJSON newJSON)
    {
        json = newJSON;
        transform.position = json.pos;
    }

    //objectPool.transform.GetChild(x).name

    private void Start()
    {
        json.pos = transform.position;
        int objectCount = objectPool.transform.childCount;
        for (int x = 0; x < objectCount; x++)
        {
            objectDict.Add(objectPool.transform.GetChild(x).transform.position, objectPool.transform.GetChild(x).gameObject);
        }
        foreach (KeyValuePair<Vector3, GameObject> obj in objectDict)
        {
            Debug.Log(obj);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (GameState.jsonInputOpen) return;
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            if (ProcessFutureMovementTile(json.pos + Vector3.right))
            {
                json.pos.x += 1;
                transform.position = json.pos;
            }
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            if (ProcessFutureMovementTile(json.pos - Vector3.right))
            {
                json.pos.x -= 1;
                transform.position = json.pos;
            }
        }
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            if (ProcessFutureMovementTile(json.pos + Vector3.up))
            {
                json.pos.y += 1;
                transform.position = json.pos;
            }
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
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
        switch (objectDict[worldCoord].tag)
        {
            case "flag":
                SceneManager.LoadScene(nextLevel);
                break;
            case "button":
                objectDict[worldCoord].GetComponent<ButtonObject>().SetButtonPressed(true);
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
                break;
        }

        if (objectDict.ContainsKey(worldCoord))
        {
            string tag = objectDict[worldCoord].tag;
            switch (tag)
            {
                case "gate":
                    return false;
                default:
                    break;
            }
        }

        return true;
    }

    void LogTileStandingOn()
    {
        Debug.Log(worldTilemap.GetTile(worldTilemap.WorldToCell(json.pos)));
    }
}
