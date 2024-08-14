using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class DarkFadeout : MonoBehaviour
{
    bool undarking = false;
    float time = 0.0f;
    float fadeoutSeconds = 2.0f;

    [SerializeField]
    Image dark;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartDarkness());
    }

    // Update is called once per frame
    void Update()
    {
        if (undarking)
        {
            Debug.Log("Blacking out!" + dark.color.a);
            if (dark.color.a > 0)
            {
                Color c = dark.color;
                c.a = Mathf.Lerp(1.0f, 0.0f, time / fadeoutSeconds);
                time += Time.deltaTime;
                dark.color = c;
            }
            else
            {
                undarking = false;
                Destroy(gameObject);
            }
        }
    }

    IEnumerator StartDarkness()
    {
        yield return new WaitForSeconds(0.5f);
        undarking = true;
    }
}
