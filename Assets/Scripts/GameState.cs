using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.IO.LowLevel.Unsafe.AsyncReadManagerMetrics;

public static class GameState
{
    public static bool jsonInputOpen = false;

    public static bool playerEditingEnabled = true;

    public static bool flagEditingEnabled = false;

    public static bool gateEditingEnabled = false;

    public static bool buttonEditingEnabled = false;

    static Dictionary<string, bool> buttonFlags = new();

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
        GameObject[] gates = GameObject.FindGameObjectsWithTag("gate");
        foreach (GameObject gate in gates)
        {
            GateObject go = gate.GetComponent<GateObject>();
            if (buttonFlags.ContainsKey(go.gateGroup))
            {
                go.SetClosedState(!buttonFlags[go.gateGroup]);
            }
        }
    }
}
