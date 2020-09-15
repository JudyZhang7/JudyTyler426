using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndScreen : MonoBehaviour
{
    public Vector4 color;
    SpriteRenderer spriteRenderer;
    public GameObject mainAudio;
    float timer = 0;
    public AudioClip[] clips;
    public GameObject ghost;
    public GameObject ghostBG;
    public Text text;
    public Text scoreText;

    // Start is called before the first frame update
    void Start()
    {
        color.w = 0;
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = color;
        mainAudio.GetComponent<AudioSource>().Pause();
        text.enabled = true;
        if (color.x > 0)
        {
            GetComponent<AudioSource>().clip = clips[1];
            text.color = Color.black;
            text.text = "\"Saving us is the greatest and most concrete demonstration of God's love, the definitive display of His grace throughout time and eternity.\"";
            text.text += "\n\n" + "Lives saved: " + InputManager.numSpared;
        }
        else
        {
            GetComponent<AudioSource>().clip = clips[0];
            text.color = Color.white;
            text.text = "\"When there's no more room in hell, the dead will walk the earth.\"";
            text.text += "\n\n" + "Lives saved: " + InputManager.numSpared;
        }
        GetComponent<AudioSource>().Play();
        ghost.SetActive(false);
        ghostBG.SetActive(false);
        scoreText.color = new Vector4(255, 255, 255, 0);

    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (color.w < 1)
        {
            color.w += Time.deltaTime * 0.75f;
            spriteRenderer.color = color;
        }
        else if (timer > 7)
        {
            text.enabled = false;
            SceneManager.LoadScene(0);
        }
    }
}
