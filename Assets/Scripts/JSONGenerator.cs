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
    bool gateEditingEnabled = false;

    [SerializeField]
    bool buttonEditingEnabled = false;


    private void Start()
    {
        GameState.playerEditingEnabled = playerEditingEnabled;
        GameState.flagEditingEnabled = flagEditingEnabled;
        GameState.gateEditingEnabled = gateEditingEnabled;
        GameState.buttonEditingEnabled = buttonEditingEnabled;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GenerateJSON()
    {
        JSONExport export = new JSONExport();
        if (GameState.playerEditingEnabled)
        {
            Debug.Log("player editing enabled");
            export.playerList.Add(player.GetComponent<PlayerController>().json);
            //export.player = player.GetComponent<PlayerController>().json;
        }
        if (GameState.flagEditingEnabled)
        {
            Debug.Log("flag editing enabled");
            GameObject[] flags = GameObject.FindGameObjectsWithTag("flag");
            foreach (GameObject flag in flags)
            {
                Debug.Log("flag found");
                export.objectList.Add(flag.GetComponent<WorldObject>().json);
            }
        }
        if (GameState.gateEditingEnabled)
        {
            GameObject[] gates = GameObject.FindGameObjectsWithTag("gate");
            foreach (GameObject gate in gates)
            {
                Debug.Log("gate found");
                export.objectList.Add(gate.GetComponent<WorldObject>().json);
            }
        }
        if (GameState.buttonEditingEnabled)
        {
            GameObject[] buttons = GameObject.FindGameObjectsWithTag("button");
            foreach (GameObject button in buttons)
            {
                Debug.Log("button found");
                export.objectList.Add(button.GetComponent<WorldObject>().json);
            }
        }
        foreach (WorldObjectJSON json in export.objectList)
        {
            Debug.Log(json.ID);
        }
        string s = JsonUtility.ToJson(export);
        jsonField.text = s;
    }
}
