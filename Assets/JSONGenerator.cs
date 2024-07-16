using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using TMPro;

public class JSONGenerator : MonoBehaviour
{
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
        string s = JsonUtility.ToJson(player.GetComponent<PlayerController>().json);
        jsonField.text = s;
    }
}
