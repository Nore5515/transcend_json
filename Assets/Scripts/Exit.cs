using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Exit : WorldObject
{
    public override WorldObjectJSON json { get; set; } = new();
    [SerializeField]
    SpriteRenderer sprite;

    public bool blackingOut = false;
    float time = 0.0f;
    float fadeoutSeconds = 3.0f;

    [SerializeField]
    GameObject ui1;
    [SerializeField]
    GameObject ui2;
    [SerializeField]
    GameObject ui3;

    // Start is called before the first frame update
    void Start()
    {
        json.pos = transform.position;
        json.type = TypeEnum.exit;
        json.ID = GetInstanceID();
    }

    public void Goodbye()
    {
        Debug.Log("Meep");
        StartCoroutine(WaitAndStartZoom());
    }

    private void Update()
    {
        if (blackingOut)
        {
            ui1.SetActive(false);
            ui2.SetActive(false);
            ui3.SetActive(false);
            Debug.Log("Blacking out!" + sprite.color.a);
            if (sprite.color.a < 1)
            {
                Color c = sprite.color;
                c.a = Mathf.Lerp(0.0f, 1.0f, time / fadeoutSeconds);
                time += Time.deltaTime;
                sprite.color = c;
            }
            else
            {
                blackingOut = false;
                StartCoroutine(WaitAndLoad());
            }
        }
    }

    IEnumerator WaitAndStartZoom()
    {
        yield return new WaitForSeconds(1.0f);
        blackingOut = true;
    }

    IEnumerator WaitAndLoad()
    {
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene("Victory");
    }
}
