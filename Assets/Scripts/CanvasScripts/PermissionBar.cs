using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PermissionBar : MonoBehaviour
{
    bool dropping = false;
    bool rising = false;

    [SerializeField]
    string text;

    [SerializeField]
    TextMeshProUGUI textField;

    [SerializeField]
    bool messageHidden = true;

    Vector3 start;

    // Start is called before the first frame update
    void Start()
    {
        if (messageHidden)
        {
            gameObject.SetActive(false);
            return;
        }

        start = transform.position;
        textField.text = text;
    }

    private void Update()
    {
        if (dropping)
        {
            if (gameObject.transform.position.y > start.y)
            {
                gameObject.transform.Translate(new Vector3(0.0f, -1.0f));
            }
            else
            {
                dropping = false;
            }
        }
        if (rising)
        {
            if (gameObject.transform.position.y < start.y + 80)
            {
                gameObject.transform.Translate(new Vector3(0.0f, 1.0f));
            }
            else
            {
                rising = false;
            }
        }
    }

    public void ButtonClick()
    {
        if (gameObject.transform.position.y < start.y + 80 && !dropping)
        {
            Debug.Log("up");
            rising = true;
        }
        if (gameObject.transform.position.y >= start.y && !rising)
        {
            Debug.Log("down");
            dropping = true;
        }
    }
}
