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
            string error = "";
            try
            {
                JSONGenerator.JSONExport export = JsonUtility.FromJson<JSONGenerator.JSONExport>(inputField.text);
                if (export.playerList.Count > 0)
                {
                    if (generator.blockerTiles.Contains(export.playerList[0].pos))
                    {
                        error = "No editing into no-json zone!";
                        throw new Exception();
                    }
                    else
                    {
                        PlayerController.PlayerJSON playerJSON = export.playerList[0];
                        pc.UpdateJSON(playerJSON);
                    }
                }
                if (export.objectList.Count > 0)
                {
                    foreach (WorldObjectJSON worldObjJSON in export.objectList)
                    {
                        // Update actual world objects based on the newly edited json objects

                        foreach (GameObject obj in worldObjects)
                        {
                            WorldObject wObj = obj.GetComponent<WorldObject>();
                            if (!generator.blockerTiles.Contains(worldObjJSON.pos))
                            {
                                if (wObj.json.ID == worldObjJSON.ID)
                                {
                                    wObj.UpdateJSON(worldObjJSON);
                                    break;
                                }
                            }
                            else
                            {
                                error = "No editing into no-json zone!";
                                throw new Exception();
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Debug.Log(e, this);
                if (error != "")
                {
                    parsingErrorText.GetComponent<TMP_Text>().text = "PARSING ERROR!\n" + error;
                }
                else
                {
                    parsingErrorText.GetComponent<TMP_Text>().text = "PARSING ERROR!";
                }
                parsingErrorText.SetActive(true);
            }
        }
        GameState.jsonInputOpen = false;
        panel.SetActive(false);
    }

    void PopulateWorldObjectList()
    {
        populated = true;
        GameObject[] buttons = GameObject.FindGameObjectsWithTag("button");
        GameObject[] flags = GameObject.FindGameObjectsWithTag("flag");
        foreach (GameObject gate in GetGates())
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

    List<GameObject> GetGates()
    {
        GameObject[] redgates = GameObject.FindGameObjectsWithTag("red_gate");
        GameObject[] bluegates = GameObject.FindGameObjectsWithTag("blue_gate");
        GameObject[] greengates = GameObject.FindGameObjectsWithTag("green_gate");
        List<GameObject> gates = new();
        foreach (GameObject red in redgates)
        {
            gates.Add(red);
        }
        foreach (GameObject blue in bluegates)
        {
            gates.Add(blue);
        }
        foreach (GameObject green in greengates)
        {
            gates.Add(green);
        }
        return gates;
    }
}
