using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    GameObject playerPrefab;

    Vector3 pos;

    private void Start()
    {
        pos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            pos.x += 1;
            transform.position = pos;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            pos.x -= 1;
            transform.position = pos;
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            pos.y += 1;
            transform.position = pos;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            pos.y -= 1;
            transform.position = pos;
        }
    }
}
