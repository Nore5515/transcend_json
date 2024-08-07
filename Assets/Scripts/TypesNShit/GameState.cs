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
    public static bool coinEditingEnabled = false;
    public static bool bridgeEditingEnabled = false;

    public static bool exitEditingEnabled = false;

    static Dictionary<string, bool> buttonFlags = new();

    public static List<TypeEnum> editableTypes = new();

    public static int coins = 0;

    public static Dictionary<string, bool> GetButtonFlags()
    {
        return buttonFlags;
    }

    public static void ResetButtonFlags()
    {
        foreach (string s in new List<string>(buttonFlags.Keys))
        {
            buttonFlags[s] = false;
        }
    }

    public static void ToggleButtonFlag(string flag, bool newState)
    {
        Debug.Log(string.Format("Toggling button flag {0} to {1}", flag, newState));
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

    public static void IncrementCoins()
    {
        coins++;
        GameObject[] coinGates = GameObject.FindGameObjectsWithTag("coin_gate");
        foreach (GameObject coinGate in coinGates)
        {
            coinGate.GetComponent<CoinGate>().UpdateCoinSprite();
        }

    }
}
