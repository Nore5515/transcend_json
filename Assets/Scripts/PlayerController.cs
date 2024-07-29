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

    float holdingLeftTime = 0.0f;
    float holdingRightTime = 0.0f;
    float holdingUpTime = 0.0f;
    float holdingDownTime = 0.0f;


    // Update is called once per frame
    void Update()
    {
        if (GameState.jsonInputOpen) return;
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            MoveOnceInDir(Vector3.right);
            holdingRightTime = 0.1f;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            MoveOnceInDir(Vector3.left);
            holdingLeftTime = 0.1f;
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
            default:
                break;
        }
        if (IsButton(collidedObj.json.type))
        {
            collidedObj.gameObject.GetComponent<ButtonObject>().SetButtonPressed(true);
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
            if (gate.gameObject.GetComponent<GateObject>().closed)
            {
                return false;
            }
        }

        return true;
    }

    bool IsGate(TypeEnum type)
    {
        if (type == TypeEnum.redgate || type == TypeEnum.bluegate || type == TypeEnum.greengate)
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
