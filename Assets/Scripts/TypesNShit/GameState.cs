using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.IO.LowLevel.Unsafe.AsyncReadManagerMetrics;

public static class GameState
{
    public static bool jsonInputOpen = false;

    public static bool playerEditingEnabled = true;

    public static bool flagEditingEnabled = false;

    public static bool redGateEditingEnabled = false;
    public static bool blueGateEditingEnabled = false;
    public static bool greenGateEditingEnabled = false;

    public static bool buttonEditingEnabled = false;

    static Dictionary<string, bool> buttonFlags = new();

    public static List<TypeEnum> editableTypes = new();

    public static Dictionary<string, bool> GetButtonFlags()
    {
        return buttonFlags;
    }

    public static void ToggleButtonFlag(string flag, bool newState)
    {
        if (!buttonFlags.ContainsKey(flag))
        {
            buttonFlags.Add(flag, newState);
        }
        else
        {
            buttonFlags[flag] = newState;
        }
        UpdateGates();
    }

    static void UpdateGates()
    {
        List<GameObject> gates = GetGates();
        foreach (GameObject gate in gates)
        {
            GateObject go = gate.GetComponent<GateObject>();
            if (buttonFlags.ContainsKey(go.gateGroup))
            {
                go.SetClosedState(!buttonFlags[go.gateGroup]);
            }
        }
    }

    static List<GameObject> GetGates()
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