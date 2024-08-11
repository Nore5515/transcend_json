using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMusic : MonoBehaviour
{
    private void Awake()
    {
        try
        {
            GameObject[] musicObjs = GameObject.FindGameObjectsWithTag("music");
            if (musicObjs.Length > 1)
            {
                Destroy(this.gameObject);
            }
            else
            {
                DontDestroyOnLoad(this.gameObject);
            }
        }
        catch (Exception e)
        {
            Debug.Log(e.Data.ToString());
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
