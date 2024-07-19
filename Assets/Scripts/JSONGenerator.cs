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
        public PlayerController.PlayerJSON player;
    }

    [SerializeField]
    Tilemap worldTilemap;

    [SerializeField]
    GameObject objectPool;

    [SerializeField]
    GameObject player;

    [SerializeField]
    TMP_InputField jsonField;


    // Start is called before the first frame update
    void LateStart()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            GenerateJSON();
        }
    }

    public void GenerateJSON()
    {
        JSONExport export = new JSONExport();
        export.player = player.GetComponent<PlayerController>().json;
        string s = JsonUtility.ToJson(export);
        jsonField.text = s;
    }
}
