using System;
using UnityEngine;

[Serializable]
public class ParsedWorldObjectJSON
{
    public string type;
    public int ID;
    public Vector3 pos;

    public ParsedWorldObjectJSON(WorldObjectJSON objJSON)
    {
        type = objJSON.type.ToString();
        ID = objJSON.ID;
        pos = objJSON.pos;
    }

    public WorldObjectJSON GetWorldObjectJSON()
    {
        WorldObjectJSON json = new();
        string[] types = Enum.GetNames(typeof(TypeEnum));

        bool found = false;
        for (int x = 0; x < types.Length; x++)
        {
            if (types[x] == type)
            {

                json.type = (TypeEnum)x;
                x = types.Length;
                found = true;
            }
        }
        if (!found)
        {
            throw new Exception("Invalid Type");
        }

        json.ID = ID;
        json.pos = pos;
        return json;
    }
}