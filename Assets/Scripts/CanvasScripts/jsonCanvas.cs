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
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameState.jsonInputOpen = false;
            panel.SetActive(false);
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
                    if (generator.blockerTiles.Contains(export.playerList[0].pos))
                    {
                        throw new Exception("No editing into no-json zone!");
                    }
                    else
                    {
                        PlayerController.PlayerJSON playerJSON = export.playerList[0];
                        pc.UpdateJSON(playerJSON);
                    }
                }
                if (export.objectList.Count > 0)
                {
                    foreach (ParsedWorldObjectJSON importedWorldObjJSON in export.objectList)
                    {
                        Console.WriteLine("Handling parsed object {0}", importedWorldObjJSON.ID.ToString());
                        if (importedWorldObjJSON == null)
                        {
                            continue;
                        }
                        // Update actual world objects based on the newly edited json objects
                        WorldObjectJSON unparsedObjJSON = importedWorldObjJSON.GetWorldObjectJSON();
                        //Debug.Log(JsonUtility.ToJson(unparsedObjJSON));

                        foreach (GameObject obj in worldObjects)
                        {
                            if (generator.blockerTiles.Contains(unparsedObjJSON.pos))
                            {
                                throw new Exception("No editing into no-json zone!");
                            }

                            WorldObject wObj = obj.GetComponent<WorldObject>();
                            if (wObj.json.ID == unparsedObjJSON.ID)
                            {
                                Console.WriteLine("Found matching ID {0}", wObj.json.ID);
                                if (wObj.json.type != unparsedObjJSON.type)
                                {
                                    if (!GameState.editableTypes.Contains(unparsedObjJSON.type) || !GameState.editableTypes.Contains(wObj.json.type))
                                    {
                                        throw new Exception("Invalid Permission: Type Edit");

                                    }
                                }
                                wObj.UpdateJSON(unparsedObjJSON);
                                break;
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                parsingErrorText.GetComponent<TMP_Text>().text = "PARSING ERROR!\n" + e.Message;
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
        GameObject[] coins = GameObject.FindGameObjectsWithTag("coin");
        GameObject[] metas = GameObject.FindGameObjectsWithTag("meta_object");
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
        foreach (GameObject coin in coins)
        {
            worldObjects.Add(coin);
        }
        foreach (GameObject meta in metas)
        {
            worldObjects.Add(meta);
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
