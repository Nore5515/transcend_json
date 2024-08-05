using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialCameraZoom : MonoBehaviour
{

    bool zoomingOut = false;
    Camera cam;

    const float zoomOutTime = 5.0f;
    float time = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(WaitAndStartZoom());
        cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (zoomingOut)
        {
            if (cam.orthographicSize < 10.0f)
            {
                cam.orthographicSize = Mathf.Lerp(5.0f, 10.0f, time / zoomOutTime);
                time += Time.deltaTime;
            }
            else
            {
                zoomingOut = false;
            }
        }
    }

    IEnumerator WaitAndStartZoom()
    {
        yield return new WaitForSeconds(5.0f);

        zoomingOut = true;
    }
}
