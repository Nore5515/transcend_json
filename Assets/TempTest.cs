using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TempTest : MonoBehaviour
{
    [SerializeField]
    TMP_Text jsonText;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            jsonText.text = "Cool Text!";
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            jsonText.text = @"Cool Text!\nCool Text!\nCool Text!\nCool Text!\nCool Text!\nCool Text!\nCool Text!\nCool Text!\nCool Text!\nCool Text!\nCool Text!
                            \nCool Text!\nCool Text!\nCool Text!/\nCool Text!\nCool Text!\nCool Text!\nCool Text!\nCool Text!\nCool
                            Text!\nCool Text!\n";
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            jsonText.text = @"CoolooadsladoasdkasjdklasjCoolooadsladoasdkasjdklasjCoolooadsladoasdkasjdklasjCoolooadsladoasdkasjdklasjCoolooadsladoasdkasjdklasj
                             CoolooadsladoasdkasjdklasjCoolooadsladoasdkasjdklasj
                             CoolooadsladoasdkasjdklasjCoolooadsladoasdkasjdklasj";
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            jsonText.text = @"Cool Text!\ndsadasdsadasdsadasdsadasdsadasdsadasdsadasdsadasdsadasdsadasdsadasdsadasdsadasext!\nCool Text!\nCool Text!\nCool Text!\nCool Text!\nCool Text!
                            \nCool Text!\nCool Text!\nCool Textdsadasdsadasdsadasdsadasdsadasdsadasdsadasdsadasdsadasdsadasdsadas!/\nCool Text!\nCool Text!\nCool Text!\nCool Text!\nCool Text!\nCool
                            Text!\nCool Textdsadasdsadasdsadasdsadasdsadasdsadasdsadasdsadasdsadas!\ndsadas";
        }
    }
}
