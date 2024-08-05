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
            Transform child = objectPool.transform.GetChild(x);
            if (child.tag == "meta_object")
            {
                //GameObject metaChild = child.GetComponent<MetaObject>().GetActiveObject();
                //worldObjects.Add(metaChild.GetComponent<WorldObject>());
                worldObjects.Add(child.GetComponent<WorldObject>());
            }
            else
            {
                WorldObject obj = child.GetComponent<WorldObject>();
                if (obj != null)
                {
                    worldObjects.Add(obj);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (GameState.jsonInputOpen) return;
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            MoveOnceInDir(Vector3.right);
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            MoveOnceInDir(Vector3.left);
        }
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            MoveOnceInDir(Vector3.up);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            MoveOnceInDir(-Vector3.up);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    void MoveOnceInDir(Vector3 moveVec)
    {
        if (ProcessFutureMovementTile(json.pos + moveVec))
        {
            json.pos.x += moveVec.x;
            json.pos.y += moveVec.y;
            transform.position = json.pos;
        }
    }

    bool ProcessFutureMovementTile(Vector3 worldCoords)
    {
        CleanupObjects();
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
            case TypeEnum.flag:
                SceneManager.LoadScene(nextLevel);
                break;
            case TypeEnum.coin:
                GameState.IncrementCoins();
                Destroy(collidedObj.gameObject);
                return;
            default:
                break;
        }
        if (IsButton(collidedObj.json.type))
        {
            collidedObj.buttonObject.SetButtonPressed(true);
        }
    }

    bool IsButton(TypeEnum type)
    {
        if (type == TypeEnum.redbutton || type == TypeEnum.bluebutton || type == TypeEnum.greenbutton)
        {
            return true;
        }
        return false;
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

        List<WorldObject> collidingGate = worldObjects.FindAll(obj => obj.json.pos == worldCoord && IsGate(obj.json.type));
        foreach (WorldObject gate in collidingGate)
        {
            if (gate.gameObject.GetComponent<GateObjectInterface>().closed)
            {
                return false;
            }
        }

        return true;
    }

    void CleanupObjects()
    {
        for (int x = 0; x < worldObjects.Count; x++)
        {
            if (worldObjects[x] == null)
            {
                worldObjects.RemoveAt(x);
            }
        }
    }

    bool IsGate(TypeEnum type)
    {
        if (type == TypeEnum.redgate || type == TypeEnum.bluegate || type == TypeEnum.greengate || type == TypeEnum.coingate)
        {
            return true;
        }
        return false;
    }

    void LogTileStandingOn()
    {
        Debug.Log(worldTilemap.GetTile(worldTilemap.WorldToCell(json.pos)));
    }
}
