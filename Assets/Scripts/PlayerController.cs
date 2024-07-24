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
    GameObject objectPool;

    [SerializeField]
    string nextLevel;

    public Dictionary<Vector3, GameObject> objectDict = new();
    public List<WorldObject> worldObjects = new();

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
            worldObjects.Add(objectPool.transform.GetChild(x).GetComponent<WorldObject>());
            //objectDict.Add(objectPool.transform.GetChild(x).transform.position, objectPool.transform.GetChild(x).gameObject);
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
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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
        List<WorldObject> collidingObjs = worldObjects.FindAll(obj => obj.json.pos == worldCoord);
        foreach (WorldObject collidingObj in collidingObjs)
        {
            HandleObjectCollision(collidingObj);
        }
    }

    void HandleObjectCollision(WorldObject collidedObj)
    {
        switch (collidedObj.json.type)
        {
            case "Flag":
                SceneManager.LoadScene(nextLevel);
                break;
            case "Button":
                collidedObj.gameObject.GetComponent<ButtonObject>().SetButtonPressed(true);
                break;
            default:
                break;
        }
    }

    bool IsTileAtVectorObject(Vector3 worldCoord)
    {
        WorldObject collidingObj = worldObjects.Find(obj => obj.json.pos == worldCoord);
        return (collidingObj != null);
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

        List<WorldObject> collidingGate = worldObjects.FindAll(obj => obj.json.pos == worldCoord && obj.json.type.Contains("Gate"));
        foreach (WorldObject gate in collidingGate)
        {
            if (gate.gameObject.GetComponent<GateObject>().closed)
            {
                return false;
            }
        }

        return true;
    }

    void LogTileStandingOn()
    {
        Debug.Log(worldTilemap.GetTile(worldTilemap.WorldToCell(json.pos)));
    }
}
