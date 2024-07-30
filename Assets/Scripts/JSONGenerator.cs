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
        public List<ParsedWorldObjectJSON> objectList = new();
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
    bool coinEditingEnabled = false;

    [SerializeField]
    List<TypeEnum> editableTypes = new();

    [SerializeField]
    GameObject jsonBlockers = null;

    public HashSet<Vector3> blockerTiles = new();

    JSONExport export = new JSONExport();

    Dictionary<string, bool> enabledTags = new();

    private void Start()
    {
        GameState.playerEditingEnabled = playerEditingEnabled;
        GameState.flagEditingEnabled = flagEditingEnabled;
        GameState.redGateEditingEnabled = redGateEditingEnabled;
        GameState.blueGateEditingEnabled = blueGateEditingEnabled;
        GameState.greenGateEditingEnabled = greenGateEditingEnabled;
        GameState.buttonEditingEnabled = buttonEditingEnabled;
        GameState.coinEditingEnabled = coinEditingEnabled;

        enabledTags.Add("flag", GameState.flagEditingEnabled);
        enabledTags.Add("red_gate", GameState.redGateEditingEnabled);
        enabledTags.Add("blue_gate", GameState.blueGateEditingEnabled);
        enabledTags.Add("green_gate", GameState.greenGateEditingEnabled);
        enabledTags.Add("button", GameState.buttonEditingEnabled);
        enabledTags.Add("coin", GameState.coinEditingEnabled);

        GameState.editableTypes = editableTypes;

        // All these other game state sets are here so why not
        GameState.coins = 0;

        if (jsonBlockers != null)
        {
            for (int x = 0; x < jsonBlockers.transform.childCount; x++)
            {
                blockerTiles.Add(jsonBlockers.transform.GetChild(x).transform.position);
            }
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
            if (!blockerTiles.Contains(pJson.pos))
            {
                export.playerList.Add(pJson);
            }
        }

        foreach (KeyValuePair<string, bool> kvp in enabledTags)
        {
            if (kvp.Value)
            {
                AddObjectsByTag(kvp.Key);
            }
        }

        string s = JsonUtility.ToJson(export);
        jsonField.text = s;
    }

    void AddObjectsByTag(string tag)
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag(tag);
        foreach (GameObject obj in objs)
        {
            WorldObjectJSON wobJSON = obj.GetComponent<WorldObject>().json;
            ParsedWorldObjectJSON parsedJSON = new(wobJSON);
            if (!blockerTiles.Contains(parsedJSON.pos))
            {
                export.objectList.Add(parsedJSON);
            }
        }
    }
}
