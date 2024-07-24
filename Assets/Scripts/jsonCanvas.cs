using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class jsonCanvas : MonoBehaviour
{
    [SerializeField]
    GameObject panel;

    [SerializeField]
    PlayerController pc;

    [SerializeField]
    TMP_InputField inputField;

    [SerializeField]
    JSONGenerator generator;

    [SerializeField]
    GameObject parsingErrorText;

    List<GameObject> worldObjects = new();
    bool populated = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!panel.activeSelf)
            {
                GameState.jsonInputOpen = true;
                panel.SetActive(true);
                generator.GenerateJSON();
                parsingErrorText.SetActive(false);
            }
            else
            {
                GameState.jsonInputOpen = false;
                panel.SetActive(false);
            }
        }
    }

    public void SubmitPanel()
    {
        if (worldObjects.Count == 0 && !populated)
        {
            PopulateWorldObjectList();
        }

        if (panel.activeSelf)
        {
            try
            {
                JSONGenerator.JSONExport export = JsonUtility.FromJson<JSONGenerator.JSONExport>(inputField.text);
                if (export.playerList.Count > 0)
                {
                    PlayerController.PlayerJSON playerJSON = export.playerList[0];
                    pc.UpdateJSON(playerJSON);
                }
                if (export.objectList.Count > 0)
                {
                    foreach (WorldObjectJSON worldObjJSON in export.objectList)
                    {
                        // Update actual world objects based on the newly edited json objects

                        foreach (GameObject obj in worldObjects)
                        {
                            WorldObject wObj = obj.GetComponent<WorldObject>();
                            if (wObj.json.ID == worldObjJSON.ID)
                            {
                                wObj.UpdateJSON(worldObjJSON);
                                break;
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Debug.Log(e, this);
                parsingErrorText.SetActive(true);
            }
        }
        GameState.jsonInputOpen = false;
        panel.SetActive(false);
    }

    void PopulateWorldObjectList()
    {
        populated = true;
        GameObject[] gates = GameObject.FindGameObjectsWithTag("gate");
        GameObject[] buttons = GameObject.FindGameObjectsWithTag("button");
        GameObject[] flags = GameObject.FindGameObjectsWithTag("flag");
        foreach (GameObject gate in gates)
        {
            worldObjects.Add(gate);
        }
        foreach (GameObject button in buttons)
        {
            worldObjects.Add(button);
        }
        foreach (GameObject flag in flags)
        {
            worldObjects.Add(flag);
        }
    }
}
