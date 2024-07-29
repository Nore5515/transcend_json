using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class DialogueBox : MonoBehaviour
{

    bool dropping = false;
    bool rising = false;

    [SerializeField]
    string text;

    [SerializeField]
    TextMeshProUGUI textField;

    [SerializeField]
    bool messageHidden = false;

    // Start is called before the first frame update
    void Start()
    {
        if (messageHidden)
        {
            gameObject.SetActive(false);
        }
        textField.text = text;
    }

    private void Update()
    {
        if (dropping)
        {
            if (gameObject.transform.position.y > -gameObject.GetComponent<RectTransform>().rect.height + 10)
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
            if (gameObject.transform.position.y < 0)
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
        if (gameObject.transform.position.y < 0 && !dropping)
        {
            Debug.Log("up");
            rising = true;
        }
        if (gameObject.transform.position.y >= 0 && !rising)
        {
            Debug.Log("down");
            dropping = true;
        }
    }
}
