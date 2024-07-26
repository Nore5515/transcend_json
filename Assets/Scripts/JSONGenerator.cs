using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using TMPro;

public class JSONGenerator : MonoBehaviour
{
    [Serializable]
    public class JSONExport
    {
        public List<PlayerController.PlayerJSON> playerList = new();
        public List<WorldObjectJSON> objectList = new();
        //public PlayerController.PlayerJSON player;
    }

    [SerializeField]
    Tilemap worldTilemap;

    [SerializeField]
    GameObject objectPool;

    [SerializeField]
    GameObject player;

    [SerializeField]
    TMP_InputField jsonField;

    [SerializeField]
    bool playerEditingEnabled = true;

    [SerializeField]
    bool flagEditingEnabled = false;

    [SerializeField]
    bool redGateEditingEnabled = false;

    [SerializeField]
    bool blueGateEditingEnabled = false;

    [SerializeField]
    bool greenGateEditingEnabled = false;

    [SerializeField]
    bool buttonEditingEnabled = false;

    [SerializeField]
    GameObject jsonBlockers = null;

    public HashSet<Vector3> blockerTiles = new();

    JSONExport export = new JSONExport();

    private void Start()
    {
        GameState.playerEditingEnabled = playerEditingEnabled;
        GameState.flagEditingEnabled = flagEditingEnabled;
        GameState.redGateEditingEnabled = redGateEditingEnabled;
        GameState.blueGateEditingEnabled = blueGateEditingEnabled;
        GameState.greenGateEditingEnabled = greenGateEditingEnabled;
        GameState.buttonEditingEnabled = buttonEditingEnabled;

        for (int x = 0; x < jsonBlockers.transform.childCount; x++)
        {
            blockerTiles.Add(jsonBlockers.transform.GetChild(x).transform.position);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GenerateJSON()
    {
        export = new JSONExport();

        if (jsonBlockers != null)
        {
            for (int x = 0; x < jsonBlockers.transform.childCount; x++)
            {
                blockerTiles.Add(jsonBlockers.transform.GetChild(x).transform.position);
            }
        }

        if (GameState.playerEditingEnabled)
        {
            PlayerController.PlayerJSON pJson = player.GetComponent<PlayerController>().json;
            if (blockerTiles.Contains(pJson.pos))
            {
                Debug.Log("No editing! Inside blocker tiles!");
            }
            else
            {
                export.playerList.Add(pJson);
            }
        }
        if (GameState.flagEditingEnabled)
        {
            AddObjectsByTag("flag");
        }
        if (GameState.redGateEditingEnabled)
        {
            AddObjectsByTag("red_gate");
        }
        if (GameState.blueGateEditingEnabled)
        {
            AddObjectsByTag("blue_gate");
        }
        if (GameState.greenGateEditingEnabled)
        {
            AddObjectsByTag("green_gate");
        }
        if (GameState.buttonEditingEnabled)
        {
            AddObjectsByTag("button");
        }
        foreach (WorldObjectJSON json in export.objectList)
        {
            Debug.Log(json.ID);
        }
        string s = JsonUtility.ToJson(export);
        jsonField.text = s;
    }

    void AddObjectsByTag(string tag)
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag(tag);
        foreach (GameObject obj in objs)
        {
            export.objectList.Add(obj.GetComponent<WorldObject>().json);
        }
    }
}
